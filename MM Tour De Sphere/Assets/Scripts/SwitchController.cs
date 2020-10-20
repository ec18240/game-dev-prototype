using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchController : MonoBehaviour
{
    public GameObject collect;
    public GameObject GateSwitch; //CurrentGate
    public GameObject Gate; //Gate the switch is responsible for

    private string switchTag;

    private int switchCount;
    private int switchCountMax;
    private GameObject[] switchList;

    private Type block_type;

    enum Type
    {
        Passive, //Does not self destruct upon collision/completion of a task
        Aggressive //Self-destructs upon collision/completion of a task
    }

    // Start is called before the first frame update
    void Start()
    {
        switchTag = collect.tag;
        switchList = GameObject.FindGameObjectsWithTag(switchTag); //Gets list of all collectibles responsible for unlocking the gate
        switchCountMax = switchList.Length; //Finds amount of collectibles under switchTag's tag
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

    public void Heartbeat()
    {
        if(block_type == Type.Aggressive)
        {
            switchCount++;
        }
        if(switchCount == switchCountMax)
        {
            Destroy(Gate);
        }
            
    }

}
