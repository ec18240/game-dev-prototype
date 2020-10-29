using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishController : MonoBehaviour
{
    public Material finish_material; //Material used to indicate that the player has completed the level

    public GameObject player; //Player
    private PlayerController player_settings; //PlayerController script from player
    private MeshRenderer player_material; //MeshRenderer component from the player

    private GameObject GUIControl; //GameUI
    private GameController GUIControl_settings; //GameController script from GameUI

    // Start is called before the first frame update
    void Start()
    {
        GUIControl = GameObject.FindWithTag("GameController"); //Get the GameUI
        player_settings = player.GetComponent<PlayerController>(); //Get PlayerController script (from the player)
        GUIControl_settings = GUIControl.GetComponent<GameController>(); //Get GameController script (from the GameUI)
        player_material = player.GetComponent<MeshRenderer>(); //Get MeshRenderer component (from the player)
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            GameFinish();
            this.gameObject.SetActive(false);
        }
    }
    
    void GameFinish() //Actions done once game ends
    {
        player_settings.SetSpeed(0); //Player stops
        player_material.material = finish_material; //Player is changed to the finish material
        GUIControl_settings.DisableText(true); //Disable all text
        GUIControl_settings.DisplayWin(); //Display WinText

    }


}
