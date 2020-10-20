using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Runtime.Versioning;
using UnityEngine;

public class DoorPuzzle3A : MonoBehaviour
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
        canActivate = false;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OpenDoor()
    {
        target = door.transform.position;
        while (target.y < 20.0)
        {
            target.y += 0.5f;
            door.transform.position = Vector3.MoveTowards(door.transform.position, target, Time.deltaTime * 5f);
        }
        door.GetComponent<MeshRenderer>().material = door_on_material;
    }

}
