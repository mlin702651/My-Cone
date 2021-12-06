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
        private Vector3 currentCameraMovement;
        public Vector3 CurrentCameraMovement{get{return currentCameraMovement;}}

        public bool isMovementPressed;
    #endregion

    #region Jump

        bool _isJumpPressed = false;
        public bool IsJumpPressed {get {return _isJumpPressed; } set {_isJumpPressed = value;}}

    #endregion

    #region Talk
        bool _isConversationPressed = false;
        public bool IsConversationPressed {get{return _isConversationPressed;} set{_isConversationPressed = value;}}

    #endregion
    #region Shoot
        bool _isShootPressed = false;
        bool _isShootReleased = false;
        public bool IsShootPressed {get{return _isShootPressed;} set{_isShootPressed = value;}}
        public bool IsShootReleased {get{return _isShootReleased;} set{_isShootReleased = value;}}
    #endregion
    #region Magic
        bool _isMagicPlusStatusPressed = false;
        bool _isMagicMinusStatusPressed = false;
        public bool IsMagicPlusStatusPressed {get{return _isMagicPlusStatusPressed;} set{_isMagicPlusStatusPressed = value;}}
        public bool IsMagicMinusStatusPressed {get{return _isMagicMinusStatusPressed;} set{_isMagicMinusStatusPressed = value;}}

    #endregion
    #region Aim
        bool _isAimPressed;
        public bool IsAimPressed {get{return _isAimPressed;} }

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
            controls.player.CameraMove.performed+=OnCameraMovementInput;
            controls.player.CameraMove.canceled += OnCameraMovementInput;
            // controls.player.CameraMove.performed+=ctx=> inputCameraMovement= ctx.ReadValue<Vector2>();
            // controls.player.CameraMove.canceled += ctx => inputCameraMovement = Vector2.zero;
            
            //跳
            controls.player.Jump.started += ctx => JumpStart();
            controls.player.Jump.canceled += ctx => JumpCanceled();

            //瞄準視角切換
            // controls.player.Aim.performed += ctx => AimStart();
            // controls.player.Aim.canceled += ctx => AimCanceled();
            controls.player.Aim.performed += OnAim;
            controls.player.Aim.canceled += OnAim;

            //衝
            controls.player.Dash.started += ctx => DashStart();
            controls.player.Dash.canceled += ctx => DashCanceled();
            //射擊
            controls.player.Shoot.started += ctx => ShootStart();
            controls.player.Shoot.canceled += ctx => ShootCanceled();
            //切換魔法
            
            controls.player.SwitchWeaponPlus.started += ctx => PlusMagicStart();
            controls.player.SwitchWeaponPlus.canceled += ctx => PlusMagicCanceled();
            controls.player.SwitchWeaponLess.started += ctx => MinusMagicStart();
            controls.player.SwitchWeaponLess.canceled += ctx => MinusMagicCanceled();

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
        inputCameraMovement = ctx.ReadValue<Vector2>()*new Vector2(-1,-1);
        currentCameraMovement.x = inputCameraMovement.x;
        currentCameraMovement.y = inputCameraMovement.y;
        //Debug.Log(currentCameraMovement);
    }


    public Vector3 GetCurrentMovement(){
        return currentMovement;
    }
    
    void OnJump(InputAction.CallbackContext ctx){
        _isJumpPressed = ctx.ReadValueAsButton();

    }

    void JumpStart(){
        _isJumpPressed = true;
    }
    void JumpCanceled(){
        _isJumpPressed = false;
    }
    void AimStart(){

    }

    void OnAim(InputAction.CallbackContext ctx){
        _isAimPressed = ctx.ReadValueAsButton();
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
        _isShootPressed = true;
        _isShootReleased = false;
    }
    void ShootCanceled(){
        _isShootPressed = false;
        _isShootReleased = true;
    }

    

    void PlusMagicStart(){
        _isMagicPlusStatusPressed = true;
    }
    void PlusMagicCanceled(){
        _isMagicPlusStatusPressed = false;
    }
    void MinusMagicStart(){
        _isMagicMinusStatusPressed = true;
    }
    void MinusMagicCanceled(){
        _isMagicMinusStatusPressed = false;
    }

    void ConversationStart(){
        _isConversationPressed = true;
        if(DialogueManager.instance.inDialogue){
            DialogueManager.instance.DequeueDialogue();
            _isConversationPressed = false;
        }
    }
    void ConversationCanceled(){
        _isConversationPressed = false;
    }
}
