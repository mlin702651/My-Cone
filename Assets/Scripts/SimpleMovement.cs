using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleMovement : MonoBehaviour
{

    Controls controls;

    public static Vector2 getMove;
    public static Vector2 getRotate;

    //move
    private Rigidbody _body;
    public float PlayerSpeed = 5f;
    public float rotSpeed = 0.6f;
    public float DashDistance = 5f;
    //jump
    //private bool _isGrounded = true;
    public static bool JumpPressDown = false;
    public float JumpHeight = 2f;
    public float jumpForwardSpeed = 4f;
    private int jumpStatus = 0;
    private bool ifTimer = false;
    private float timer = 0;

    void Awake()
    {
        //手把控制
        controls = new Controls();

            controls.player.Move.performed += ctx => getMove = ctx.ReadValue<Vector2>();
            controls.player.Move.canceled += ctx => getMove = Vector2.zero;
            
            //跳
            controls.player.Jump.started += ctx => JumpStart();
            controls.player.Jump.canceled += ctx => JumpCanceled();
            

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
        jumpStatus += 1;
        Debug.Log("jump!");

    }
    void JumpCanceled()
    {
        //JumpPressDown = false;
        //jumpStatus = 0;
        Debug.Log("jumpEnd!");
    }

    // Start is called before the first frame update
    void Start()
    {
        _body = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (getMove.x > 0.2 || getMove.x < -0.2 || getMove.y > 0.2 || getMove.y < -0.2)
        {
            Vector3 TargetDir = new Vector3(getMove.x, 0, getMove.y);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(TargetDir), Time.time * rotSpeed);


            Vector3 dashVelocity = Vector3.Scale(transform.forward, DashDistance * new Vector3((Mathf.Log(1f / (Time.deltaTime * _body.drag + 1)) / -Time.deltaTime), 0, (Mathf.Log(1f / (Time.deltaTime * _body.drag + 1)) / -Time.deltaTime)));

            _body.velocity = new Vector3(getMove.x * PlayerSpeed * Time.deltaTime, 0, getMove.y * PlayerSpeed * Time.deltaTime);

        }

        if (ifTimer)
        {
            timer += Time.deltaTime;
        }

        //跳        
        if (JumpPressDown)
        {
            _body.AddForce(Vector3.up * Mathf.Sqrt(JumpHeight * -1f * Physics.gravity.y), ForceMode.VelocityChange);
            JumpPressDown = false;
            ifTimer = true;
            //jumpStatus = 1;
        }
        else if(jumpStatus >= 1)
        {
            if (timer < 2.0 && jumpStatus==2)
            {
                _body.AddForce(Vector3.forward * Mathf.Sqrt(jumpForwardSpeed * -1f * Physics.gravity.y), ForceMode.VelocityChange);
                ifTimer = false;
                timer = 0;
                jumpStatus = 0;
            }
            else if(timer>=2.0)
            {
                ifTimer = false;
                timer = 0;
                jumpStatus = 0;
            }
           
            

        }
    }
}
