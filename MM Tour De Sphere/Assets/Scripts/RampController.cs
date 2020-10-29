using System.Collections;
using System.Collections.Generic;
using System.Media;
using UnityEngine;

public class RampController : MonoBehaviour
{
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
                index++;
            }
        }
        if (index >= rampPathArray.Length)
        {
            playerControl.SetPlayerControl(true); // Give the player back control once this segment is over
            Destroy(this.gameObject); //Once finished, destroy this object and all the children
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
