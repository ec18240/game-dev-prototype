using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using UnityEngine;

public class BlockController : MonoBehaviour
{
    private Mode state;

    public GameObject player; // this is the player;
    private GameObject GUIControl; //Gets the GUI;

    private Vector3 current_location; //Vector of current object;


    public enum Mode
    {
        Health,
        Enemy,
        Objective,
        Inactive
    }
    // Start is called before the first frame update
    void Start()
    {
        setMode();
        GUIControl = GameObject.FindWithTag("GameController");
    }

    void setMode()
    {
        if(gameObject.tag == "EnemyBlock")
        {
            state = Mode.Enemy;
        }
        else if(gameObject.tag == "HealthBlock")
        {
            state = Mode.Health;
        }
        else if(gameObject.tag == "SwitchBlock")
        {
            state = Mode.Objective;
        }
        else
        {
            state = Mode.Inactive;
        }
    }

    public Vector3 getLocation()
    {
        return gameObject.transform.position;
    }

    public Mode getMode()
    {
        return state;
    }

    // Update is called once per frame
    void Update()
    {
        
        if(Vector3.Distance(player.transform.position, getLocation()) < 10)
        {
            UnityEngine.Debug.Log(Vector3.Distance(player.transform.position, getLocation()));
            GUIControl.GetComponent<GameController>().SetPrompt("Press A to interact with the block");
        }
    }
}
