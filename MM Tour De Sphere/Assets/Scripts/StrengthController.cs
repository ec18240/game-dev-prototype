using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StrengthController : MonoBehaviour
{
    public GameObject player; // the player
    private PlayerController playerControl; // PlayerController script from player
    private GameObject GUIControl; // The GameController/GUI
    private GameController GameGUIControl; //GameController script from the GUI
    public GameObject block; //Template of a block
    private BlockController blockControl; //Obtains BlockController script from the point GameObject

    private bool strengthPower = false; //Sets where player can destroy red objects
    private const float strengthPowerTimerReset = 30.0f; //Max strength timer
    private float strengthPowerTimer;
    private const string powerUpName = "Strength"; //Name of the powerup

    //private double magnetLightRange; //Light range of the capsule
    //

    // Start is called before the first frame update
    void Start()
    {
        GUIControl = GameObject.FindWithTag("GameController");
        GameGUIControl = GUIControl.GetComponent<GameController>();
        blockControl = block.GetComponent<BlockController>();
        playerControl = player.GetComponent<PlayerController>();
    }

    // Update is called once per frame
    /* IF THE STRENGTH POWERUP IS ACTIVE AND THE TIMER GOES TO 0,
     * DISABLE THE OBJECT (strengthPower is initally false)
     * 
     * TRIED MAKING A COOL ANIMATION WHERE THE POWERUP SHRINKS AND VANISHES
     * WILL PATCH LATER
     */
    void Update()
    {
        strengthPowerTimer -= Time.deltaTime;
        if (strengthPowerTimer <= 0 && strengthPower == true)
        {
            strengthPower = false;
            SetStrength(strengthPower);
            GameGUIControl.RemovePowerUp(powerUpName);
            SetDisable();
        }
    }


    /*
     * PLAYER WILL BE ABLE DESTROY RED OBJECTS
     */
    public void SetStrength(bool value)
    {
        playerControl.SetStrength(value);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            this.gameObject.layer = 8;
            GameGUIControl.AddPoints(250);
            GameGUIControl.AddPowerUp(powerUpName);
            strengthPower = true;
            SetStrength(strengthPower);
            strengthPowerTimer = strengthPowerTimerReset;
        }
    }

    //KILL THE OBJECT

    void SetDisable()
    {
        Destroy(this.gameObject);
    }
}
