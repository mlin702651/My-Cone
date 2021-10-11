using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputSystem : MonoBehaviour
{
    public static InputSystem instance;
    
    Controls controls;
    private Vector2 getMove;
    private Vector2 getCamMove;
    #region Menu

    public bool MenuPressDown{get; set;}
    public bool MenuSelectUpPressDown;
    public bool MenuSelectDownPressDown;

    #endregion

    // Start is called before the first frame update

    private void Awake() {
        if(instance!= null){
             Debug.LogWarning("fix this: " + gameObject.name);
        }
        else instance = this;

        controls = new Controls();

            //選單
            controls.player.Menu.performed += ctx => MenuStart();
            controls.player.Menu.canceled += ctx => MenuEnd();
            controls.player.MenuSelectUp.started += ctx => SetPressDownTrue(ref MenuSelectUpPressDown);
            controls.player.MenuSelectUp.canceled += ctx => SetPressDownFalse(ref MenuSelectUpPressDown);
            controls.player.MenuSelectDown.started += ctx => SetPressDownTrue(ref MenuSelectDownPressDown);
            controls.player.MenuSelectDown.canceled += ctx => SetPressDownFalse(ref MenuSelectDownPressDown);
            // //角色移動
            // controls.player.Move.performed += ctx => getMove = ctx.ReadValue<Vector2>();
            // controls.player.Move.canceled += ctx => getMove = Vector2.zero;

            // //視角移動 + 魔法瞄準角度
            // controls.player.CameraMove.performed+=ctx=> getCamMove= ctx.ReadValue<Vector2>();
            // controls.player.CameraMove.canceled += ctx => getCamMove = Vector2.zero;
            
            // //瞄準視角切換
            // controls.player.Aim.performed += ctx => AimStart();
            // controls.player.Aim.canceled += ctx => AimCanceled();

            // //跳
            // controls.player.Jump.started += ctx => JumpStart();
            // controls.player.Jump.canceled += ctx => JumpCanceled();

            // //衝
            // controls.player.Dash.started += ctx => DashStart();
            // controls.player.Dash.canceled += ctx => DashCanceled();
            // //射擊
            // controls.player.Shoot.started += ctx => ShootStart();
            // controls.player.Shoot.canceled += ctx => ShootCanceled();
            // //切換魔法
            // controls.player.SwitchWeaponPlus.started += ctx => PlusMagicStatus();
            // controls.player.SwitchWeaponLess.started += ctx => MinusMagicStatus();

            //  //對話
            // controls.player.Talk.started += ctx => ConversationStart();
            // controls.player.Talk.canceled += ctx => ConversationCanceled();
    }
    

    void OnEnable()
    {
        controls.player.Enable();
    }
    void OnDisable()
    {
        controls.player.Disable();
    }

    void MenuStart(){
        MenuPressDown = true;
        Debug.Log("menu open");
    }

    void MenuEnd(){
        MenuPressDown = false;
    }


    void SetPressDownTrue(ref bool pressDown){
        pressDown = true;
        //print("hey");
    }
    void SetPressDownFalse(ref bool pressDown){
        pressDown = false;
    }
}
