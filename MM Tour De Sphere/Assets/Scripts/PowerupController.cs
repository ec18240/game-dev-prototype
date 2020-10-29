using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupController : MonoBehaviour
{
    public GameObject player;

    private bool powerActivate = false;

    
    private bool allPower = false; //All powers activated at the same time for 10 seconds
    private const float allPowerTimerReset = 10.0f;
    private float allPowerTimer;

    private bool magnetPower = false; //Points attracted to the player
    private const float magnetPowerTimerReset = 5.0f;
    private float magnetPowerTimer;

    private bool strengthPower = false; //Player gets more health and takes much less damage
    private const float strengthPowerTimerReset = 5.0f;
    private float strengthPowerTimer;

    private bool speedPower = false; //Player's speed increases
    private const float speedPowerTimerReset = 8.0f;
    private float speedPowerTimer;

    private bool multiplierPower = false; //Game multiplier turns to 5.0 (if not below 5 already) and does not go down until the power up is finished
    private const float multiplierPowerTimerReset = 5.0f;
    private float multiplierPowerTimer;
    private const float multiplierBoost = 5.0f;



    // Start is called before the first frame update
    void Start()
    {
        magnetPowerTimer = magnetPowerTimerReset;
        strengthPowerTimer = strengthPowerTimerReset;
        speedPowerTimer = speedPowerTimerReset;
        multiplierPowerTimer = multiplierPowerTimerReset;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void ActivatePower(GameObject powerUp)
    {
        switch (powerUp.tag)
        {
            case "MagnetPower":
                magnetPower = true;
                magnetPowerTimer = magnetPowerTimerReset;
                break;

        }
    }

    void DeactivatePower(GameObject powerUp)
    {
        switch (powerUp.tag)
        {
            case "MagnetPower":
                magnetPower = false;
                magnetPowerTimer = 0;
                break;
        }
    }

    void OnTriggerEnter(Collider other)
    {

    }

    void AllPowerActivate()
    {

    }
}
