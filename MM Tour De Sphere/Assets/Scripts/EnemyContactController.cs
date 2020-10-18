﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyContactController : MonoBehaviour
{
    public GameObject player;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            player.GetComponent<PlayerController>().TakeDamage();
        }
    }

    public void setInactive()
    {
        this.gameObject.GetComponent<EnemyContactController>().enabled = false;
    }

}
