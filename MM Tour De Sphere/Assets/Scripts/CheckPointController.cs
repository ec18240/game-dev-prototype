using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPointController : MonoBehaviour
{
    public GameObject player; //PLAYER THAT TRIGGERS THE CHECKPOINT
    public string checkpointName; //NAME OF THE CHECKPOINT
    private bool checkActivated; //WHETHER THE CHECKPOINT IS ACTIVATED OR NOT
    private int checkpointLives; //AMOUNT OF LIVES A CHECKPOINT OFFERS

    public GameObject checkpointBulb; //ACTIVATES WHEN THE CHECKPOINT IS ACTIVE
    public Material [] bulbMaterial; //MATERIALS ON THE BULB
    public Transform respawn; //PLACE THE CHARACTER WILL RESPAWN
    private Vector3 respawnPoint; //COORDINATES OF RESPAWN

    private int checkPointID; //INDEX CHECKPOINT IS IN, INSIDE MASTERCHECKPOINT

    public MasterCheckpointController masterCheckpoint; //GETS CHECKPOINT MASTER


    void Awake()
    {
        initalise();
    }

    void initalise()
    {
        respawnPoint = respawn.position;
        this.checkpointLives = 3;
    }

    void SwitchBulb(bool bulbswitch)
    {
        if(bulbswitch == true)
        {
            checkpointBulb.GetComponent<MeshRenderer>().material = bulbMaterial[1]; //BLUE MATERIAL
        }
        else
        {
            checkpointBulb.GetComponent<MeshRenderer>().material = bulbMaterial[0]; //DEAD OBJECT MATERIAL (BLACK)
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            SoundManagerScript.PlaySound("checkpoint");
            masterCheckpoint.SetActiveCheckpoint(this.checkPointID);
            masterCheckpoint.SetCheckPointData(this.gameObject);
        }
    }

    public void RespawnPlayer()
    {
        SetLives(this.checkpointLives - 1); //LIVES DECREASED BY 1
        player.GetComponent<PlayerController>().NewPosition(this.respawnPoint);
        player.GetComponent<PlayerController>().NewRotation(new Quaternion(0.0f,0.0f,0.0f,1.0f));

    }

    //SETS THE ID OF THE CHECKPOINT WHICH IS THE INDEX IN THE MASTER CHECKPOINT ARRAY

    public void SetID(int index)
    {
        this.checkPointID = index;
    }

    //GETS THE ID OF THE CHECKPOINT WHICH IS THE INDEX IN THE MASTER CHECKPOINT ARRAY

    public int GetID()
    {
        return this.checkPointID;
    }

    //SETS THE CHECKPOINT TO ACTIVE OR NOT (ON OR OFF) ONLY ONE CHECKPOINT ACTIVE AT A TIME
    public void SetActivate(bool activation)
    {
        this.checkActivated = activation;
        SwitchBulb(activation);
    }

    //GETS THE ACTIVATION BOOL
    public bool GetActivate()
    {
        return this.checkActivated;
    }

    //GETS THE CHECKPOINT LIVES AMOUNT
    public int GetLives()
    {
        return checkpointLives;
    }

    //SETS THE AMOUNT OF LIVES PRESENT AT A CHECKPOINT
    void SetLives(int lives)
    {
        this.checkpointLives = lives;
    }

    public string GetName()
    {
        return this.checkpointName;
    }

}
