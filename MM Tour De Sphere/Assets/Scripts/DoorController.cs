using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Runtime.Versioning;
using UnityEngine;

public class DoorController : MonoBehaviour
{

    private GameObject player; //PLAYER
    public GameObject door; //INDIVIDUAL DOOR THAT HOLDS THIS SCRIPT

    private GameObject GUIControl; //GUI GAMEOBJECT WHICH HAS GAMECONTROLLER SCRIPT
    private Vector3 target; // TARGET, NEW LOCATION OF THE DOOR

    public Material door_on_material; //MATERIAL OF THE DOOR ONCE IT IS ACTIVATED

    private bool canActivate; //WHETHER THE DOOR CAN BE ACTIVATED OR NOT
    private bool doorActivated; //Whether the door HAS been activated 

    private string promptText;
    private const string interactButton = "A";

    public Vector3 newPosition; // THE NEW POSITION
    public float speed; // DOOR MOVEMENT SPEED
    public bool initaliseSpeed; //TO SEE IF SPEED 0 IS INTENDED (see initalise())

    // Start is called before the first frame update
    void Start()
    {
        initalise();
        GUIControl = GameObject.FindWithTag("GameController");
        player = GameObject.FindWithTag("Player");
        promptText = "Press " + interactButton + " to activate the door";
        canActivate = false;
        doorActivated = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (doorActivated)
        {
            this.gameObject.transform.position = Vector3.MoveTowards(transform.position, newPosition, speed);
            if(this.gameObject.transform.position == newPosition)
            {
                gameObject.GetComponent<DoorController>().enabled = false;
            }
        }
        else
        {
            if (Vector3.Distance(player.transform.position, door.transform.position) < 6.5)
            {
                GUIControl.GetComponent<GameController>().SetPrompt(promptText);
                canActivate = true;
            }
            else
            {
                GUIControl.GetComponent<GameController>().SetPrompt(""); //CLEAR THE PROMPT TEXT
                canActivate = false; //IF USER IS FAR, THEY CAN'T ACTIVATE DOOR
            }
            if (canActivate == true && Input.GetKeyDown(KeyCode.A))
            {
                SoundManagerScript.PlaySound("DoorsOpen");
                GUIControl.GetComponent<GameController>().SetPrompt(""); //CLEAR THE PROMPT TEXT
                canActivate = false;   //IF USER IS FAR, THEY CAN'T ACTIVATE DOOR  
                doorActivated = true; // THE DOOR HAS BEEN ACTIVATED
                door.GetComponent<MeshRenderer>().material = door_on_material;
            }
        }
        

    }

    void initalise()
    {
        if(newPosition == null)
        {
            newPosition = this.gameObject.transform.position; // IF NEW POSITION IS NOT SET, THE OBJECT WILL NOT MOVE
        }
        if(speed == 0 && initaliseSpeed == false)
        {
            speed = 2.0f; //IF NO SPEED IS SET, THEN SPEED IS ZERO
        }
    }

    public void OpenDoor() //OPEN DOOR
    {
        this.doorActivated = true;
        this.canActivate = true;

        
    }

    public void OpenDoor(Vector3 newPosition) //OPEN DOOR AT POSITION <X>
    {
        this.newPosition = newPosition;
        doorActivated = true;
        this.canActivate = true;

    }

}
