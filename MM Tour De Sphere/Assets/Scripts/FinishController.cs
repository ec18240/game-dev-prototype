using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishController : MonoBehaviour
{
    public Material finish_material; //MATERIAL USED TO INDICATE THAT THE PLAYER HAS COMPLETED THE LEVEL. PLAYER SWITCHES TO YELLOW

    public GameObject player; //PLAYER GAMEOBJECT
    private PlayerController player_settings; //PLAYERCONTROLLER SCRIPT FROM THE PLAYER
    private MeshRenderer player_material; //MESHRENDERER SCRIPT FROM PLAYER

    private GameObject GUIControl; //GAMEUI
    private GameController GUIControl_settings; //GAMECONTROLLER SCRIPT FROM GAMEGUI

    // Start is called before the first frame update
    /*
     * THIS IMPLEMENTATION SEEMS BETTER
     * I MAY USE THIS FOR THE OTHER SCRIPTS
     */ 
    void Start()
    {
        GUIControl = GameObject.FindWithTag("GameController"); //GET THE GAME UI
        player_settings = player.GetComponent<PlayerController>(); //ET THE PLAYER CONTROLLER SCRIPT (from the player)
        GUIControl_settings = GUIControl.GetComponent<GameController>(); //GET THE GAMECONTROLLER SCRIPT (from the GameUI)
        player_material = player.GetComponent<MeshRenderer>(); //GET THE MESHRENDERER COMPONENT (from the player)
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            GameFinish();
            this.gameObject.SetActive(false);
        }
    }
    
    void GameFinish() //ACTIONS PERFORMED ONCE THE GAME ENDS
    {
        player_settings.SetSpeed(0); //PLAYER STOPS AND CAN'T MOVE
        player_material.material = finish_material; //PLAYER IS CHANGED TO THE WINNER MATERIAL (FINISH MATERIAL)
        GUIControl_settings.DisableText(true); //DISABLE ALL TEXT 
        GUIControl_settings.DisplayWin(); //DISPLAY WIN TEXT

    }


}
