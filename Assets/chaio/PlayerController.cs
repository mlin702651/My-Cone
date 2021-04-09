using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    //控制器有關的變數
    Controls controls;
    public static Vector2 getMove;
    public static Vector2 getCamMove;
    public static Vector2 getRotate;
    public static bool pressDown = false;
    int weaponNum = 1;

    //腳色動作有關的變數
    public CharacterController controller;
    public Transform mainCam;
    public float speed = 6f;
    public float turnSmoothTime = 0.1f;
    float turnSmoothVelocity;

    //aim
    public Transform targetAim;
    public float Sensitivity = 100f;
    float xRotation = 0f;

    void Awake()
    {
        //手把控制
        controls = new Controls();
            //角色移動
        controls.player.Move.performed += ctx => getMove = ctx.ReadValue<Vector2>();
        controls.player.Move.canceled += ctx => getMove = Vector2.zero;
            //視角移動
        controls.player.CameraMove.performed+=ctx=> getCamMove= ctx.ReadValue<Vector2>();
        controls.player.CameraMove.canceled += ctx => getCamMove = Vector2.zero;
            //切換武器
        controls.player.SwitchWeaponPlus.performed += ctx => SwitchWeaponPlus();
        controls.player.SwitchWeaponLess.performed += ctx => SwitchWeaponLess();
            //開槍
        controls.player.Shoot.started += ctx => ShootStart();
        controls.player.Shoot.canceled += ctx => ShootCanceled();

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
        Debug.Log("fire");
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

    

    // Start is called before the first frame update
    void Start()
    {
        //鎖定滑鼠游標
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 direction = new Vector3(getMove.x, 0f, getMove.y).normalized;
        Debug.Log(getMove.x);
        //瞄準相機的移動
        if (Input.GetKey("space"))
        {
            float camMoveSpeedX = getCamMove.x * Sensitivity * Time.deltaTime;
            float camMoveSpeedY = getCamMove.y * Sensitivity * Time.deltaTime;
            controller.transform.Rotate(Vector3.up * camMoveSpeedX);

            Vector3 move = transform.right * getMove.x + transform.forward * getMove.y;
            controller.Move(move * speed * Time.deltaTime);
        }
        //free相機的移動
        else
        {
            if (direction.magnitude >= 0.1f)
            {
                float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + mainCam.eulerAngles.y;
                float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
                transform.rotation = Quaternion.Euler(0f, angle, 0f);
                Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
                controller.Move(moveDir.normalized * speed * Time.deltaTime);
            }
        }
    }
}
