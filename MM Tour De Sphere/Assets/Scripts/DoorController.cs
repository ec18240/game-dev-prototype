using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Runtime.Versioning;
using UnityEngine;

public class DoorController : MonoBehaviour
{

    private GameObject player;
    public GameObject door;
    private GameObject GUIControl;
    private Vector3 target;

    public Material door_on_material;

    private bool canActivate;

    private string promptText;

    // Start is called before the first frame update
    void Start()
    {
        GUIControl = GameObject.FindWithTag("GameController");
        player = GameObject.FindWithTag("Player");
        //door = this.gameObject;
        promptText = "Press A to activate the door";
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
