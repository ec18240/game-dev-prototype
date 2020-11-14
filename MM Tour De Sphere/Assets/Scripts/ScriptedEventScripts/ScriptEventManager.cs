using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptEventManager : MonoBehaviour
{
    public GameObject player; //The person being scripted
    private PlayerController playerControl; //PlayerController from the player gameobject

    public GameObject[] scriptedObjects; //If any other objects are involved in the scripted event, add them here
    public TimeManagerController timer; //Affects slow-motion
     

    // Start is called before the first frame update
    void Start()
    {
        initialise();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void initialise()
    {
        if(player == null)
        {
            UnityEngine.Debug.Log("Object has not been set, using default object");
            player = GameObject.FindWithTag("Player");
        }
        else
        {
            playerControl = player.GetComponent<PlayerController>();
        }
    }

    public void Disable()
    {
        playerControl.SetPlayerControl(true); //Control is given back to the player just before the script ends
        this.gameObject.GetComponent<ScriptEventManager>().enabled = false; // Scripted even is over 
    }

    public GameObject getPlayer()
    {
        return player;
    }

    public TimeManagerController getTimeManager()
    {
        return timer;
    }
}
