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

    private const string interactButton = "A";

    public static bool isUsed; //USED TO TELL IF CURRENT GAMEOBJECT IS IN USE, WILL IMPLEMENT LATER
    private float radius_distance; //DISTANCE PLAYER MUST BE FROM THE BLOCK

    /*TYPE OF BLOCK THE BLOCK IS
     *HEALTH : THIS BLOCK WILL HEAL YOU
     *ENEMY: THIS BLOCK WILL DAMAGE YOU
     *OBJECTIVE: THIS BLOCK IS A SWITCH
     *INACTIVE: A WHITE INACTIVE BLOCK WHICH DOES NOTHING
     */

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
        radius_distance = 5.0f;
    }

    //DEAD METHOD, INITIALLY INTENDED TO GET LIST OF CHILDREN
    //MAY NOT NEED TO ADD IT IN BUT I WILL TRY TO (Ashley)

    void setList()
    {

    }

    //SETS MODES AS SHOWN ABOVE

    void setMode()
    {
        if (gameObject.tag == "EnemyBlock")
        {
            state = Mode.Enemy;
        }
        else if (gameObject.tag == "HealthBlock")
        {
            state = Mode.Health;
        }
        else if (gameObject.tag == "SwitchBlock")
        {
            state = Mode.Objective;
        }
        else
        {
            state = Mode.Inactive;
        }
    }

    //ALLOWS BLOCKS TO SWITCH MODES

    public void switchMode(Mode mode)
    {
        switch (mode)
        {
            case Mode.Enemy:
                state = Mode.Enemy;
                break;
            case Mode.Health:
                state = Mode.Health;
                break;
            case Mode.Objective:
                state = Mode.Objective;
                break;
            case Mode.Inactive:
                state = Mode.Inactive;
                break;
            default:
                UnityEngine.Debug.Log("Incorrect Mode inputted, please check code");
                break;
        }
    }

    public void SetDisable()
    {
        if (state == Mode.Enemy)
        {
            Destroy(this.gameObject);
        }
    }


    //GET LOCATION OF CURRENT GAMEOBJECT
    public Vector3 getLocation()
    {
        return gameObject.transform.position;
    }

    //RETRIEVE CURRENT MODE OF THE OBJECT

    public Mode getMode()
    {
        return state;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
