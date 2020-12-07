using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using UnityEngine;

public class SwitchBlockController : MonoBehaviour
{
    public GameObject[] childrenList; //LIST OF CHILDREN THE SWITCH IS RESPONSIBLE FOR
    public Material reset_material; //INACTIVE MATERIAL THAT THE CHILDREN RESET TO
    public GameObject player; //CURRENT PLAYER
    public GameObject GUIControl; //GameController
    private bool canActivate = false;

    private const string interactButton = "A";

    public static bool isUsed; //USED TO TELL IF CURRENT GAMEOBJECT IS IN USE, WILL IMPLEMENT LATER
    private float radius_distance; //DISTANCE PLAYER MUST BE FROM THE BLOCK

    // Start is called before the first frame update
    void Start()
    {
        radius_distance = 4.5f;
    }

    // Update is called once per frame
    void Update()
    {
        //IF THE PLAYER IS LESS THAN 2 (I guess CM) AWAY FROM THE BLOCK, DISPLAY A PROMPT (THIS IS BUGGED, WILL FIX SOON)
        if (Vector3.Distance(player.transform.position, getLocation()) < radius_distance)
        {
            //UnityEngine.Debug.Log("AYE");
            GUIControl.GetComponent<GameController>().SetPrompt("Press " + interactButton + " to interact with the block");
            canActivate = true;
        }
        else
        {
            //UnityEngine.Debug.Log(this.gameObject.name + ": not near");
            GUIControl.GetComponent<GameController>().SetPrompt("");
            canActivate = false;
        }

        //IF NOT ALREADY ACTIVATED, THE PLAYER IS ABLE TO PRESS A TO INTERACT WITH THE BLOCK
        // I MAY REDO THIS IMPLEMENTATION LIKE I HAVE DONE WITH MAGNET CONTROLLER.
        // MAKE A SEPARATE BLOCK CONTROLLER AND LEAVE THE PARTS THAT ALL BLOCKS HAVE IN THIS CODE

        if (canActivate == true && Input.GetKeyDown(KeyCode.A))
        {
            SoundManagerScript.PlaySound("SwitchBtn");
            GUIControl.GetComponent<GameController>().SetPrompt("");
            SwitchActivate();
            canActivate = false;
            gameObject.GetComponent<SwitchBlockController>().enabled = false;
            gameObject.GetComponent<BlockController>().enabled = false;
        }
    }
    //RETURNS LOCATION OF THE OBJECT
    public Vector3 getLocation()
    {
        return gameObject.transform.position;
    }

    /*WHEN THE SWITCH IS ACTIVATED
     * THEN ALL ENEMY BLOCKS THAT THE SWITCH CONTROLS BECOME WHITE AND INACTIVE
     */

    public void SwitchActivate()
    {
        for (int index = 0; index < childrenList.Length; index++)
        {
            BlockController bc_component = childrenList[index].GetComponent<BlockController>();
            Renderer ren_component = childrenList[index].GetComponent<MeshRenderer>();
            bc_component.switchMode(BlockController.Mode.Inactive);
            ren_component.material = reset_material;

        }
    }
}
