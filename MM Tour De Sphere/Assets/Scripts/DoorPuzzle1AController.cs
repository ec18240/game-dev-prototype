using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorPuzzle1AController : MonoBehaviour
{
    private int num_of_switches;
    private int switches_on = 0;

    private GameObject[] puzzle_switches; //List of all child switches
    public GameObject puzzle_switch; //Template switch (to retrieve the switch's tag)

    public GameObject puzzle_door; //Door puzzle activates

    private string switchTag; //Tag of the switches that are part of the puzzle (they have the same tag)



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

    public void OnSwitch()
    {
        switches_on++;
    }

    public void OffSwitch()
    {
        switches_on--;
    }

    void switchActivate()
    {
        if(switches_on == num_of_switches)
        {
            puzzle_door.GetComponent<DoorController>().OpenDoor();
            for(int index = 0; index<puzzle_switches.Length; index++)
            {
                puzzle_switches[index].GetComponent<DoorPuzzle1A>().DisableSwitch();
            }
            
        }
    }
}
