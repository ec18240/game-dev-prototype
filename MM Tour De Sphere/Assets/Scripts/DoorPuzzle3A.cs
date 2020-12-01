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
    public Vector3 newPosition;//New position
    public Material door_on_material; // material for when the door is activated
    private bool doorActivated; //Check if the door is activated

    public float doorMovementSpeed;

    // Start is called before the first frame update
    void Start()
    {
        GUIControl = GameObject.FindWithTag("GameController");
        player = GameObject.FindWithTag("Player");
        doorActivated = false;
    }

    // Update is called once per frame
    void Update()
    {
        /* MESSAGE TO DEMONSTRATOR (From Ashley)
         * If you are wondering why I made this script instead of just using door controller
         * It was because I had an issue where if one door was activated, every door would activate
         * Didn't have time to figure out why but yes, using a single doorcontroller would be better
         */

        if (doorActivated)
        {
            this.gameObject.transform.position = Vector3.MoveTowards(transform.position, newPosition, doorMovementSpeed);
            if (this.gameObject.transform.position == newPosition)
            {
                gameObject.GetComponent<DoorPuzzle3A>().enabled = false;
            }
        }
    }

    public void OpenDoor() //Change door and state that the door is activated when the method runs
    {
        door.GetComponent<MeshRenderer>().material = door_on_material;
        doorActivated = true;
    }

}
