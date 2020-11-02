using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointController : MonoBehaviour
{
    private int point_amount = 100; //AMOUNT OF POINTS EACH POINT IS WORTH
    private GameObject GUIControl; //GUICONTROL
    public GameObject player; //PLAYER

    private static bool magnetMode; //CHECKS IF THE POWERUP IS ON. IT IS STATIC AS IT AFFECTS ALL POINTS, NOT JUST ONE
    private float magnetTimer;
    private float magnetTimerRest; //INITIAL TIMER FOR THE MAGNET, HOWEVER I MAY NOT NEED TO USE THIS ANYMORE BECAUSE OF MY MAGNET POWER IMPLEMENTATION
    private const float speed = 5.0f;

    private float delta; // KEEPS CONSISTENT TRACK OF THE MOVEMENT IN RELATION TO THE FRAME-RATE

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
    /*
     * IF MAGNETMODE IS ON, THE POINTS WILL GO TO THE PLAYER
     */
    void Update()
    {
        delta = speed * Time.deltaTime;
        if(magnetMode == true)
        {
            MoveToPlayer();
        }
        
    }

    void MoveToPlayer() //IF THE PLAYER IS NEAR THE OBJECT WHILE THE POWER UP IS ON, THE POINT MOVES TO THE PLAYER
    {
        if(getDistance() < 4)
        {
            this.transform.position = Vector3.MoveTowards(this.gameObject.transform.position, player.transform.position, delta); 
        }
    }

    //GETS THE DISTANCE BETWEEN PLAYER AND CURRENT GAMEOBJECT 

    float getDistance()
    {
        return Vector3.Distance(player.transform.position, this.gameObject.transform.position);
    }

    /*IF MAGNETMODE IS SET TO TRUE
     * THEN ALL POINTS WILL BE ATTRACTED TO THE PLAYER UNTIL
     * MAGNETMODE IS SET TO FALSE!
     * 
     * I WILL CHANGE THE NAME OF THIS METHOD TO PROBABLY SOMETHING BETTER
     * 
     */ 

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
