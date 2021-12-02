using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputSystem : MonoBehaviour
{
    #region blablabla
    #endregion
    public static InputSystem instance;
    
    Controls controls;

    #region Move
    private Vector2 inputMovement;
    private Vector3 currentMovement;
    private Vector2 inputCameraMovement;

    public bool isMovementPressed;
    #endregion

    #region Jump

    public bool isJumpPressed = false;

    #endregion

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
            controls.Menu.Menu.performed += ctx => MenuStart();
            controls.Menu.Menu.canceled += ctx => MenuEnd();
            controls.Menu.MenuSelectUp.started += ctx => SetPressDownTrue(ref MenuSelectUpPressDown);
            controls.Menu.MenuSelectUp.canceled += ctx => SetPressDownFalse(ref MenuSelectUpPressDown);
            controls.Menu.MenuSelectDown.started += ctx => SetPressDownTrue(ref MenuSelectDownPressDown);
            controls.Menu.MenuSelectDown.canceled += ctx => SetPressDownFalse(ref MenuSelectDownPressDown);
            
            
            //角色移動
            controls.player.Move.performed += OnMovementInput;
            controls.player.Move.started += OnMovementInput;
            controls.player.Move.canceled += OnMovementInput;
            //controls.player.Move.canceled += ctx => inputMovement = Vector2.zero;

            //視角移動 + 魔法瞄準角度
            controls.player.CameraMove.performed+=ctx=> inputCameraMovement= ctx.ReadValue<Vector2>();
            controls.player.CameraMove.canceled += ctx => inputCameraMovement = Vector2.zero;
            
            //跳
            controls.player.Jump.started += OnJump;
            controls.player.Jump.canceled += OnJump;

            //瞄準視角切換
            controls.player.Aim.performed += ctx => AimStart();
            controls.player.Aim.canceled += ctx => AimCanceled();

            //衝
            controls.player.Dash.started += ctx => DashStart();
            controls.player.Dash.canceled += ctx => DashCanceled();
            //射擊
            controls.player.Shoot.started += ctx => ShootStart();
            controls.player.Shoot.canceled += ctx => ShootCanceled();
            //切換魔法
            controls.player.SwitchWeaponPlus.started += ctx => PlusMagicStatus();
            controls.player.SwitchWeaponLess.started += ctx => MinusMagicStatus();

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

    void OnMovementInput(InputAction.CallbackContext ctx){
        inputMovement = ctx.ReadValue<Vector2>();
        currentMovement.x = inputMovement.x;
        currentMovement.y = 0;
        currentMovement.z = inputMovement.y;
        isMovementPressed = inputMovement.x != 0 || inputMovement.y !=0;
    }

    void OnCameraMovementInput(InputAction.CallbackContext ctx){
        inputCameraMovement = ctx.ReadValue<Vector2>()*new Vector2(-1,0);
    }
    public Vector3 GetCurrentMovement(){
        return currentMovement;
    }
    
    void OnJump(InputAction.CallbackContext ctx){
        isJumpPressed = ctx.ReadValueAsButton();

    }
    void AimStart(){

    }

    void AimCanceled(){

    }


    void DashStart(){
    }
    void DashCanceled(){
    }
    void DashComplete(){
    }

    void ShootStart(){
    }
    void ShootCanceled(){
    }

    void PlusMagicStatus(){
    }
    void MinusMagicStatus(){
    }

    void ConversationStart(){
    }
    void ConversationCanceled(){

    }
}
