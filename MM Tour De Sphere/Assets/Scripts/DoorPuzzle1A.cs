using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorPuzzle1A : MonoBehaviour
{
    public Material switch_on_material; //material when switch is on
    public Material originalMaterial;

    private GameObject puzzleControl;


    private const float switch_on_timer = 3.5f;
    private float switch_timer;
    private bool timer_active = false;

    private bool canTurnOn = true;

    // Start is called before the first frame update
    void Start()
    {
        puzzleControl = this.transform.parent.gameObject; //Puzzle switch gets its parent, the parent owns the puzzle controller
        originalMaterial = this.GetComponent<MeshRenderer>().material; //Keeps record of the objects original material
        switch_timer = switch_on_timer; //Timer is set to the duration of the on switch
        
    }

    // Update is called once per frame
    void Update()
    {
        switch_timer -= Time.deltaTime;
        switchTimer();
    }

    void OnTriggerEnter(Collider other)
    {
        UnityEngine.Debug.Log("OBJECT HIT ");
        if (other.gameObject.tag == "Player")
        {
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

    void resetTimer() //resets the timer when called
    {
        switch_timer = switch_on_timer;
    }

    void setMaterial(Material object_material)
    {
        this.gameObject.GetComponent<MeshRenderer>().material = object_material;
    }

    public void DisableSwitch()
    {
        timer_active = false;
        switch_timer = switch_on_timer;
        setMaterial(switch_on_material);
        this.GetComponent<DoorPuzzle1A>().enabled = false;
    }



    
}
