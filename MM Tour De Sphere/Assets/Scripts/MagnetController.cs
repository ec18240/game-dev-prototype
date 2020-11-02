using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagnetController : MonoBehaviour
{
    public GameObject player; // the player
    private GameObject GUIControl; // The GameController/GUI
    private GameController GameGUIControl; //GameController script from the GUI
    public GameObject point; //Template of a point
    private PointController pointControl; //Obtains PointController script from the point GameObject

    private bool magnetPower = false; //Points attracted to the player
    private const float magnetPowerTimerReset = 20.0f; //Max Magnet timer
    private float magnetPowerTimer;

    private double magnetLightRange; //Light range of the capsule
    private const string powerUpName = "Magnet"; //Name of the powerup

    // Start is called before the first frame update
    void Start()
    {
        GUIControl = GameObject.FindWithTag("GameController");
        GameGUIControl = GUIControl.GetComponent<GameController>();
        pointControl = point.GetComponent<PointController>();
        magnetLightRange = this.gameObject.GetComponent<Light>().range;
    }

    // Update is called once per frame
    /* IF THE MAGNET POWERUP IS ACTIVE AND THE TIMER GOES TO 0,
     * DISABLE THE OBJECT (MagnetPower is initally false)
     * 
     * TRIED MAKING A COOL ANIMATION WHERE THE POWERUP SHRINKS AND VANISHES
     * WILL PATCH LATER
     */
    void Update()
    {
        magnetPowerTimer -= Time.deltaTime;
        if(magnetPowerTimer <= 0 && magnetPower == true)
        {
            magnetPower = false;
            SetMagnet(magnetPower);
            GameGUIControl.RemovePowerUp(powerUpName);
            SetDisable();
        }
        if(magnetPower == true && magnetLightRange >= 0)
        {
            magnetLightRange -= System.Math.Floor((Time.deltaTime*10))/10.0;
            this.gameObject.GetComponent<Light>().range -= (float)magnetLightRange;
        }
    }

    
    /*
     * ALL POINTS WILL BE ATTRACTED TO THE MAGNET
     * WILL RENAME METHOD TO SOMETHING MORE UNDERSTANDBALE LIKE
     * GoToPlayer MAYBE!
     */
    public static void SetMagnet(bool value)
    {
        PointController.SetMagnet(value);
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            this.gameObject.layer = 8;
            GameGUIControl.AddPoints(500);
            GameGUIControl.AddPowerUp(powerUpName);
            magnetPower = true;
            SetMagnet(magnetPower);
            magnetPowerTimer = magnetPowerTimerReset;
        }
    }

    //KILL THE OBJECT

    void SetDisable()
    {
        Destroy(this.gameObject);
    }
}
