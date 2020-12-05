using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MasterCheckpointController : MonoBehaviour
{
    public GameObject[] checkpoints; //ARRAY OF CHECKPOINTS
    private GameObject current_checkpoint; //CURRENT CHECKPOINT WHICH IS ACTIVE
    private CheckPointController checkPointData; //SCRIPT FOR CURRENTCHECKPOINT
    private int currentCheckpointID; //ID OF THE CURRENT 

    private GameObject gameControl; //GAMECONTROLLER
    private GameController gameControlScript; //SCRIPT FOR GAMECONTROLLER

    // Start is called before the first frame update
    void Start()
    {
        initialise();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void initialise()
    {
        gameControl = GameObject.FindWithTag("GameController");
        gameControlScript = gameControl.GetComponent<GameController>();
        SetCheckpointID();
    }

    public void SetActiveCheckpoint(int checkpointIndex)
    {
        GameObject newCheckpoint = checkpoints[checkpointIndex];
        CheckPointController newCheckpointData = GetCheckpointData(newCheckpoint);
        gameControlScript.SetCheckPointData(newCheckpointData);

        if(current_checkpoint == null)
        {
            this.current_checkpoint = newCheckpoint;
            this.currentCheckpointID = newCheckpointData.GetID();
            SetCheckPointData(newCheckpoint);
        }
        else 
        {
            GetCurrentCheckpointData().SetActivate(false); //CURRENT (now old) CHECKPOINT IS SET TO FALSE
            newCheckpointData.SetActivate(true); //NEW CHECKPOINT IS SET TO TRUE
            this.current_checkpoint = newCheckpoint; //NEW CHECKPOINT IS THE CURRENT CHECKPOINT
            this.currentCheckpointID = newCheckpointData.GetID(); //THE ID OF THE NEW CHECKPOINT IS THE CURRENT ID
            SetCheckPointData(newCheckpoint); 

        }

    }

    void SetCheckpointID() //SETS THE INDEX OF THE CHECKPOINT AS THE ID
    {
        for(int index = 0; index<checkpoints.Length; index++)
        {
            checkpoints[index].GetComponent<CheckPointController>().SetID(index); //CHECKPOINT IS GIVEN AN INDEX
        }
    }

    int GetCheckpointID(int index) //GETS THE ID OF THE CHECKPOINT  (INDEX)
    {
        return checkpoints[index].GetComponent<CheckPointController>().GetID();
    }

    //KEEPS TRACK OF THE CHECKPOINTCONTROLLER SCRIPT, SPECIFIC TO A PARTICULAR CHECKPOINT
    public void SetCheckPointData(GameObject checkPoint)
    {
        checkPointData = checkPoint.GetComponent<CheckPointController>();
    }

    //RETRIEVES THE SCRIPT SPECIFIC FOR THE CURRENT CHECKPOINT
    CheckPointController GetCurrentCheckpointData()
    {
        return checkPointData;
    }

    CheckPointController GetCheckpointData(GameObject newCheckpointData)
    {
        return newCheckpointData.GetComponent<CheckPointController>();
    }
}
