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

    private bool canActivate = false;


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
        
        if(Vector3.Distance(player.transform.position, getLocation()) < 2)
        {
            UnityEngine.Debug.Log(Vector3.Distance(player.transform.position, getLocation()));
            GUIControl.GetComponent<GameController>().SetPrompt("Press A to interact with the block");
            canActivate = true;
        }
        else
        {
            GUIControl.GetComponent<GameController>().SetPrompt("");
            canActivate = false;
        }

        if(canActivate == true && Input.GetKeyDown(KeyCode.A)){
            GUIControl.GetComponent<GameController>().SetPrompt("");
            canActivate = false;
            gameObject.GetComponent<BlockController>().enabled = false;
        }

    }
}
