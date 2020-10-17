using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectibleController : MonoBehaviour
{
    private const int collectible_amount = 200;

    private GameObject GUIControl;
    private GameObject switchLeader;

    // Start is called before the first frame update
    void Start()
    {
        GUIControl = GameObject.FindWithTag("GameController");
        switchLeader = this.transform.parent.gameObject; //The child declares its parent

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            GUIControl.GetComponent<GameController>().addPoints(collectible_amount);
            switchLeader.GetComponent<SwitchController>().Heartbeat();

        }
    }
}
