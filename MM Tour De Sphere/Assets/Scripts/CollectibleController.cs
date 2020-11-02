using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectibleController : MonoBehaviour
{
    private const int collectible_amount = 200; //YELLOW RUBIES WORTH 200 POINTS

    private GameObject GUIControl;
    private GameObject switchLeader;

    // Start is called before the first frame update
    void Start()
    {
        GUIControl = GameObject.FindWithTag("GameController");
        switchLeader = this.transform.parent.gameObject; //CHILD DECLARES ITS PARENT

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /* COLLECTIBLE (yellow points)
     * REPORT BACK TO THE PARENT. HOWEVER I MAY JUST MERGE THIS CODE WITH POINT CONTROLLER
     * AS POINT CONTROLLER SEEMS MUCH SIMPLER AND MUCH EASIER FOR THE LONG-TERM
     */

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            GUIControl.GetComponent<GameController>().AddPoints(collectible_amount);
            switchLeader.GetComponent<SwitchController>().Heartbeat();

        }
    }
}
