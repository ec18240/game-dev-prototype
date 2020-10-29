using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointController : MonoBehaviour
{
    private int point_amount = 100;
    private GameObject GUIControl;
    public GameObject player;

    private static bool magnetMode; //Whether the Magnet power-up is on
    private float magnetTimer;
    private float magnetTimerRest; //Initial start time of the magnet powerup
    private const float speed = 5.0f;

    private float delta; // Keeps a consistent track of the movement speed during a frame

    // Start is called before the first frame update
    void Start()
    {
        GUIControl = GameObject.FindWithTag("GameController");
        magnetMode = false;
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            GUIControl.GetComponent<GameController>().AddPoints(point_amount);
            Destroy(this.gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        delta = speed * Time.deltaTime;
        if(magnetMode == true)
        {
            MoveToPlayer();
        }
        
    }

    void MoveToPlayer() //If the player is near the object (whilst the power up is on), points get attracted to the player
    {
        if(getDistance() < 4)
        {
            this.transform.position = Vector3.MoveTowards(this.gameObject.transform.position, player.transform.position, delta); 
        }
    }

    float getDistance()
    {
        return Vector3.Distance(player.transform.position, this.gameObject.transform.position);
    }

    public static void SetMagnet(bool value)
    {
        if(value == true)
        {
            magnetMode = true;
        }
        else
        {
            magnetMode = false;
        }

    }

}
