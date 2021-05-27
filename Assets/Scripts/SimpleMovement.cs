using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleMovement : MonoBehaviour
{

    Controls controls;

    public static Vector2 getMove;
    public static Vector2 getRotate;

    public GameObject woomi;

    //move
    private Rigidbody _body;
    public float PlayerSpeed = 5f;
    public float rotSpeed = 0.6f;
    public float DashDistance = 5f;
    //jump
    //private bool _isGrounded = true;
    public static bool JumpPressDown = false;
    public static bool canHighJump = false;
    public static bool isPressing = false;
    public float JumpHeight = 2f;
    public float jumpForwardSpeed = 4f;
    private int jumpStatus = 0;
    private bool ifTimer = false;
    private float timer = 0;
    private bool ifStartJumpTimer = false;
    private float jumpTimer = 0;

    //dash
    public static bool DashPressDown = false;
    public static bool canDash = false;
    private int dashCount = 0;

    //talk
    private bool ConversationPress = false;
    private bool isTalking = false;

    void Awake()
    {
        //手把控制
        controls = new Controls();

            controls.player.Move.performed += ctx => getMove = ctx.ReadValue<Vector2>();
            controls.player.Move.canceled += ctx => getMove = Vector2.zero;
            
            //跳
            controls.player.Jump.started += ctx => JumpStart();
            controls.player.Jump.canceled += ctx => JumpCanceled();

            //衝
            controls.player.Dash.started += ctx => DashStart();
            controls.player.Dash.canceled += ctx => DashCanceled();

             //對話
            controls.player.Talk.started += ctx => ConversationStart();
            controls.player.Talk.canceled += ctx => ConversationCanceled();


    }

    void OnEnable()
    {
        controls.player.Enable();
    }

    void OnDisable()
    {
        controls.player.Disable();
    }

    void JumpStart()
    {
        JumpPressDown = true;
        isPressing = true;
        Debug.Log("jump!");

    }
    void JumpCanceled()
    {
        canHighJump = false;
        isPressing = false;
        Debug.Log("jumpEnd!");
    }

    void DashStart()
    {
        DashPressDown = true;
        Debug.Log("dash!");

    }
    void DashCanceled()
    {
        DashPressDown = false;
        Debug.Log("dashEnd!");
    }

    void ConversationStart(){
         Debug.Log("ConversationBtn Start");
         ConversationPress = true;
    }
    void ConversationCanceled(){
         Debug.Log("ConversationBtn Leave");

    }

    public void SetTalkingStatus(bool talkbool){
        isTalking = talkbool;
    }

    // Start is called before the first frame update
    void Start()
    {
        _body = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        
        //對話
        if(ConversationPress){
            FindObjectOfType<DialogueManager>().DisplayNextSentence();
            ConversationPress = false;
        }
        if(isTalking){
            return;//在講話的時候就不能動
        }
        if (getMove.x > 0.2 || getMove.x < -0.2 || getMove.y > 0.2 || getMove.y < -0.2)
        {
            Vector3 TargetDir = new Vector3(getMove.x, 0, getMove.y);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(TargetDir), Time.time * rotSpeed);


            Vector3 dashVelocity = Vector3.Scale(transform.forward, DashDistance * new Vector3((Mathf.Log(1f / (Time.deltaTime * _body.drag + 1)) / -Time.deltaTime), 0, (Mathf.Log(1f / (Time.deltaTime * _body.drag + 1)) / -Time.deltaTime)));

            //_body.velocity = new Vector3(getMove.x * PlayerSpeed * Time.deltaTime, -getMove.y*0.2f, getMove.y * PlayerSpeed * Time.deltaTime);
            transform.position += new Vector3(getMove.x * PlayerSpeed * Time.deltaTime, 0, getMove.y * PlayerSpeed * Time.deltaTime);

        }

        if (ifTimer)
        {
            timer += Time.deltaTime;
            if(woomi.transform.localScale.y>=0.77) woomi.transform.localScale -= new Vector3(0, 0.0035f, 0);
            //Debug.Log(timer);
        }
        if (ifStartJumpTimer)
        {
            jumpTimer += Time.deltaTime;
            if (jumpTimer >= 2)
            {
                jumpTimer = 0;
                ifStartJumpTimer = false;
            }
                
            //Debug.Log(timer);
        }

        //跳        
        if (JumpPressDown&& jumpStatus == 0)
        {
            ifStartJumpTimer = true;
            if (jumpTimer==0) _body.AddForce(Vector3.up * Mathf.Sqrt(JumpHeight * -1f * Physics.gravity.y), ForceMode.VelocityChange);
            jumpStatus += 1;
            JumpPressDown = false;
            canDash = true;
            //jumpStatus = 1;
        }
        else if(jumpStatus == 1&& isPressing == true)
        {
            ifTimer = true;
            
            
        }
        if (timer > 1.5)
        {
            canHighJump = true;
            Debug.Log("CHJ!");
        }
        
        if (isPressing == false)
        {
            if(canHighJump == true)
            {
                _body.AddForce(Vector3.up * Mathf.Sqrt(JumpHeight * -3f * Physics.gravity.y), ForceMode.VelocityChange);
                canHighJump = false;

            }
            ifTimer = false;
            woomi.transform.localScale = new Vector3(1, 1, 1);
            timer = 0;
            jumpStatus = 0;
        }
        if (canDash&& DashPressDown == true)
        {
            if (dashCount < 2)
            {
                _body.AddForce(Vector3.up * Mathf.Sqrt(JumpHeight * -0.5f * Physics.gravity.y), ForceMode.VelocityChange);
                //woomi.transform.position += new Vector3(10 * Time.deltaTime, 0, 10 * Time.deltaTime);
                // woomi.GetComponent<Rigidbody>().velocity = transform.forward * Time.deltaTime * 10;
                 _body.velocity = transform.forward * Time.deltaTime * 500;
                dashCount++;
                Debug.Log("DDD!");
            }
            else if (dashCount >= 2)
            {
                canDash = false;
                dashCount = 0;
            }
                
            DashPressDown = false;
        }
       
        //else if(jumpStatus >= 1)
        //{
        //    if (timer < 2.0 && jumpStatus==2)
        //    {
        //        _body.AddForce(Vector3.forward * Mathf.Sqrt(jumpForwardSpeed * -1f * Physics.gravity.y), ForceMode.VelocityChange);
        //        ifTimer = false;
        //        timer = 0;
        //        jumpStatus = 0;
        //    }
        //    else if(timer>=2.0)
        //    {
        //        ifTimer = false;
        //        timer = 0;
        //        jumpStatus = 0;
        //    }
           
            

        //}

        
        
    }
}
