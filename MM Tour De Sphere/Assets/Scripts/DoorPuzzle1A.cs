using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorPuzzle1A : MonoBehaviour
{
    public Material switch_on_material; //MATERIAL WHEN SWITCH IS ON
    public Material originalMaterial;

    private GameObject puzzleControl;


    private const float switch_on_timer = 3.5f;
    private float switch_timer;
    private bool timer_active = false;

    private bool canTurnOn = true;

    // Start is called before the first frame update
    void Start()
    {
        puzzleControl = this.transform.parent.gameObject; //THE PUZZLE SWITCH GETS ITS PARENT, THE DOOR THAT OWNS THE PUZZLE CONTROLELR
        originalMaterial = this.GetComponent<MeshRenderer>().material; //KEEPS A RECORD OF THE SWITCHES ORIGINAL MATERIAL
        switch_timer = switch_on_timer; //TIMER OF HOW LONG THE SWITCH REMAINS ON
        
    }

    // Update is called once per frame
    void Update()
    {
        switch_timer -= Time.deltaTime;
        switchTimer();
    }


    //IF THE PLAYER HITS THE SWITCH, TURN ON
    void OnTriggerEnter(Collider other)
    {
        UnityEngine.Debug.Log("OBJECT HIT ");
        if (other.gameObject.tag == "Player")
        {
            SoundManagerScript.PlaySound("SwitchBtn");
            UnityEngine.Debug.Log("HMMMMMM ");
            timer_active = true;
            if(canTurnOn == true)
            {
                UnityEngine.Debug.Log("SWITCH ON ");
                puzzleControl.GetComponent<DoorPuzzle1AController>().OnSwitch();
                canTurnOn = false;
            }
            resetTimer();
            
        }
    }

    /*SETS WHETHER THE SWITCH IS ON OR NOT
     * IF IT IS ON, THE OBJECT SWITCHES MATERIAL
     */

    void switchTimer()
    {
        if(switch_timer < 0)
        {
            canTurnOn = true;
            resetTimer();
            setMaterial(originalMaterial);
            if(timer_active == true)
            {
                puzzleControl.GetComponent<DoorPuzzle1AController>().OffSwitch();
            }
            timer_active = false;
        }
        if(switch_timer > 0 && timer_active == true)
        {
            setMaterial(switch_on_material);
        }
    }

    void resetTimer() //RESETS TIMER WHEN CALLED
    {
        switch_timer = switch_on_timer;
    }

    void setMaterial(Material object_material)
    {
        this.gameObject.GetComponent<MeshRenderer>().material = object_material;
    }


    //WHEN ALL SWITCHES ARE ACTIVE, USE THIS
    public void DisableSwitch()
    {
        timer_active = false;
        switch_timer = switch_on_timer;
        setMaterial(switch_on_material);
        this.GetComponent<DoorPuzzle1A>().enabled = false;
    }



    
}
