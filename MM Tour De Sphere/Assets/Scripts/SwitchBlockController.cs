using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchBlockController : MonoBehaviour
{
    public GameObject[] childrenList; //LIST OF CHILDREN THE SWITCH IS RESPONSIBLE FOR
    public Material reset_material; //INACTIVE MATERIAL THAT THE CHILDREN RESET TO
    // Start is called before the first frame update
    void Start()
    {
        //reset_material = (Material)Resources.Load("Assets/Materials/NormalObject");
    }

    // Update is called once per frame
    void Update()
    {
 
    }

    /*WHEN THE SWITCH IS ACTIVATED
     * THEN ALL ENEMY BLOCKS THAT THE SWITCH CONTROLS BECOME WHITE AND INACTIVE
     */

    public void SwitchActivate()
    {
        for(int index = 0; index<childrenList.Length; index++)
        {
            BlockController bc_component = childrenList[index].GetComponent<BlockController>();
            Renderer ren_component = childrenList[index].GetComponent<MeshRenderer>();
            bc_component.switchMode(BlockController.Mode.Inactive);
            ren_component.material = reset_material;

        }
    }
}
