using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    Controls controls;
    public float speed = 5f;
    public float rotSpeed = 0.6f;

    public static Vector2 getMove;
    public static Vector2 getRotate;
    public static bool pressDown = false;
    int weaponNum = 1;

    Vector3 velocity;
    public float gravity = -0.98f;

    void Awake()
    {
        //手把控制
        controls = new Controls();
        controls.player.Move.performed += ctx => getMove = ctx.ReadValue<Vector2>();
        controls.player.Move.canceled += ctx => getMove = Vector2.zero;
        controls.player.SwitchWeaponPlus.performed += ctx => SwitchWeaponPlus();
        controls.player.SwitchWeaponLess.performed += ctx => SwitchWeaponLess();
        //
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

    }

    // Update is called once per frame
    void Update()
    {

    }
}
