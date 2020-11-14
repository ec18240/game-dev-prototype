using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoverController : MonoBehaviour
{
    public GameObject trigger; //Get object that triggers the movement (if present)

    public Vector3 starting_point; //Object's starting point
    public Vector3 end_point; //Object's end point

    private Vector3 target_point; //Target, this will switch between the start and end point

    public float speed;
    private float originalSpeed; 
    private float delta;

    public bool moveOnlyOnce;
    public string modeTag;
    private Mode mode;

    public Transform proximityLocation;
    public int proximityDistance;

    /*Sets the mode of the mover controller:
     *  Normal - The object moves automatically without any restrictions
     *  Proximity - The object only moves when in the proximity of a nearby trigger
     *  Manual - The object does not move unless called upon
     */

    enum Mode
    {
        Normal,
        Proximity,
        Manual
    }

    // Start is called before the first frame update
    void Start()
    {
        DeclareTrigger();
        modeTag = modeTag.ToUpper().Trim(); //Captures current gameobject's tag
        SetMode();
        target_point = end_point;
        //speed = 5f;
        originalSpeed = this.speed; //Keeps a record of the current speed in case the object's movement is paused
        moveOnce();
    }

    // Update is called once per 
    /*
    Object moves to the target point. If the target point hits the starting_point, the target becomes the end_point
    If the target hits the end_point, the target becomes the start_point

    When target position == the position of the start_point or end_point, then the target has hit one of the points
     */
    void Update()
    {
        if (mode == Mode.Normal)
        {
            delta = speed * Time.deltaTime; //Speed of the moving object in correspondence to the frame-rate
            transform.position = Vector3.MoveTowards(transform.position, target_point, delta);

            if (transform.position == end_point)
            {
                ChangePoint(starting_point);
            }
            else if (transform.position == starting_point)
            {
                ChangePoint(end_point);
            }
        }
        else if(mode == Mode.Proximity)
        {
            
            while (transform.position != end_point && GetDistance() < 5)
            {
                delta = speed * Time.deltaTime; //Speed of the moving object in correspondence to the frame-rate
                transform.position = Vector3.MoveTowards(transform.position, target_point, delta);
            }
        }
        
    }
    /*
     * Pauses the move
     * False = Do not pause
     * True = Pause
     */

    public void PauseMove(bool pause)
    {
        if (pause)
        {
            this.speed = 0;
        }
        else
        {
            this.speed = originalSpeed;
        }
        
    }

    /*
     * Checks if the object should just move once
     */

    void moveOnce()
    {
        if (moveOnlyOnce && this.mode == Mode.Normal)
        {
            while (transform.position != end_point)
            {
                delta = speed * Time.deltaTime; //Speed of the moving object in correspondence to the frame-rate
                transform.position = Vector3.MoveTowards(transform.position, target_point, delta);
            }
            this.gameObject.GetComponent<MoverController>().enabled = false;
        }
        else
        {
            moveOnlyOnce = false; //If the user has left this variable as None, it is set to false (for consistency)
        }
    }

    /*
     * Returns the distance between the object and the player
     */
    
    float GetDistance()
    {
        return Vector3.Distance(trigger.transform.position, proximityLocation.position);
    }

    /*
     * Swaps the target position
     */
    void ChangePoint(Vector3 vector_point)
    {
        target_point = vector_point;
    }

    /*
     * Sets the trigger mode
     */

    void SetMode()
    {
        switch (modeTag)
        {
            case null: this.mode = MoverController.Mode.Normal;
                break;
            case "N":
                this.mode = MoverController.Mode.Normal;
                break;
            case "P":
                this.mode = MoverController.Mode.Proximity;
                break;
            default: UnityEngine.Debug.Log("Error, invalid mode type - MOVER CONTROLLER (155)");
                break;
        }
    }

    /*
     * Sets the player variable
     */

    void DeclareTrigger()
    {
        if(trigger != null)
        {
            return;
        }
        trigger = GameObject.FindWithTag("Player");
        if (proximityLocation == null)
        {
            UnityEngine.Debug.Log("Error, null proximity mode - MOVER CONTROLLER (151)");
            proximityLocation = this.gameObject.transform;
        }
    }

    /*
     * Code to manually move an object from one place to another
     */

    public void ManualMove(Vector3 start, Vector3 end, bool once)
    {
        if(start != null)
        {
            this.starting_point = start;
        }
        if(end != null)
        {
            this.end_point = end;
        }

        while (transform.position != end_point)
        {
            delta = speed * Time.deltaTime; //Speed of the moving object in correspondence to the frame-rate
            transform.position = Vector3.MoveTowards(transform.position, target_point, delta);
        }

        if (once)
        {
            this.gameObject.GetComponent<MoverController>().enabled = false;
        }
        
    }
}
