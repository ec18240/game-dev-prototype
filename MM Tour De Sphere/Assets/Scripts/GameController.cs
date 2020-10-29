using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    private GameObject current_player;

    private int points; //[points
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

    // Update is called once per frame
    void Update()
    {
        gameTimer += Time.deltaTime;
        checkMultiplier();
    }

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

    public void displayText() //Displays info
    {
        if(text_disable == false) //Checks if text is disable first before displaying info
        {
            scoreText.text = "POINTS: " + this.points + getMultiplier();
            playerHealthText.text = "HEARTS: " + getHealth();
        }
    }

    public void DisplayTime() //Eventually used to display time in-game
    {

    }

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
    

    string getMultiplier() //Gets the multiplier in string form
    {
        return "\n" + multiplierText;
    }

    public void setMultiplier(int new_multiplier) //sets multiplier
    {
        points_multiplier = new_multiplier;
    }

    string getHealth()
    {
        float amount = current_player.GetComponent<PlayerController>().getHealth();
        switch (amount)
        {
            case 3.0f: return "<3 <3 <3";
            case 2.0f: return "<3 <3";
            case 1.0f: return "<3";
            default: return "DEAD";
        }
    }

    public void SetPrompt(string text)
    {
        promptText.text = text;
    }

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

    int getResult() //Gets final points
    {
        return points * points_multiplier;
    }

    public void DisplayWin() //Displays win text
    {
        winText.text = "FINISH LINE REACHED! \n" + getTimer() + " SECONDS \n" + this.points + " POINTS\n" + getResult() + " FINAL POINTS (x" + this.points_multiplier + " MULTIPLIER BONUS)";
    }
}
