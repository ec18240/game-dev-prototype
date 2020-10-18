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

    private GameObject[] childrenList;
    private Vector3 current_location; //Vector of current object
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
        setList();
        GUIControl = GameObject.FindWithTag("GameController");
    }

    void setList()
    {
        
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

    public void switchMode(Mode mode)
    {
        switch (mode)
        {
            case Mode.Enemy: state = Mode.Enemy;
                break;
            case Mode.Health: state = Mode.Health;
                break;
            case Mode.Objective: state = Mode.Objective;
                break;
            case Mode.Inactive: state = Mode.Inactive;
                break;
            default:
                UnityEngine.Debug.Log("Incorrect Mode inputted, please check code");
                break;
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
            gameObject.GetComponent<SwitchBlockController>().SwitchActivate();
            canActivate = false;
            state = Mode.Inactive;
            gameObject.GetComponent<BlockController>().enabled = false;
        }

    }
}
