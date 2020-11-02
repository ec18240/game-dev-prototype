using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameObject player;
    private GameObject GUIControl;

    private float speed;
    private float speed_boost;
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
    private float health_hearts = 3.0f; //Player by default has 3 hearts
    private const float red_damage = 1.0f; //When the player gets damaged, he loses 1 health


    //IN A SCRIPTED EVENT, YOU DON'T WANT THE PLAYER TO HAVE CONTROL OF THE CHARACTER-MOVEMENT
    private bool playerInControl = true;

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

    // Start is called before the first frame update
    void Start()
    {
        speed = 450.0f;
        jump = new Vector3(0.0f,jumpForce, 0.0f);
        state = Form.Normal;
        playerInfo = player.transform;
        GUIControl = GameObject.FindWithTag("GameController");
        
    }


    //SETS WHETHER THE PLAYER IS IN-CONTROL OR NOT. FALSE IF A SCRIPTED EVENT OCCURS
    public void SetPlayerControl(bool value)
    {
        playerInControl = value;
    }

    void FixedUpdate()
    {
        if(playerInControl == true)
        {
            float horAxis = Input.GetAxis("Horizontal");
            float verAxis = Input.GetAxis("Vertical");
            Vector3 movement = new Vector3(horAxis, 0.0f, verAxis);

            GetComponent<Rigidbody>().AddForce(movement * speed * Time.fixedDeltaTime);
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
        }

        if(other.gameObject.tag == "HealthBlock")
        {
            health_hearts = 3.0f;
            ReportToGUI();
        }
    }
    /*
     * WHEN THE PLAYER COLLIDES WITH AN ENEMY BLOCK
     * THEY TAKE DAMAGE AND THEN THE HEALTH IS REPORTED
     * BACK TO THE GUI WHICH IS CONTROLLED BY THE GAMECONTROLLER WHICH IS VARIABLE GUICONTROL
     */

    public void TakeDamage()
    {
        UnityEngine.Debug.Log(canDamage);
        health_hearts -= red_damage;
        canDamage = false;
        damageCoolDown = damageCoolDown_Reset;
        UnityEngine.Debug.Log("TAKE DAMAGE");
        ReportToGUI();
    }
    /* WHEN THE CHARACTER STAYS COLLIDED
     * WITH THE ENEMY BLOCKS, THEY CONTINUE TO LOSE DAMAGE
     */

    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "EnemyBlock" && canDamage == true)
        {
            if(other.gameObject.GetComponent<BlockController>().getMode() == BlockController.Mode.Enemy)
            {
                TakeDamage();
            }
            
        }
    }
    
    /*
     * THE GUICONTROL VARIABLE WHICH HAS THE GAMECONTROLLER 
     * REPORTS BACK TO THE GUI
     */

    void ReportToGUI()
    {
        GUIControl.GetComponent<GameController>().displayText();
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
            player.transform.localScale = new Vector3(playerInfo.localScale.x * JFSmallScale, playerInfo.localScale.y * JFSmallScale, playerInfo.localScale.y * JFSmallScale);
            jumpForce *= JFSmallScale;
            //player.GetComponent<Halo>().size() *= JFSmallScale;
            state = Form.Small;
            
        }
        else if(state == Form.Small) //CHARACTER TURNS BIG
        {
            player.transform.localScale = new Vector3(playerInfo.localScale.x * JFNormalScale, playerInfo.localScale.y * JFNormalScale, playerInfo.localScale.y * JFNormalScale);
            jumpForce *= JFSmallScale;
            //player.GetComponent<Halo>().size *= JFNormalScale;
            state = Form.Normal;
        }
    }

    // Update is called once per frame
    void Update()
    {
        //IF THE USER US GROUNDED AND PRESSES THE SPACE_BAR, THEY CAN JUMP
        if(Input.GetKeyDown(KeyCode.Space) && playerGrounded == true)
        {
            player.GetComponent<Rigidbody>().velocity = player.GetComponent<Rigidbody>().velocity + jump;
            playerGrounded = false;
        }
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            toggleSize();
        }
        if(damageCoolDown <= 0)
        {
            canDamage = true;
        }
        jumpCoolDown -= Time.deltaTime;
        damageCoolDown -= Time.deltaTime;
        

    }

    /*
     * SETS SPEED
     */

    public void SetSpeed(float speed)
    {
        this.speed = speed;
    }

    /*
     * GETS HEALTH 
     */

    public float getHealth()
    {
        return health_hearts;
    }

}
