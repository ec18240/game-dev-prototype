using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroEvent : MonoBehaviour
{
    private GameObject player; 
    private ScriptEventManager scriptControl;
    private PlayerController playerControl;
    private float player_speed;
    private TimeManagerController timer;
    private const int offset = 125;
    public Transform trigger;

    // Start is called before the first frame update
    void Start()
    {
        initialise();
        ScriptEnter();
    }

    // Update is called once per frame
    void Update()
    {
        ScriptStay();
    }

    void initialise()
    {
        if(player == null)
        {
            scriptControl = this.gameObject.GetComponent<ScriptEventManager>();
            player = scriptControl.getPlayer();
            playerControl = player.GetComponent<PlayerController>();
            timer = scriptControl.getTimeManager();
            
        }

    }

    void ScriptEnter()
    {
        playerControl.SetPlayerControl(false); //Player loses control
        playerControl.SetSpeed(400);
        this.player_speed = playerControl.getSpeed() - offset;
    }

    void ScriptStay()
    {
        float delta = 4.0f * Time.deltaTime;
        player.transform.position = Vector3.MoveTowards(player.transform.position, trigger.position, delta);
        if(player.transform.position == trigger.position)
        {
            playerControl.SetPlayerControl(true); //Control is given back to the player just before the script ends
            Vector3 movement = new Vector3(0.0f, 0.0f, this.player_speed);
            player.GetComponent<Rigidbody>().AddForce(movement * this.player_speed * Time.fixedDeltaTime);
            ScriptLeave();
        }

    }

    void ScriptLeave()
    {
        timer.ChangeMotion();
        scriptControl.Disable();
        this.gameObject.GetComponent<IntroEvent>().enabled = false;
    }
}
