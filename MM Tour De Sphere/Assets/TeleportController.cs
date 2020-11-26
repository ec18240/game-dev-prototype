using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportController : MonoBehaviour
{
    public GameObject passenger; // Object being teleported
    public Transform newPosition; //Coordinates of the new position

    // Start is called before the first frame update
    void Start()
    {
        initialise();
    }

    void initialise() //Set inital values
    {
        if(passenger == null)
        {
            passenger = GameObject.FindWithTag("Player");
        }
        if(newPosition == null)
        {
            newPosition.position = new Vector3(0.0f, 0.0f, 0.0f); // Default position
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            UnityEngine.Debug.Log("HMMMMM");
            passenger.transform.position = newPosition.position;
        }
    }
}
