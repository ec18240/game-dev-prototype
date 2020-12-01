using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorPuzzle1AController : MonoBehaviour
{

    /*CONCEPT (Ashley):
     * Each switch has an independent timer, when the player
     * interacts with blocks, they contact this script and call the OnSwitch
     * method in order to increase the amount of switches on.
     * When their timer decreases, the switches are reset and they call the off switch
     * method to notify the script that they are off
     * 
     * When all the switches are on as in, when the number of children (switches)
     * is equal to the amount of switches on, the door activates and the player can proceed
     */


    private int num_of_switches;
    private int switches_on = 0;

    private GameObject[] puzzle_switches; //LIST OF ALL CHILD SWITCHES
    public GameObject puzzle_switch; //TEMPLATE SWITCH (to retrieve the switch's tag)

    public GameObject puzzle_door; //DOOR THAT'S TRAPPED BY THE PUZZLE IS ACTIVATED

    private string switchTag; //TAG OF THE SWITCHES THAT ARE PART OF THE PUZZLE (they have the same tag)



    // Start is called before the first frame update
    void Start()
    {
        switchTag = puzzle_switch.tag;
        puzzle_switches = GameObject.FindGameObjectsWithTag(switchTag);
        num_of_switches = puzzle_switches.Length;
    }

    // Update is called once per frame
    void Update()
    {
        switchActivate();
    }

    //AMOUNT OF SWITCHES INCREASE/DECREASES

    public void OnSwitch()
    {
        switches_on++;
    }

    public void OffSwitch()
    {
        switches_on--;
    }

    //WHEN ALL SWITCHES ARE ON, PROCEED!

    void switchActivate()
    {
        if(switches_on == num_of_switches)
        {
            //puzzle_door.GetComponent<DoorController().OpenDoor();
            puzzle_door.GetComponent<DoorPuzzle3A>().OpenDoor();
            for(int index = 0; index<puzzle_switches.Length; index++)
            {
                puzzle_switches[index].GetComponent<DoorPuzzle1A>().DisableSwitch();
            }
            
        }
    }
}
