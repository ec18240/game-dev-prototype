using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchController : MonoBehaviour
{
    public GameObject collect;
    public GameObject GateSwitch; //CURRENT GATE
    public GameObject Gate; //GATE THAT THE SWITCH IS REPSONSIBLE FOR

    private string switchTag; //TAG OF THE OBJECT

    private int switchCount;
    private int switchCountMax;
    private GameObject[] switchList;

    private Type block_type;

    enum Type
    {
        Passive, //OBJECT DOES NOT SELF_DESTRUCT UPON THE COMPLETION OF A TASK
        Aggressive //SELF-DESTRUCTS UPON THE COMPLETION OF A TASK
    }

    // Start is called before the first frame update
    void Start()
    {
        switchTag = collect.tag;
        switchList = GameObject.FindGameObjectsWithTag(switchTag); //GETS A LIST OF ALL COLLECTIBLES RESPONSIBLE FOR OPENING THE GATE
        switchCountMax = switchList.Length; //FINDS AMOUNT OF COLLECTIBLES UNDER SWITCHTAG'S TAG
        setType(); 
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    void setType()
    {
        if(gameObject.tag == "SwitchBlock")
        {
            block_type = Type.Passive;
        }
        else
        {
            block_type = Type.Aggressive;
        }
    }


    //CHECKS AND REPORTS THE STATUS OF THE SWITCH CONTROLLER
    //I HAVE SINCE LEARNED A BETTER IMPLEMENTATION FROM WRITING THE DOOR PUZZLE SCRIPTS AND THE SWITCH BLOCK CONTROLLER SCRIPTS
    // WILL REDO THIS IMPLEMENTATION IF THERE IS TIME (Ashley)
    public void Heartbeat()
    {
        if(block_type == Type.Aggressive)
        {
            switchCount++;
        }
        if(switchCount == switchCountMax)
        {
            SoundManagerScript.PlaySound("DoorsOpen");
            Destroy(Gate);
        }
            
    }

}
