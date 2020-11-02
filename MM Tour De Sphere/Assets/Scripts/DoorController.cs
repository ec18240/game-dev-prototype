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

    private string promptText;
    private const string interactButton = "A";

    // Start is called before the first frame update
    void Start()
    {
        GUIControl = GameObject.FindWithTag("GameController");
        player = GameObject.FindWithTag("Player");
        //door = this.gameObject;
        promptText = "Press " + interactButton + " to activate the door";
        canActivate = false;  
    }

    // Update is called once per frame
    void Update()
    {
        if(Vector3.Distance(player.transform.position, door.transform.position) < 3)
        {
            GUIControl.GetComponent<GameController>().SetPrompt(promptText);
            canActivate = true;
        }
        else
        {
            GUIControl.GetComponent<GameController>().SetPrompt("");
            canActivate = false;
        }
        if(canActivate == true && Input.GetKeyDown(KeyCode.A))
        {
            target = door.transform.position;
            door.GetComponent<DoorController>().OpenDoor();
            GUIControl.GetComponent<GameController>().SetPrompt("");
            canActivate = false;
            gameObject.GetComponent<DoorController>().enabled = false;
        }

    }


    /*I HAVE SINCE LEARNED A BETTER WAY TO IMPLEMENT THIS AS SEEN IN THE
     * MOVERCONTROLLER SCRIPT THAT I WROTE
     * I WILL USE THE IMPLEMENTATION THAT I MADE THERE AS A GUIDE
     * FOR HOW TO IMPROVE ON THIS IMPLEMENTATION
     */ 
    public void OpenDoor()
    {
        while (target.y < 20.0)
        {
            target.y += 0.5f;
            door.transform.position = Vector3.MoveTowards(door.transform.position, target, Time.deltaTime * 5f);
        }
        door.GetComponent<MeshRenderer>().material = door_on_material;
    }

}
