using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPointController : MonoBehaviour
{
    public GameObject player; //PLAYER THAT TRIGGERS THE CHECKPOINT
    private string checkpointName; //NAME OF THE CHECKPOINT
    private bool checkActivated; //WHETHER THE CHECKPOINT IS ACTIVATED OR NOT
    private int checkpointLives; //AMOUNT OF LIVES A CHECKPOINT OFFERS

    public Transform respawn; //PLACE THE CHARACTER WILL RESPAWN
    private Vector3 respawnPoint; //COORDINATES OF RESPAWN

    private int checkPointID; //INDEX CHECKPOINT IS IN, INSIDE MASTERCHECKPOINT

    public MasterCheckpointController masterCheckpoint; //GETS CHECKPOINT MASTER



    // Start is called before the first frame update
    void Start()
    {
        initalise();
    }

    void initalise()
    {
        respawnPoint = respawn.position;
        this.checkpointLives = 3;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            print("Checkpoint Hit");
            masterCheckpoint.SetActiveCheckpoint(this.checkPointID);
            masterCheckpoint.SetCheckPointData(this.gameObject);
        }
    }

    void print(string text)
    {
        UnityEngine.Debug.Log(text);
    }

    public void RespawnPlayer()
    {
        SetLives(this.checkpointLives - 1); //LIVES DECREASED BY 1
        print("NUMBER OF LIVES: " + this.checkpointLives);
        print("CHECKPOINT ID: " + GetID());
        print(respawnPoint);
        player.GetComponent<PlayerController>().NewPosition(respawnPoint);
        //player.GetComponent<PlayerController>().NewRotation(new Quaternion(0.0f,0.0f,0.0f,1.0f));

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

}
