using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    private GameObject current_player;
    public GameObject finish_point;

    private int points; //points
    private int points_multiplier; //the multiplier
    private float multiplier_time = 5.0f; //time that time multiplier is active
    private const int multiplier_point_bracket = 5; //Every 5 points, the multiplier increases
    private int multiplier_point_count = 0; //Tracks how many points have been collected before the multiplier is increased

    public Text scoreText;
    public Text promptText;
    public Text playerHealthText;
    public Text winText;
    public Text powerUpText; //Displays the current powerUp

    private float gameTimer;

    private string multiplierText;

    private bool multiplier_on; //Informs whether the multiplier is on or not

    private bool text_disable; // if on, all text in game will be disabled (other than winText);

    private ArrayList powerUpList = new ArrayList();

    public float lowerBound; //LOWEST Y-AXIS PLAYER CAN BE BEFORE THEY DIE 

    private CheckPointController currentCheckpointData; //KEEPS TRACK OF THE CURRENT CHECKPOINT



    // Start is called before the first frame update
    void Start()
    {
        text_disable = false; //text is not automatically disabled
        multiplier_on = false;
        points = 0;
        points_multiplier = 1;
        scoreText.text = "POINTS: 0";
        multiplierText = "MULTIPLIER: 1X";
        current_player = GameObject.FindWithTag("Player");
        displayText();
        gameTimer = 0;
    }

    /* THROUGHOUT ALL OF THIS,
     * THE GAMECONTROLLER CHECKS IF THERE ARE ANY MULTIPLIERS PRESENT
     * IT ALSO KEEPS TRACK OF THE GAME TIME BY ADDING TIME.DELTATIME
     */ 

    // Update is called once per frame
    void Update()
    {
        gameTimer += Time.deltaTime;
        checkMultiplier();
        CheckDead();

    }

    void CheckDead() {
        if(current_player.transform.position.y <= lowerBound && finish_point.GetComponent<FinishController>().getFinish() == false)
        {
            if(this.currentCheckpointData == null)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
            else
            {
                if (this.currentCheckpointData.GetLives() <= 0)
                {
                    SceneManager.LoadScene(3, LoadSceneMode.Single);
                    Cursor.lockState = CursorLockMode.None;
                }
                else
                {
                    this.currentCheckpointData.RespawnPlayer();
                    current_player.GetComponent<PlayerController>().SetHealth(5.0f);
                    displayText();
                }
            }
            
        }

        if (getHealth() <= 0.0f)
        {
            if(this.currentCheckpointData != null)
            {
                if(this.currentCheckpointData.GetLives() <= 0)
                {
                    SceneManager.LoadScene(3, LoadSceneMode.Single);
                    Cursor.lockState = CursorLockMode.None;
                }
                else
                {
                    this.currentCheckpointData.RespawnPlayer();
                    current_player.GetComponent<PlayerController>().SetHealth(5.0f);
                    displayText();
                }
            }
            else
            {
                SceneManager.LoadScene(3, LoadSceneMode.Single);
                Cursor.lockState = CursorLockMode.None;
            }
            
        }

    }

    /*
     * CHECKS TO SEE IF THE MULTIPLIER IS LESS THAN 0
     * IF SO AND THE MULTIPLIER IS ON, THE MULTIPLIER IS SET TO 0
     * (Ask me, Ashley, for more details)
     */

    void checkMultiplier()
    {
        multiplier_time -= Time.deltaTime;
        if (multiplier_time < 0 && multiplier_on == true)
        {
            UnityEngine.Debug.Log("MULTIPLIER OFF");
            multiplier_point_count = 0;
            multiplier_on = false;
            this.points_multiplier = 1;
            multiplier_time = 5;
            multiplierText = "Multiplier: " + this.points_multiplier + "X";
            displayText();
        }
    }
    /*
     * GETS THE OVERALL GAME TIMER WHICH IS RUNNING IN THE UPDATE METHOD
     * THIS GETS ROUNDED TO ONE DECIMAL PLACE
     * THE TIME IS DISPLAYED IN SECONDS
     */

    public double getTimer()
    {
        return Math.Round(this.gameTimer, 1);
    }

    public void AddPoints(int point) //Adds (and later decreases) points
    {
        multiplier_on = true;
        if(multiplier_time > 0) 
        {
            UnityEngine.Debug.Log("MULTIPLIER ON");
            multiplier_point_count++;
            if(multiplier_point_count == multiplier_point_bracket)
            {
                multiplier_point_count = 0;
                this.points_multiplier++;
                multiplierText = "Multiplier: " + this.points_multiplier + "X";
            }
            multiplier_time = 5.0f;
        }

        this.points += point * this.points_multiplier;
        displayText();
    }

    public void displayText() //DISPLAYS INFORMATION IN THE UI, CALLED BY OTHER SCRIPTS
    {
        if(text_disable == false) //IF THE TEXT IS DISABLED I.E WHEN THE GAME FINISHES, TEXT IS NOT DISPLAYED
        {
            scoreText.text = "POINTS: " + this.points + getMultiplier();
            if (currentCheckpointData != null)
            {
                playerHealthText.text = "CHECKPOINT LIVES: " + currentCheckpointData.GetLives().ToString() +
                    "| HEARTS: " + getHealth().ToString();
            }
            else
            {
                playerHealthText.text = "HEARTS: " + getHealth().ToString();
            }
            
        }

    }

    public void DisplayTime() //EVENTUALLY USED TO DISPLAY TIME IN THE GAME, WILL IMPLEMENT IN FULL VERSION (Ashley)
    {

    }

    /*
     * WILL BE USED TO CREATE A LIST OF USED POWERUPS
     * THIS LIST WILL BE USED TO DISPLAY ALL CURRENT POWERUPS AVAILABLE
     * ADD AND REMOVE METHODS ADD AND REMOVE POWER UPS FROM THE LIST
     */ 

    public void AddPowerUp(string powerup)
    {
        if (powerUpList.Contains(powerup))
        {
            powerUpList.Add(powerup);
        }
        DisplayPower();
    }

    public void RemovePowerUp(string powerup)
    {
        powerUpList.Remove(powerup);
        DisplayPower();
    }

    public void DisplayPower()
    {
        string text = "";
        if(powerUpList.Count != 0)
        {
            text += "POWER UP: ";
            foreach (var power in powerUpList){
                text += power + "\n"; 
            }
        }
        powerUpText.text = text;
    }
    

    //GETS THE MULTIPLIER IN STRING FROM HOWEVER I MIGHT CHANGE THIS
    string getMultiplier() 
    {
        return "\n" + multiplierText;
    }

    public void setMultiplier(int new_multiplier) //SETS THE MULTIPLIER
    {
        points_multiplier = new_multiplier;
    }

    float getHealth()
    {
        float amount = current_player.GetComponent<PlayerController>().getHealth();
        return amount;
    }

    public void SetPrompt(string text)
    {
        promptText.text = text;
    }

    /*IF THE TEXT IS DISABLED
     * DO NOT DISPLAY ANYTHING
     */

    public void DisableText(bool value)
    {
        if(value == true)
        {
            scoreText.text = "";
            promptText.text = "";
            multiplierText = "";
            playerHealthText.text = "";
            text_disable = true;
        }
        else
        {
            text_disable = false;
            displayText();
        }

    }

    int getResult() //RETRIEVES FINAL POINTS
    {
        return points * points_multiplier;
    }

    public void DisplayWin() //DISPLAYS THE WIN TEXT
    {
        winText.text = "FINISH LINE REACHED! \n" + getTimer() + " SECONDS \n" + this.points + " POINTS\n" + getResult() + " FINAL POINTS (x" + this.points_multiplier + " MULTIPLIER BONUS)";
    }

    public void SetCheckPointData(CheckPointController checkpointData)
    {
        this.currentCheckpointData = checkpointData;
    }

    CheckPointController GetCheckPointData()
    {
        return this.currentCheckpointData;
    }
}
