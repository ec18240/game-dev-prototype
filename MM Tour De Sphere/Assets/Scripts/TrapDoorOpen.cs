using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapDoorOpen : MonoBehaviour
{
    public GameObject TrapDoor;
     void OnTriggerEnter()
    {
        TrapDoor.GetComponent<Animation>().Play("TrapDoorAnimation");

    }
}
