﻿using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameObject player;
    private Rigidbody rigidBodyScript;
    private GameObject GUIControl;
    private GameController gameControlScript;

    private const float speedReset = 450.0f;
    public float speed;
    private float speedBoost;
    private Transform playerInfo;

    private Form state;

    private bool playerGrounded = true;
    private bool canDamage = true;

    //PLAYER JUMP VARIABLES
    private Vector3 jump;
    private float jumpForce = 5.5f; //HOW HIGH THHE PLAYER CAN JUMP
    private float JFSmallScale = 0.5f; //Jump multiplier when player goes from normal size to small
    private float JFNormalScale = 2.0f; //Jump multiplier when player goes from small to normal
    private float jumpCoolDown = 1.0f;
    private const float jumpCoolDown_Reset = 1.0f; //DEFAULT JUMP COOLDOWN TIMER



    //PLAYER DAMAGE COOLDOWN VARIABLES
    private float damageCoolDown = 0.5f; //TIME BEFORE PLAYER CAN TAKE DAMAGE AGAIN
    private const float damageCoolDown_Reset = 0.5f;


    //PLAYER HEALTH/DAMAGE VARIABLES
    private float health_hearts = 5.0f; //Player by default has 5 hearts
    private const float health_hearts_reset = 5.0f; //Max health player has (for reset)
    private const float red_damage = 1.0f; //When the player gets damaged, he loses 1 health


    //IN A SCRIPTED EVENT, YOU DON'T WANT THE PLAYER TO HAVE CONTROL OF THE CHARACTER-MOVEMENT
    public bool playerInControl;

    private bool strengthMode;

    /*  PLANNED ON MAKING IT SO WHEN THE MUMMY COLLIDES WITH A WALL
     *  THEY BOUNCE BACK
     *  I MAY ADD THIS LATER IN THE GAME AND MAKE ANOTHER SCRIPT CALLED
     *  'BOUNCYWALLCONTROLLER'
     *    
     *  private float wallPushLeft;
        private float wallPushRight;
        private float wallPushUp;
        private float wallPushDown; */


    /* I set some of the methods to public
     * So that other major gameobject scripts like gamecontroller
     * could interact with the player (Ashley)
     */

    enum Form
    {
        Normal,
        Small
    }

    void Awake()
    {
        rigidBodyScript = GetComponent<Rigidbody>();
        GUIControl = GameObject.FindWithTag("GameController");
        gameControlScript = GUIControl.GetComponent<GameController>();
    }

    // Start is called before the first frame update
    void Start()
    {
        initialise();
        speedBoost = 1.0f;
        jump = new Vector3(0.0f,jumpForce, 0.0f);
        state = Form.Normal;
        playerInfo = player.transform;
        strengthMode = false;
        
    }


    //SETS WHETHER THE PLAYER IS IN-CONTROL OR NOT. FALSE IF A SCRIPTED EVENT OCCURS
    public void SetPlayerControl(bool value)
    {
        playerInControl = value;
    }

    void initialise()
    {
        if(speed == 0)
        {
            this.speed = speedReset;
        }
    }

    //Takes player to a new position without Teleport Controller
    public void NewPosition(Vector3 newPosition)
    {
        this.gameObject.transform.position = newPosition;
    }

    //Takes player to a new position without Teleport Controller
    public void NewRotation(Quaternion newRotation)
    {
        this.gameObject.transform.rotation = newRotation;
    }

    void FixedUpdate()
    {
        if(playerInControl == true)
        {
            float horAxis = Input.GetAxis("Horizontal");
            float verAxis = Input.GetAxis("Vertical");
            Vector3 movement = new Vector3(horAxis, 0.0f, verAxis);

            rigidBodyScript.AddForce(movement * speed * Time.fixedDeltaTime);
        }
        
        
    }

    /*WILL UPDATE THE FIRST IF STATEMENT AS I FOUND AN EASIER WAY
     * 
     * IF THE PLAYER COLLIDES WITH A HEALTH BLOCK THEY ARE HEALED
     * WHEN THEY COLLIDE WITH AN ENEMY BLOCK, THEY GET HURT
     * 
     * WHEN THE USER COLLECTS ALL THE SWITCH COLLECTIBLES IN 1A THE COLLECTIBLES DIE.
     * HOWEVER I LEARNED THAT IT WOULD BE MUCH EASIER TO PUT THIS IN COLLECTIBLECONTROLLER
     * LIKE I DID WHEN I WROTE POINTCONTROLLER
     */

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "CollectibleSwitchArea1A")
        {
            other.gameObject.SetActive(false);
        }
        if (other.gameObject.tag == "EnemyBlock" && canDamage == true)
        {
            if (other.gameObject.GetComponent<BlockController>().getMode() == BlockController.Mode.Enemy)
            {
                TakeDamage();
            }
            else
            {
                other.gameObject.GetComponent<BlockController>().SetDisable();
            }
        }

        if(other.gameObject.tag == "HealthBlock")
        {
            SoundManagerScript.PlaySound("Health");
            health_hearts = health_hearts_reset;
            ReportToGUI();
        }
    }
    /*
     * WHEN THE PLAYER COLLIDES WITH AN ENEMY BLOCK
     * THEY TAKE DAMAGE AND THEN THE HEALTH IS REPORTED
     * BACK TO THE GUI WHICH IS CONTROLLED BY THE GAMECONTROLSCRIPT
     */

    public void TakeDamage()
    {
        health_hearts -= red_damage;
        canDamage = false;
        damageCoolDown = damageCoolDown_Reset;
        SoundManagerScript.PlaySound("TakingDamage");
        ReportToGUI();
    }

    /* WHEN THE CHARACTER STAYS COLLIDED
     * WITH THE ENEMY BLOCKS, THEY CONTINUE TO LOSE DAMAGE
     */

    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "EnemyBlock" && canDamage == true)
        {
            if (other.gameObject.GetComponent<BlockController>().getMode() == BlockController.Mode.Enemy && getStrength() == false)
            {
                TakeDamage();
            }
            
        }
    }
    
    /*
     * GAMECONTROLSCRIPT REPORTS BACK TO THE GUI
     */

    void ReportToGUI()
    {
        gameControlScript.displayText();
    }

    /*PUT A JUMP COOLDOWN BECAUSE THE PLAYER
     * COULD JUMP MID-AIR AND COULD ALSO WALL JUMP
     * I WILL UPDATE THIS METHOD
     * 
     * I BELIEVE THIS IF STATEMENT IS ACTUALLY INCORRECT BUT I WILL PATCH IT IN THE FINAL BUILD OF THE GAME
     */ 

    void OnCollisionStay()
    {
        if(jumpCoolDown <= 0)
        {
            playerGrounded = true;
            jumpCoolDown = jumpCoolDown_Reset;
        }
        
    }
    
    void toggleSize()
    {

        if(state == Form.Normal) // CHARACTER TURNS SMALL
        {
            SoundManagerScript.PlaySound("Shrinking");
            player.transform.localScale = new Vector3(playerInfo.localScale.x * JFSmallScale, playerInfo.localScale.y * JFSmallScale, playerInfo.localScale.y * JFSmallScale);
            jumpForce *= JFSmallScale;
            state = Form.Small;
            
        }
        else if(state == Form.Small) //CHARACTER TURNS BIG
        {
            SoundManagerScript.PlaySound("Growing");
            player.transform.localScale = new Vector3(playerInfo.localScale.x * JFNormalScale, playerInfo.localScale.y * JFNormalScale, playerInfo.localScale.y * JFNormalScale);
            jumpForce *= JFSmallScale;
            state = Form.Normal;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(playerInControl == true)
        {
            //IF THE USER US GROUNDED AND PRESSES THE SPACE_BAR, THEY CAN JUMP
            if (Input.GetKeyDown(KeyCode.Space) && playerGrounded == true)
            {
                rigidBodyScript.velocity = rigidBodyScript.velocity + jump;
                SoundManagerScript.PlaySound("Jump");
                playerGrounded = false;
            }
            if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                toggleSize();
            }
            if (damageCoolDown <= 0)
            {
                canDamage = true;
            }
            jumpCoolDown -= Time.deltaTime;
            damageCoolDown -= Time.deltaTime;
        }
        
        

    }

    /*
     * SETS SPEED
     */

    public void SetSpeed(float speed)
    {
        this.speed = speed;
    }

    public float getSpeed()
    {
        return this.speed;
    }

    public void SetBoost(float speedBoost)
    {
        this.speedBoost = speedBoost;
        this.speed = speed * this.speedBoost;
    }


    /*
     * GETS HEALTH 
     */

    public float getHealth()
    {
        return health_hearts;
    }

    public void SetHealth(float health)
    {
        this.health_hearts = health;
    }

    public void SetStrength(bool strength_value)
    {
        this.strengthMode = strength_value;
    }

    public bool getStrength()
    {
        return this.strengthMode;
    }

}
