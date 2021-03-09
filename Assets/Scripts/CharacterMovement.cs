using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    //public CharacterController controller;
    public float PlayerSpeed = 5f;
    public float JumpHeight = 2f;
    public float GroundDistance = 0.2f;
    public float DashDistance = 5f;
    public LayerMask Ground;

    private Rigidbody _body;
    private CapsuleCollider playerCollider;
    private Vector3 _inputs = Vector3.zero;
    private bool _isGrounded = true;
    public Transform _groundChecker;
    public GameObject groundObject;



    Controls controls;
    public float rotSpeed = 0.6f;

    public static Vector2 getMove;
    public static Vector2 getRotate;
    public static bool pressDown = false;
    public static Vector2 getAngle;
    float angle;
    Vector3 currentEulerAngles;
    public float currentGunYAngle = 0;
    public float currentGunZAngle = 0;
    public GameObject spotlight;

    //public float jumpHeight = 1f;
    public static bool JumpPressDown = false;
    int count = 100;//計算延遲

    //grab
    public static bool grabPressDown = false;
    private bool hold;
    public GameObject hand;
    private Rigidbody objectrb;
    private GameObject rememberObject = null;
    public GameObject[] box = new GameObject[4];
    public GameObject key;

    //dialogue
    public static int dialogueCount = 0;
    public static bool currentDialogueStart = false;
    public static bool currentDialoguePress;

    public static int weaponNum = 1;
    public static bool toxinGunOn = false;
    public static bool lightGunOn = false;
    public GameObject toxinGun;
    public GameObject lightGun;
    public static bool hasLamp = false;

    Vector3 velocity;//速度
    public float gravity = -0.98f;

    //血量
    int hp = 5;

    //有沒有在地板上
    public float margin = 0.1f;
    bool IsGrounded()
    {
        return Physics.Raycast(transform.position, -Vector3.up, margin);
    }

    void Awake()
    {
        //手把控制
        controls = new Controls();

        if (currentDialogueStart == false)
        {
            controls.player.Move.performed += ctx => getMove = ctx.ReadValue<Vector2>();
            controls.player.Move.canceled += ctx => getMove = Vector2.zero;
            //切槍
            controls.player.SwitchWeaponPlus.performed += ctx => SwitchWeaponPlus();
            controls.player.SwitchWeaponLess.performed += ctx => SwitchWeaponLess();
            //射擊
            controls.player.Shoot.started += ctx => ShootStart();
            controls.player.Shoot.canceled += ctx => ShootCanceled();
            //跳
            controls.player.Jump.performed += ctx => JumpStart();
            controls.player.Jump.canceled += ctx => JumpCanceled();
            //移動槍的角度
            controls.player.HandMove.performed += ctx => getAngle = ctx.ReadValue<Vector2>();
            controls.player.HandMove.canceled += ctx => getAngle = Vector2.zero;
            //grab
            controls.player.Grab.performed += ctx => GrabStart();
            controls.player.Grab.canceled += ctx => GrabCanceled();
        }






        //talk
        //controls.player.Talk.performed += ctx => DialoguePlus();
        //controls.player.Talk.canceled += ctx => DialogueStop();


    }
    //切換武器
    void SwitchWeaponPlus()
    {
        weaponNum++;
        if (weaponNum > 3)
        {
            weaponNum %= 3;
        }
    }
    void SwitchWeaponLess()
    {
        weaponNum--;
        if (weaponNum == 0)
        {
            weaponNum = 3;
        }
    }
    void ShootStart()
    {
        pressDown = true;
    }
    void ShootCanceled()
    {
        pressDown = false;

    }
    void OnEnable()
    {
        controls.player.Enable();
    }

    void OnDisable()
    {
        controls.player.Disable();
    }
    //跳
    void JumpStart()
    {
        JumpPressDown = true;

    }
    void JumpCanceled()
    {
        JumpPressDown = false;
    }
    //Grab
    void GrabStart()
    {
        grabPressDown = true;
        Debug.Log("grabstart!");
    }
    void GrabCanceled()
    {
        grabPressDown = false;
        for (int i = 0; i < box.Length; i++)
        {
            box[i].gameObject.transform.parent = null;
            box[i].GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
            box[i].layer = 0;
        }
        //if (Clearence311.clearence)
        //{
        //    key.gameObject.transform.parent = null;
        //    key.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
        //    key.layer = 0;
        //}
        Debug.Log("grabend!");
    }
    //void DialoguePlus()
    //{
    //    if (DialogueTrigger.dialogueStart == 1)
    //    {
    //        dialogueCount++;
    //    }

    //    Debug.Log("dialogueCount:" + dialogueCount);
    //}
    //void DialogueStop()
    //{
    //    //currentDialoguePress = false;
    //    //dialogueCount--;
    //}


    void Start()
    {
        //currentEulerAngles = new Vector3(0, -42.41f, 0);
        _body = GetComponent<Rigidbody>();
        playerCollider = GetComponent<CapsuleCollider>();
        //_groundChecker = transform.GetChild(0);
    }
    void Update()
    {

        //Walk+Rotate
        if (getMove.x > 0.2 || getMove.x < -0.2 || getMove.y > 0.2 || getMove.y < -0.2)
        {
            Vector3 TargetDir = new Vector3(getMove.x, 0, getMove.y);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(TargetDir), Time.time * rotSpeed);


            Vector3 dashVelocity = Vector3.Scale(transform.forward, DashDistance * new Vector3((Mathf.Log(1f / (Time.deltaTime * _body.drag + 1)) / -Time.deltaTime), 0, (Mathf.Log(1f / (Time.deltaTime * _body.drag + 1)) / -Time.deltaTime)));

            _body.velocity = new Vector3(getMove.x * PlayerSpeed * Time.deltaTime, 0, getMove.y * PlayerSpeed * Time.deltaTime);

        }

        //跳        
        if (_isGrounded)
        {
            count++;
            if (JumpPressDown && count > 18)
            {
                _body.AddForce(Vector3.up * Mathf.Sqrt(JumpHeight * -1f * Physics.gravity.y), ForceMode.VelocityChange);
                //_body.velocity = new Vector3(0, JumpHeight * Time.deltaTime*(-1), 0);
                count = 0;
                //Mathf.Sqrt(JumpHeight * -2f * Physics.gravity.y)

            }
            if (count > 1000) count = 50;
        }

        //切換槍
        if (weaponNum == 1)
        {
            toxinGunOn = false;
            lightGunOn = false;
            toxinGun.SetActive(false);
            lightGun.SetActive(false);

            playerCollider.center = new Vector3(0, -0.54f, 0.43f);

        }
        else if (weaponNum == 2)
        {
            //啟用toxinGun
            toxinGunOn = true;
            lightGunOn = false;
            toxinGun.SetActive(true);
            lightGun.SetActive(false);

            playerCollider.center = new Vector3(0, -0.54f, 1.12f);
            //調整角度
            //Debug.Log(currentEulerAngles.x);

            //Debug.Log("getAngle.y"+ getAngle.y);
            if (currentEulerAngles.x < 60.1 && currentEulerAngles.x > -60.1)
            {
                //currentEulerAngles += new Vector3(-getAngle.y, 0, 0) * Time.deltaTime * 30;
                currentEulerAngles += new Vector3(getAngle.y, 0, 0) * Time.deltaTime * 30;
                currentEulerAngles.y = currentGunYAngle;
                currentEulerAngles.z = currentGunZAngle;
                toxinGun.transform.localEulerAngles = currentEulerAngles;
                if (currentEulerAngles.x > 60) currentEulerAngles.x = 60;
                else if (currentEulerAngles.x < -60) currentEulerAngles.x = -60;
            }

        }
        else if (weaponNum == 3)
        {
            //啟用lightGun
            if (hasLamp)
            {
                toxinGunOn = false;
                lightGunOn = true;
                toxinGun.SetActive(false);
                lightGun.SetActive(true);

                playerCollider.center = new Vector3(0, -0.54f, 1.12f);
            }
            else weaponNum = 1;

        }
        //Debug.Log("hp"+hp);
        //血量控制
        //if (AttackDetermination.elfHit == true)
        //{
        //    hp--;
        //    AttackDetermination.elfHit = false;
        //}


    }

    //碰到不同顏色的果醬換標籤
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "yellow")
        {
            spotlight.tag = "yellow";
        }
        else if (other.gameObject.tag == "blue")
        {
            spotlight.tag = "blue";
        }
        else if (other.gameObject.tag == "red")
        {
            spotlight.tag = "red";
        }
        if (other.gameObject.transform.tag == "Grabbable" || other.gameObject.transform.tag == "Key")
        {
            objectrb = other.GetComponent<Rigidbody>();

            if (grabPressDown && weaponNum == 1)
            {
                //rememberObject = other.gameObject;
                other.gameObject.transform.parent = hand.transform;

                objectrb.constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezePositionY;

                other.gameObject.layer = 9;
            }
            else
            {

            }


        }





    }
}
