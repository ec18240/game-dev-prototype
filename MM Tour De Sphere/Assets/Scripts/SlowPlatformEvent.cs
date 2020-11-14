using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowPlatformEvent : MonoBehaviour
{
    public TimeManagerController timer;
    public GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    void OnTriggerExit(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            timer.ChangeMotion();
        }
    }
}
