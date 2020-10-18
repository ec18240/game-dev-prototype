using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchBlockController : MonoBehaviour
{
    public GameObject[] childrenList;
    public Material reset_material;
    // Start is called before the first frame update
    void Start()
    {
        //reset_material = (Material)Resources.Load("Assets/Materials/NormalObject");
    }

    // Update is called once per frame
    void Update()
    {
 
    }

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
