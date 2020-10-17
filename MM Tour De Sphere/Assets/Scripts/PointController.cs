using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointController : MonoBehaviour
{
    private int point_amount = 100;
    private GameObject GUIControl;
    public GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        GUIControl = GameObject.FindWithTag("GameController");
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            GUIControl.GetComponent<GameController>().addPoints(point_amount);
            Destroy(this.gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
