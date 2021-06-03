using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WoomiMovement : MonoBehaviour
{
    Controls controls;

    private Rigidbody _body;
    private bool _isGrounded = false;
    #region Animation
    Animator _animator;
    private int currentAnimationState;
    private int animationIdle;
    private int animationWalk;
    private int animationRun;
    private int animationJumpStart;
    private int animationJumpLevel2;
    private int animationJumpEnd;
    private int animationDash;
    private int animationDashInAir;
    private int animationDashInAirLevel2;
    private int animationTalk;
    private int animationStartMagicConch;
    private int animationHoldMagicConch;
    private int animationEndMagicConch;
    private int animationStartMagicBubble;
    private int animationHoldMagicBubble;
    private int animationEndMagicBubble;
    private int animationMagicBomb;
    private int animationSlide;
    private int animationStayInAir;
    #endregion
    
    #region Walk
    
    private Vector2 getMove;
    [Header("Walk")]
    [SerializeField]private float PlayerSpeed = 5f;
    [SerializeField]private float rotSpeed = 0.6f;
    #endregion
    
    #region Jump
    [Header("Jump")]
    [SerializeField]private float JumpHeight = 2f;
    [SerializeField]private float JumpLevel2Height = 4f;
    [SerializeField]private float JumpToLevel2HoldingTime = 1.5f;
    
    private static bool JumpPressDown = false;
    private bool isJumping;
    private bool isPressingJump;
    private float PressingJumpTimer = 0;
    private bool canJumpLevel2 = false;
    private float jumpLevel2Timer = 0;
    private bool isLanding;
    private float landingTimer = 0;
    private float jumpDelay = 0;

    [SerializeField] private float landingDuration = 0.5f;
    
    #endregion
    
    #region Dash
    private static bool DashPressDown = false;
    private bool isGroundDashing = false;
    [Header("Dash")]
    [SerializeField]private float dashSpeed = 300;
    [SerializeField]private float groundDashDelay = 2f;
    #endregion

    #region Shoot
    private int magicStatus = 0; //0海螺 1泡泡 2海菇
    private bool ShootPressDown = false;
    private bool isPressingShoot = false;
    private float PressingShootTimer = 0;
    private bool PlusMagicStatusPress = false;
    private bool MinusMagicStatusPress = false;
    private bool isShooting = false;
    #endregion
    // #region#endregion
    void Awake()
    {
         _animator = GetComponent<Animator>();
        #region Set Animation HashID
        animationIdle = Animator.StringToHash("Player_Idle");
        animationWalk = Animator.StringToHash("Player_Walk");
        animationRun = Animator.StringToHash("Player_Run");
        animationStayInAir = Animator.StringToHash("Player_StayInAir");
        animationJumpStart = Animator.StringToHash("Player_JumpStart");
        animationJumpLevel2 = Animator.StringToHash("Player_JumpLevel2");
        animationJumpEnd = Animator.StringToHash("Player_JumpEnd");
        animationDash = Animator.StringToHash("Player_Dash");
        animationDashInAir = Animator.StringToHash("Player_DashInAir");
        animationDashInAirLevel2 = Animator.StringToHash("Player_DashInAirLevel2");
        animationTalk = Animator.StringToHash("Player_Talk");
        animationSlide = Animator.StringToHash("Player_Slide");
        animationStartMagicConch = Animator.StringToHash("Player_StartMagicConch");
        animationHoldMagicConch = Animator.StringToHash("Player_HoldMagicConch");
        animationEndMagicConch = Animator.StringToHash("Player_EndMagicConch");
        animationStartMagicBubble = Animator.StringToHash("Player_StartMagicBubble");
        animationHoldMagicBubble = Animator.StringToHash("Player_HoldMagicBubble");
        animationEndMagicBubble = Animator.StringToHash("Player_EndMagicBubble");
        animationMagicBomb = Animator.StringToHash("Player_MagicBomb");
        
        //= Animator.StringToHash("Player_");
        #endregion

        #region GamePadControls
        
        controls = new Controls();

            controls.player.Move.performed += ctx => getMove = ctx.ReadValue<Vector2>();
            controls.player.Move.canceled += ctx => getMove = Vector2.zero;
            
            //跳
            controls.player.Jump.started += ctx => JumpStart();
            controls.player.Jump.canceled += ctx => JumpCanceled();

            //衝
            controls.player.Dash.started += ctx => DashStart();
            controls.player.Dash.canceled += ctx => DashCanceled();
            //射擊
            controls.player.Shoot.started += ctx => ShootStart();
            controls.player.Shoot.canceled += ctx => ShootCanceled();
            //切換魔法
            controls.player.SwitchWeaponPlus.started += ctx => PlusMagicStatus();
            controls.player.SwitchWeaponLess.started += ctx => MinusMagicStatus();

            //  //對話
            // controls.player.Talk.started += ctx => ConversationStart();
            // controls.player.Talk.canceled += ctx => ConversationCanceled();
        #endregion
    }

    private void Start() {
       
        _body = GetComponent<Rigidbody>();
        currentAnimationState = animationIdle;
        ChangeAnimationState(animationIdle);
    }

    void OnEnable()
    {
        controls.player.Enable();
    }

    void OnDisable()
    {
        controls.player.Disable();
    }

    void ChangeAnimationState(int newAnimationState)
    {
        if(newAnimationState == currentAnimationState) return; //一樣的話就不重新開始播ㄌ

        _animator.Play(newAnimationState);
        
        currentAnimationState = newAnimationState;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        
        if ((getMove.x > 0.2 || getMove.x < -0.2 || getMove.y > 0.2 || getMove.y < -0.2)&&!isShooting)
        {
            if(!isJumping&&!isLanding&&!isGroundDashing&&!isShooting)ChangeAnimationState(animationWalk);
            Vector3 TargetDir = new Vector3(getMove.x, 0, getMove.y);
            transform.rotation = Quaternion.RotateTowards(
                transform.rotation,
                Quaternion.LookRotation(TargetDir),
                Time.time * rotSpeed
            );
            transform.position += new Vector3(
                getMove.x * PlayerSpeed * Time.deltaTime,
                0,
                getMove.y * PlayerSpeed * Time.deltaTime
            );
            // _body.velocity = transform.forward * Time.deltaTime * PlayerSpeed *getMove.y;
            // transform.rotation = Quaternion.RotateTowards(
            //     transform.rotation,
            //     transform.rotation+new Quaternion(),
            //     Time.time * rotSpeed
            // );

        }
        else{
            if(!isJumping&&!isLanding&&!isGroundDashing&&!isShooting)ChangeAnimationState(animationIdle);
        }
        #region Jump&Land
        if (JumpPressDown)
        {
            
            ChangeAnimationState(animationJumpStart);
            _body.AddForce(Vector3.up * Mathf.Sqrt(JumpHeight * -1f * Physics.gravity.y), ForceMode.VelocityChange);
            JumpPressDown = false;
            isJumping = true;
        }
        else{
            //_animator.SetBool("_bIsJumping",false);
            //_animator.SetBool("_bIsGrounded",_isGrounded);
        }
        if(isLanding) 
        {
            landingTimer+= Time.deltaTime;
            if(landingTimer >= landingDuration){
                landingTimer= 0;
                isLanding = false;
            }
        }
        //有沒有在蓄力 有沒有蓄力成功
        
        if(PressingJumpTimer>JumpToLevel2HoldingTime&&_isGrounded){ //時間按夠久 而且玩家在地上
            canJumpLevel2 = true;
            Debug.Log("can jump level 2!");
        }
        else if(isPressingJump){
            PressingJumpTimer += Time.deltaTime;
        }

        if(canJumpLevel2&&!isPressingJump&&_isGrounded){
            PressingJumpTimer = 0;
            canJumpLevel2 = false;
            isJumping = true;
            ChangeAnimationState(animationJumpStart);
            //jumpDelay = _animator.GetCurrentAnimatorStateInfo(0).length;
            jumpDelay = 0.5f;
            Invoke("JumpLevel2",jumpDelay);//等跳的動畫播完才播下一個動畫
            _body.AddForce(Vector3.up * Mathf.Sqrt(JumpLevel2Height * -1f * Physics.gravity.y), ForceMode.VelocityChange);
            
        }
        else if(canJumpLevel2&&!isPressingJump){
            PressingJumpTimer = 0;
            canJumpLevel2 = false;
        }

        #endregion
        #region Dash
        if(DashPressDown&&_isGrounded&&!isGroundDashing){
            ChangeAnimationState(animationDash);
            isGroundDashing = true;
             _body.velocity = transform.forward * Time.deltaTime * dashSpeed;
             Invoke("DashComplete",groundDashDelay);
        }
        #endregion
        #region ChangeMagic
        if(PlusMagicStatusPress){
            PlusMagicStatusPress = false;
            magicStatus ++;
            if(magicStatus==3) magicStatus = 0;
            Debug.Log("magicStatus"+magicStatus);
        }
        if(MinusMagicStatusPress){
            MinusMagicStatusPress = false;
            magicStatus --;
            if(magicStatus==-1) magicStatus = 2;
            Debug.Log("magicStatus"+magicStatus);
        }
        #endregion
        #region Shoot
        if(ShootPressDown){
            
            switch(magicStatus){
                case 0:
                Debug.Log("magic1");
                ChangeAnimationState(animationStartMagicConch);
                ShootPressDown = false;
                    break;
                case 1:
                    break;
                case 2:
                    break;
                default:
                    break;
            }
        }
        if(isPressingShoot){
            isShooting = true;
            //PressingShootTimer
        }
        else{
            isShooting = false;
        }
        #endregion
    }

    void JumpLevel2(){
        ChangeAnimationState(animationJumpLevel2);
    }
    void JumpStart()
    {
        if(_isGrounded)JumpPressDown = true;
        isPressingJump = true;
        Debug.Log("jump!");

    }
    void JumpCanceled()
    {
        isPressingJump = false;
        Debug.Log("jumpEnd!");
    }

    void DashStart(){
        DashPressDown = true;
    }
    void DashCanceled(){
        DashPressDown = false;
    }

    void DashComplete(){
        isGroundDashing = false;
    }

    void ShootStart(){
        ShootPressDown = true;
        isPressingShoot = true;
    }
    void ShootCanceled(){
        ShootPressDown = false;
        isPressingShoot = false;
    }

    void PlusMagicStatus(){
        PlusMagicStatusPress = true;
    }
    void MinusMagicStatus(){
        MinusMagicStatusPress = true;
    }


    private void OnTriggerEnter(Collider other) {
         _isGrounded = true;
         if(isJumping){
            isLanding = true;
            ChangeAnimationState(animationJumpEnd);
         }
         isJumping = false;
    }
    private void OnTriggerStay(Collider other) {
        _isGrounded = true;
    }
    private void OnTriggerExit(Collider other) {
         _isGrounded = false;
    }
}
