using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedPowerController : MonoBehaviour
{
    public GameObject player; // the player
    private GameObject GUIControl; // The GameController/GUI
    private GameController GameGUIControl; //GameController script from the GUI
    private PlayerController playerControl; //Obtains PlayerController script from the player GameObject

    private bool speedPower = false; //Points attracted to the player
    private const float speedPowerTimerReset = 15.0f; //Max Speed timer
    private float speedPowerTimer; 
    private float playerSpeed; //Player's original speed
    private float playerNewSpeed; //Player's new speed with speed boost
    private const float playerSpeedBoost = 1.2f; //Speed boost multiplier for the player when applied

    private double speedLightRange; //Light range of the capsule
    private const string powerUpName = "Speed"; //Name of the powerup


    // Start is called before the first frame update
    void Start()
    {
        GUIControl = GameObject.FindWithTag("GameController");
        GameGUIControl = GUIControl.GetComponent<GameController>();
        playerControl = player.GetComponent<PlayerController>();
        speedLightRange = this.gameObject.GetComponent<Light>().range;
    }

    // Update is called once per frame
    /* IF THE SPEED POWERUP IS ACTIVE AND THE TIMER GOES TO 0,
     * DISABLE THE OBJECT (SpeedPower is initally false)
     * 
     * TRIED MAKING A COOL ANIMATION WHERE THE POWERUP SHRINKS AND VANISHES
     * WILL PATCH LATER
     */
    void Update()
    {
        speedPowerTimer -= Time.deltaTime;
        if (speedPowerTimer <= 0 && speedPower == true)
        {
            speedPower = false;
            DeactivatePower();
        }
        if (speedPower == true && speedLightRange >= 0)
        {
            speedLightRange -= System.Math.Floor((Time.deltaTime * 10)) / 10.0;
            this.gameObject.GetComponent<Light>().range -= (float)speedLightRange;
        }
    }


    /*
     *NEW NAME, ACTIVATEPOWER
     *WHEN ACTIVATEPOWER IS USED, POWERUP IS CREATED
     */

    public void ActivatePower()
    {
        playerControl.SetSpeed(playerNewSpeed);
    }

    /*
     * OBJECT IS DEACTIVATED AND OBJECT IS KILLED
     */

    void DeactivatePower()
    {
        GameGUIControl.RemovePowerUp(powerUpName);
        this.playerSpeed = playerControl.getSpeed(); 
        playerControl.SetSpeed(playerSpeed);
        Destroy(this.gameObject);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            this.gameObject.layer = 8;
            GameGUIControl.AddPoints(500);
            GameGUIControl.AddPowerUp(powerUpName);
            speedPower = true;
            ActivatePower();
            speedPowerTimer = speedPowerTimerReset;
        }
    }

    
}
