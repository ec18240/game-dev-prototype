using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using UnityEngine;

public class HealthBlockController : MonoBehaviour
{
    public GameObject player; // this is the player;
    private GameObject GUIControl; //Gets the GUI;

    public const int max_attempts = 3;
    private int attempt_count;

    private Vector3 current_location; //Vector of current object
    private bool canActivate = false;

    // Start is called before the first frame update
    void Start()
    {
        attempt_count = max_attempts;
        GUIControl = GameObject.FindWithTag("GameController");
    }

    public Vector3 getLocation()
    {
        return gameObject.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        UnityEngine.Debug.Log("MORE THAN 2");
        if (Vector3.Distance(player.transform.position, getLocation()) < 2)
        {
            UnityEngine.Debug.Log("LESS THAN 2");
            if(attempt_count == 1)
            {
                GUIControl.GetComponent<GameController>().SetPrompt("Press A to interact with the HEALTH block (" + attempt_count + " heals remaining)");
            }
            else
            {
                GUIControl.GetComponent<GameController>().SetPrompt("Press A to interact with the HEALTH block (" + attempt_count + " heal remaining)");
            }
            canActivate = true;
        }
        else
        {
            GUIControl.GetComponent<GameController>().SetPrompt("");
            canActivate = false;
        }

        if (canActivate == true && Input.GetKeyDown(KeyCode.A))
        {
            if (attempt_count == 0)
            {
                GUIControl.GetComponent<GameController>().SetPrompt("");
                canActivate = false;
                gameObject.GetComponent<HealthBlockController>().enabled = false;
            }
            else
            {
                attempt_count--;
            }
        }

    }
}
