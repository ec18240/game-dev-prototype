using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalAreaEvent : MonoBehaviour
{
    public GameObject player; //PLAYER INVOLVED IN THIS SCRIPTED EVENT
    private PlayerController playerScript; //PLAYER CONTROLLER SCRIPT
    

    public GameObject target; //WHEN PLAYER COLLIDES WITH TARGET
    public GameObject [] door; //DOOR BEING CLOSED
    private DoorController doorClosed; //SCRIPT THAT WILL CLOSE DOOR
    public GameObject gameControl; //GameController object
    private GameController gameScript; //GameController Script

    private const string interactButton = "A"; //BUTTON DISPLAYED WHEN INTERACTING WITH OBJECT

    private float radius_distance;

    private bool canActivate;


    // Start is called before the first frame update
    void Start()
    {
        initialise();
        playerScript.SetPlayerControl(false); //PLAYER CAN'T BE CONTROLLED DURING SCRIPTED EVENT
    }

    void initialise()
    {
        radius_distance = 5.0f;
        canActivate = false;

        if (player == null)
        {
            player = GameObject.FindWithTag("Player");
        }
        if(gameControl == null)
        {
            gameControl = GameObject.FindWithTag("GameController");
        }
        gameScript = gameControl.GetComponent<GameController>();
        playerScript = player.GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {

        //IF THE PLAYER IS LESS THAN 5 (I guess CM) AWAY FROM THE BLOCK, DISPLAY A PROMPT (THIS IS BUGGED, WILL FIX SOON)
        if (Vector3.Distance(player.transform.position, getLocation()) < radius_distance)
        {
           gameScript.SetPrompt("Press " + interactButton + " to interact with the block");
            canActivate = true;
        }
        else
        {
            gameScript.SetPrompt("");
            canActivate = false;
        }

        //IF NOT ALREADY ACTIVATED, THE PLAYER IS ABLE TO PRESS A TO INTERACT WITH THE BLOCK
        // I MAY REDO THIS IMPLEMENTATION LIKE I HAVE DONE WITH MAGNET CONTROLLER.
        // MAKE A SEPARATE BLOCK CONTROLLER AND LEAVE THE PARTS THAT ALL BLOCKS HAVE IN THIS CODE

        if (canActivate == true && Input.GetKeyDown(KeyCode.A))
        {
            gameScript.SetPrompt("");
            switchActivate();
            canActivate = false;
            gameObject.GetComponent<BlockController>().enabled = false;
        }

    }

    void switchActivate()
    {
        for(int index = 0; index<door.Length; index++)
        {
            doorClosed = door[index].GetComponent<DoorController>();
            doorClosed.enabled = true;
            doorClosed.OpenDoor();
        }
        
    }

    public Vector3 getLocation()
    {
        return this.gameObject.transform.position;
    }
}
