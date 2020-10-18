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

    private Vector3 jump;
    private float jumpForce = 6.0f;
    private float JFSmallScale = 0.5f;
    private float JFNormalScale = 2.0f;
    private float jumpCoolDown = 1.0f;
    private const float jumpCoolDown_Reset = 1.0f;

    private float damageCoolDown = 0.5f;
    private const float damageCoolDown_Reset = 0.5f;


    private float health_hearts = 3.0f;
    private const float red_damage = 1.0f;

    private float wallPushLeft;
    private float wallPushRight;
    private float wallPushUp;
    private float wallPushDown;

    enum Form
    {
        Normal,
        Small
    }

    // Start is called before the first frame update
    void Start()
    {
        speed = 450;
        jump = new Vector3(0.0f,jumpForce, 0.0f);
        state = Form.Normal;
        playerInfo = player.transform;
        GUIControl = GameObject.FindWithTag("GameController");
        
    }

    void FixedUpdate()
    {
        float horAxis = Input.GetAxis("Horizontal");
        float verAxis = Input.GetAxis("Vertical");
        Vector3 movement = new Vector3(horAxis, 0.0f, verAxis);

        GetComponent<Rigidbody>().AddForce(movement * speed * Time.fixedDeltaTime);
        
    }

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

    public void TakeDamage()
    {
        UnityEngine.Debug.Log(canDamage);
        health_hearts -= red_damage;
        canDamage = false;
        damageCoolDown = damageCoolDown_Reset;
        UnityEngine.Debug.Log("TAKE DAMAGE");
        ReportToGUI();
    }

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

    void ReportToGUI()
    {
        GUIControl.GetComponent<GameController>().displayText();
    }

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

        if(state == Form.Normal) // character turns small
        {
            player.transform.localScale = new Vector3(playerInfo.localScale.x * JFSmallScale, playerInfo.localScale.y * JFSmallScale, playerInfo.localScale.y * JFSmallScale);
            jumpForce *= JFSmallScale;
            //player.GetComponent<Halo>().size() *= JFSmallScale;
            state = Form.Small;
            
        }
        else if(state == Form.Small)
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

    public void SetSpeed(float speed)
    {
        this.speed = speed;
    }

    public float getHealth()
    {
        return health_hearts;
    }

}
