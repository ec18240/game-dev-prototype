using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RampController : MonoBehaviour
{
    /*Concept(Ashley):
     * 
     * When writing this script, the idea was, there'd be points along a path which the
     * player moves towards. The path is based on the rampPathArray.
     * Each point is a sphere in an invisible camera layer that I added (so you can't see the spheres in-game)
     * 
     * The player will move towards each sphere in the index of the array. This script simulates a 
     * quick-time scripted event so the player does not have control of the mummy until the event is done
     */



    public GameObject [] rampPathArray; //Array of all the ramp points
    private bool rampActivate; //Detects whether player has hit the ramp or not
    private int index;
    public GameObject player;
    private PlayerController playerControl;

    private float speed = 25.0f;
    private float delta;

    // Start is called before the first frame update
    void Start()
    {
        rampActivate = false; //Checks whether the ramp has been collided with
        index = 0;
        playerControl = player.GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        
        if (rampActivate == true)//When player collides with ramp, perform this code on every frame
        {
            delta = speed * Time.deltaTime;
            player.transform.position = Vector3.MoveTowards(player.transform.position, rampPathArray[index].transform.position, delta);
            if (getDistance() <= 1)
            {
                UnityEngine.Debug.Log("INDEX BEFORE: " + index);
                index++;
                UnityEngine.Debug.Log("INDEX AFTER: " + index);
            }
        }
        if (index >= rampPathArray.Length )
        {
            UnityEngine.Debug.Log("INDEX AFTER (CLOSE): " + index);
            rampActivate = false; //Close telepot/travel
            playerControl.SetPlayerControl(true); // Give the player back control once this segment is over
            Destroy(this.gameObject); //Once finished, destroy this object and all the children
            this.gameObject.GetComponent<RampController>().enabled = false; //Close script
        }
    }

    float getDistance()
    {
        return Vector3.Distance(player.transform.position, rampPathArray[index].transform.position);
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            rampActivate = true;
            playerControl.SetPlayerControl(false); //Give the player no control during this scripted event
        }
    }
}
