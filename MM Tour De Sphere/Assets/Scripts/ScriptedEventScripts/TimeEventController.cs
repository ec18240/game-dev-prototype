using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class TimeEventController : MonoBehaviour
{
    //Script that can be used to alter TimeManager for a particular period

    public GameObject trigger;


    //Gets the TimeManager that the user wants to edit
    public TimeManagerController timer;

    public float timeFactor;
    public float timeFactorLength;

    public string modeTag;// Tag for the mode 
    private Mode mode; //Current mode for the object

    public bool triggerOnce; //If this is only supposed to come into play once

    /*
     * Proximity - If the player is near, perform this action
     * Inactive - Do nothing right now
     * Wait - Wait until something happens to be activated
     * Clash -  Activate when a direction collision with a collider occurs
     */

    enum Mode
    {
        Proximity,
        Wait,
        Clash,
        Inactive
    }

    // Start is called before the first frame update
    void Start()
    {
        modeTag = modeTag.ToUpper().Trim(); //Normalises the data
        SetMode();
        initialise();
        
    }

    /*
     * Default values for unset values
     */


    void initialise()
    {
        if(trigger == null)
        {
            trigger = GameObject.FindWithTag("Player");
        }
    }

    /*
     * If the user collides with the object while the object
     * is in clash mode, activate
     * 
     */

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player" && mode == TimeEventController.Mode.Clash)
        {
            UnityEngine.Debug.Log("AYE GUYS");
            ChangeMotion();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //Just like TimeManagerController. This will let you call TimeManagerController with the edited time mods
    void ChangeMotion()
    {
        timer.SetSlowMotion(timeFactor, timeFactorLength);
        timer.ChangeMotion();
        if (triggerOnce)
        {
            this.gameObject.GetComponent<TimeEventController>().enabled = false;
        }
    }

    /*
     * Sets the trigger mode
     * Proximity - If the player is near, perform this action
     * Inactive - Do nothing right now
     * Wait - Wait until something happens to be activated
     * Clash -  Activate when a direction collision with a collider occurs
     */

    void SetMode()
    {
        switch (modeTag)
        {
            case null:
                this.mode = TimeEventController.Mode.Inactive;
                break;
            case "I":
                this.mode = TimeEventController.Mode.Inactive;
                break;
            case "P":
                this.mode = TimeEventController.Mode.Proximity;
                break;
            case "W":
                this.mode = TimeEventController.Mode.Wait;
                break;
            case "C":
                this.mode = TimeEventController.Mode.Clash;
                UnityEngine.Debug.Log("CLASH!");
                UnityEngine.Debug.Log(trigger.tag);
                break;
            default:
                UnityEngine.Debug.Log("Error, invalid mode type - TimeEventController (117)");
                break;
        }
    }

}
