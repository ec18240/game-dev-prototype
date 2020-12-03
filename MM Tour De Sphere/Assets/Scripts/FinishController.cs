using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinishController : MonoBehaviour
{
    public Material finish_material; //MATERIAL USED TO INDICATE THAT THE PLAYER HAS COMPLETED THE LEVEL. PLAYER SWITCHES TO YELLOW

    public GameObject player; //PLAYER GAMEOBJECT
    private PlayerController player_settings; //PLAYERCONTROLLER SCRIPT FROM THE PLAYER
    private MeshRenderer player_material; //MESHRENDERER SCRIPT FROM PLAYER

    private GameObject GUIControl; //GAMEUI
    private GameController GUIControl_settings; //GAMECONTROLLER SCRIPT FROM GAMEGUI

    public bool nextScene; //WHETHER ANOTHER SCENE WILL BE LOADED
    private bool countdown; //TRIGGER FOR WHEN COUNTDOWN SHOULD BEGIN
    public int sceneIndex; //INDEX FOR THE SCENE
    public float nextSceneDelay; //TIME UNTIL NEXT SCENE BEGINS

    private bool isFinished; //Checks if the level is finished

    // Start is called before the first frame update
    /*
     * THIS IMPLEMENTATION SEEMS BETTER
     * I MAY USE THIS FOR THE OTHER SCRIPTS
     */ 
    void Start()
    {
        countdown = false;
        GUIControl = GameObject.FindWithTag("GameController"); //GET THE GAME UI
        player_settings = player.GetComponent<PlayerController>(); //ET THE PLAYER CONTROLLER SCRIPT (from the player)
        GUIControl_settings = GUIControl.GetComponent<GameController>(); //GET THE GAMECONTROLLER SCRIPT (from the GameUI)
        player_material = player.GetComponent<MeshRenderer>(); //GET THE MESHRENDERER COMPONENT (from the player)
    }
    
    void Update()
    {
        if (countdown)
        {
            NextScene();
        } 

    }

    void NextScene()
    {
        nextSceneDelay -= Time.deltaTime;
        UnityEngine.Debug.Log("NEW SCENE SOON: " + nextSceneDelay);
        if (nextSceneDelay <= 0.0f)
        {
            UnityEngine.Debug.Log("NEW SCENE now: " + nextSceneDelay);
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + sceneIndex);
        }
        
    }

     

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            GameFinish();
            this.gameObject.layer = 8;
            //this.gameObject.SetActive(false);
        }
    }
    
    void GameFinish() //ACTIONS PERFORMED ONCE THE GAME ENDS
    {
        player_settings.SetSpeed(0); //PLAYER STOPS AND CAN'T MOVE
        player_material.material = finish_material; //PLAYER IS CHANGED TO THE WINNER MATERIAL (FINISH MATERIAL)
        GUIControl_settings.DisableText(true); //DISABLE ALL TEXT 
        GUIControl_settings.DisplayWin(); //DISPLAY WIN TEXT
        setFinish(true); //FINISHED NOTIFICATION
        countdown = true; //COUNTDOWN BEGINS

    }

    private void setFinish(bool finish_state)
    {
        this.isFinished = finish_state;
    }

    public bool getFinish() //NOTIFIES GAME THAT THE GAME IS FINISHED
    {
        return isFinished;
    }


}
