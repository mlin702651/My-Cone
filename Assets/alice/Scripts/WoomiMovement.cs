using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class WoomiMovement : MonoBehaviour
{
    Controls controls;
    private Rigidbody _body;
    private bool _isGrounded = false;
    // #region#endregion
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
    
    [Header("Walk")]
    [SerializeField]private float PlayerWalkingSpeed = 5f;
    [SerializeField]private float PlayerRunningSpeed = 7f;
    [SerializeField]private float rotSpeed = 0.6f;
    private Vector2 getMove;
    #endregion
    #region Camera
    [Header("Camera")]
    [SerializeField]private Transform mainCam;
    [SerializeField]private float Sensitivity = 100f;
    [SerializeField]private float turnSmoothTime = 0.1f;
    private Vector2 getCamMove;
    public GameObject freeCamera;
    public GameObject aimCamera;
    public GameObject aimReticle;
    [SerializeField]private static int cameraChange = 1;
    private float turnSmoothVelocity;

    private Vector2 screenCenterPoint = new Vector2(Screen.width/2,Screen.height/2);
    private Ray centerRay;
    [SerializeField] private LayerMask aimColliderMask = new LayerMask();
    [SerializeField] private GameObject raycastHitPoint;
    #endregion
    #region Firepoint
    [Header("Firepoint")]
    public Transform firepoint;
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
    private bool isHolding = false;
    [Header("Shoot")]
    [SerializeField]private float magicConchHoldTime = 2f;
    #endregion
    #region Talk
    private bool ConversationPress = false;
    private bool isTalking = false;
    private bool canTalk = false;
    private Dialogue dialogue = null;
    #endregion
    
    #region SetUp
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
            //角色移動
            controls.player.Move.performed += ctx => getMove = ctx.ReadValue<Vector2>();
            controls.player.Move.canceled += ctx => getMove = Vector2.zero;

            //視角移動 + 魔法瞄準角度
            controls.player.CameraMove.performed+=ctx=> getCamMove= ctx.ReadValue<Vector2>();
            controls.player.CameraMove.canceled += ctx => getCamMove = Vector2.zero;
            
            //瞄準視角切換
            controls.player.Aim.performed += ctx => AimStart();
            controls.player.Aim.canceled += ctx => AimCanceled();

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

             //對話
            controls.player.Talk.started += ctx => ConversationStart();
            controls.player.Talk.canceled += ctx => ConversationCanceled();
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
       if(newAnimationState == currentAnimationState) {
            return; //一樣的話就不重新開始播ㄌ
        }
        _animator.Play(newAnimationState);
        
        currentAnimationState = newAnimationState;
    }
    #endregion
    void FixedUpdate()
    {
        #region Talk
        if(canTalk&&ConversationPress){
            FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
            canTalk = false;
            ConversationPress = false;
        }
        else if(ConversationPress){
            FindObjectOfType<DialogueManager>().DisplayNextSentence();
            ConversationPress = false;
        }
        if(isTalking){
            return;//在講話的時候就不能動
        }
        #endregion
        #region Camera Move Firepoint
        centerRay = Camera.main.ScreenPointToRay(screenCenterPoint);
        if(Physics.Raycast(centerRay, out RaycastHit raycastHit, 999f,aimColliderMask )){
            raycastHitPoint.transform.position = raycastHit.point;
        }

        Vector3 direction = new Vector3(getMove.x, 0f, getMove.y).normalized;
        Vector3 camDirection = new Vector3(getCamMove.x, 0f, getCamMove.y).normalized;
        //瞄準相機的移動
        if (cameraChange == 2)
        {
            float camMoveSpeedX = getCamMove.x * Sensitivity * Time.deltaTime;
            float camMoveSpeedY = getCamMove.y * Sensitivity * Time.deltaTime;
            //controller.transform.Rotate(Vector3.up * camMoveSpeedX);

            Vector3 move = transform.right * getMove.x + transform.forward * getMove.y;
            //controller.Move(move * speed * Time.deltaTime);

            // //魔法的發射角度
            // if (getCamMove.x != 0 || getCamMove.y != 0)
            // {
            //     Debug.Log("firepoint"+firepoint.transform.localEulerAngles);             
            //     Debug.Log("WOOMI"+transform.localEulerAngles);
            //     //可以用來確認local的角度
            //     Vector3 firepointlocalEulerAngles=firepoint.transform.localEulerAngles;
            //     Vector3 woomilocalEulerAngles=transform.localEulerAngles;
            //     Vector3 rotate = new Vector3( -getCamMove.y, getCamMove.x, 0) * 100f * Time.deltaTime;
            //     firepoint.Rotate(rotate, Space.Self);
                
            // }
            //firepoint會射向粉紅球的位置
            firepoint.transform.LookAt(raycastHitPoint.transform);

            if (direction.magnitude >= 0.1f)
            {
                float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + mainCam.eulerAngles.y;
                
                Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
                if(!isShooting)transform.position += moveDir.normalized*PlayerWalkingSpeed*Time.deltaTime;
                
            }
            //相機轉不了 轉woomi
            transform.eulerAngles = Vector3.Lerp(   transform.eulerAngles, 
                                                    transform.eulerAngles + new Vector3(-getCamMove.y*4,getCamMove.x*10,0),
                                                    0.1f);
            //設定上下的範圍限制
            if(transform.eulerAngles.x >= 20 && transform.eulerAngles.x <= 340) transform.eulerAngles = new Vector3(340,transform.eulerAngles.y,transform.eulerAngles.z);
            else if(transform.eulerAngles.x >= 5 && transform.eulerAngles.x < 20) transform.eulerAngles = new Vector3(5,transform.eulerAngles.y,transform.eulerAngles.z);
            
            print(transform.eulerAngles);

            
        }
        //free相機的移動
        else
        {
            transform.eulerAngles = new Vector3(0,transform.eulerAngles.y,transform.eulerAngles.z);
            if (direction.magnitude >= 0.1f)
            {
                float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + mainCam.eulerAngles.y;
                float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
                transform.rotation = Quaternion.Euler(0f, angle, 0f);
                Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
                //controller.Move(moveDir.normalized * speed * Time.deltaTime);
                if(!isShooting&&getMove.x<=0.35f&&getMove.x>=-0.35f&&getMove.y<=0.35f&&getMove.y>=-0.35f)transform.position += moveDir.normalized*PlayerWalkingSpeed*Time.deltaTime;
                else if(!isShooting)transform.position += moveDir.normalized*PlayerRunningSpeed*Time.deltaTime;
                print(getMove.x);
            }
        }

        if (cameraChange==2)
        {
            //freeCamera.SetActive(false);
            aimCamera.SetActive(true);
            aimReticle.SetActive(true);
            raycastHitPoint.SetActive(true);
        }
        else
        {
            //freeCamera.SetActive(true);
            aimCamera.SetActive(false);
            aimReticle.SetActive(false);
            raycastHitPoint.SetActive(false);
        }
        #endregion
        #region Move Animation
        if ((getMove.x > 0.1 || getMove.x < -0.1 || getMove.y > 0.1 || getMove.y < -0.1)&&!isShooting)
        {
            if(!isJumping&&!isLanding&&!isGroundDashing&&!isShooting)ChangeAnimationState(animationWalk);
        }
        else{
            if(!isJumping&&!isLanding&&!isGroundDashing&&!isShooting)ChangeAnimationState(animationIdle);
        }
        #endregion
        #region Jump&Land
        // if (JumpPressDown)//第一次跳
        // {
            
        //     ChangeAnimationState(animationJumpStart);
        //     _body.AddForce(Vector3.up * Mathf.Sqrt(JumpHeight * -1f * Physics.gravity.y), ForceMode.VelocityChange);
        //     JumpPressDown = false;
        //     isJumping = true;
        // }
        // if(_body.velocity.y<0&&!_isGrounded)//是不是在落下
        // {
        //     isLanding = true;
        //     ChangeAnimationState(animationJumpEnd);
        // }
        // if(isLanding) //如果正在落下 判斷動畫用ㄉ
        // {
        //     landingTimer+= Time.deltaTime;
        //     if(landingTimer >= landingDuration){
        //         landingTimer= 0;
        //         isLanding = false;
        //     }
        // }
        // //有沒有在蓄力 有沒有蓄力成功
        
        // if(PressingJumpTimer>JumpToLevel2HoldingTime&&_isGrounded){ //時間按夠久 而且玩家在地上
        //     canJumpLevel2 = true;
        //     Debug.Log("can jump level 2!");
        // }
        // else if(isPressingJump){
        //     PressingJumpTimer += Time.deltaTime;
        // }

        // if(canJumpLevel2&&!isPressingJump&&_isGrounded){
        //     PressingJumpTimer = 0;
        //     canJumpLevel2 = false;
        //     isJumping = true;
        //     ChangeAnimationState(animationJumpStart);
        //     //jumpDelay = _animator.GetCurrentAnimatorStateInfo(0).length;
        //     jumpDelay = 0.5f;
        //     Invoke("JumpLevel2",jumpDelay);//等跳的動畫播完才播下一個動畫
        //     _body.AddForce(Vector3.up * Mathf.Sqrt(JumpLevel2Height * -1f * Physics.gravity.y), ForceMode.VelocityChange);
            
        // }
        // else if(canJumpLevel2&&!isPressingJump){
        //     PressingJumpTimer = 0;
        //     canJumpLevel2 = false;
        // }

        //new jump
        if (JumpPressDown && !canJumpLevel2)//第一次跳  第二次跳沒有打開
        {
            
            ChangeAnimationState(animationJumpStart);
            _body.AddForce(Vector3.up * Mathf.Sqrt(JumpHeight * -1f * Physics.gravity.y), ForceMode.VelocityChange);
            JumpPressDown = false;
            isJumping = true;
            canJumpLevel2 = true;
            Debug.Log("jump 1 start!");
        }
        
        if(JumpPressDown && canJumpLevel2){
            ChangeAnimationState(animationJumpStart);
            _body.AddForce(Vector3.up * Mathf.Sqrt(JumpHeight * -1f * Physics.gravity.y), ForceMode.VelocityChange);
            JumpPressDown = false;
            canJumpLevel2 = false;
            isJumping = true;
            Debug.Log("jump 2 start!");
        }

        if(_body.velocity.y<0&&!_isGrounded)//是不是在落下
        {
            isLanding = true;
            ChangeAnimationState(animationJumpEnd);
        }
        if(isLanding) //如果正在落下 判斷動畫用ㄉ
        {
            landingTimer+= Time.deltaTime;
            if(landingTimer >= landingDuration){
                landingTimer= 0;
                isLanding = false;
            }
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
                case 0: //海螺
                    Debug.Log("magic1");
                    ChangeAnimationState(animationStartMagicConch);
                    float magicConchStartDelay = _animator.GetCurrentAnimatorStateInfo(0).length;
                    Invoke("PlayHoldMagicConchAnimation",magicConchStartDelay);
                    ShootPressDown = false;
                    break;
                case 1:
                    Debug.Log("magic1");
                    ChangeAnimationState(animationStartMagicBubble);
                    float magicBubbleStartDelay = _animator.GetCurrentAnimatorStateInfo(0).length;
                    Invoke("PlayHoldMagicBubbleAnimation",magicBubbleStartDelay);
                    ShootPressDown = false;
                    break;
                case 2:
                    Debug.Log("magic3");
                    ChangeAnimationState(animationMagicBomb);
                    float magicBombDelay = _animator.GetCurrentAnimatorStateInfo(0).length;
                    Invoke("CompleteShooting",magicBombDelay);
                    ShootPressDown = false;
                    break;
                default:
                    break;
            }
        }
        if(isHolding&&!isPressingShoot){
            isHolding = false;
            switch(magicStatus){
                case 0: //海螺
                    ChangeAnimationState(animationEndMagicConch);
                    float magicConchEndDelay = _animator.GetCurrentAnimatorStateInfo(0).length;
                    Invoke("CompleteShooting",magicConchEndDelay);
                    
                    
                    break;
                case 1:
                    ChangeAnimationState(animationEndMagicBubble);
                    float magicBubbleEndDelay = _animator.GetCurrentAnimatorStateInfo(0).length;
                    Invoke("CompleteShooting",magicBubbleEndDelay);
                    break;
                case 2:
                    break;
                default:
                    break;
            }
            
        }
        if(isPressingShoot){
            isShooting = true;
            PressingShootTimer += Time.deltaTime;
        }
        else{
            //isShooting = false;
        }
        #endregion
    }

#region Aim

    void AimStart()
    {
        cameraChange = 2;
        
    }
    void AimCanceled()
    {
        cameraChange = 1;
        
    }
#endregion
#region Jump

    // void JumpLevel2(){
    //     ChangeAnimationState(animationJumpLevel2);
    // }
    // void JumpStart()
    // {
    //     if(_isGrounded)JumpPressDown = true;
    //     isPressingJump = true;
    //     Debug.Log("jump!");

    // }
    // void JumpCanceled()
    // {
    //     isPressingJump = false;
    //     Debug.Log("jumpEnd!");
    // }

    // new jump
    void JumpStart()
    {
        if(_isGrounded||canJumpLevel2){
            JumpPressDown = true;
            Debug.Log("can jump!");

        }
        if(canJumpLevel2) {
            JumpPressDown = true;
            // Debug.Log("can jump!");

        }
        isPressingJump = true;
        Debug.Log("jump press start!");

    }
    void JumpCanceled()
    {
        isPressingJump = false;
        Debug.Log("jump press end!");
    }
#endregion
#region Dash
    void DashStart(){
        DashPressDown = true;
    }
    void DashCanceled(){
        DashPressDown = false;
    }
    void DashComplete(){
        isGroundDashing = false;
    }
#endregion
#region Shoot
    void ShootStart(){
        ShootPressDown = true;
        isPressingShoot = true;
    }
    void ShootCanceled(){
        //ShootPressDown = false;
        isPressingShoot = false;
        PressingShootTimer = 0;
    }
    void PlayHoldMagicConchAnimation(){
        if(isPressingShoot&&PressingShootTimer<=magicConchHoldTime){
            isHolding = true;
            ChangeAnimationState(animationHoldMagicConch);
            float magicConchStartDelay = _animator.GetCurrentAnimatorStateInfo(0).length;
            Invoke("PlayHoldMagicConchAnimation",magicConchStartDelay-0.2f);
            Debug.Log("is playing hold1");
        }
        else{
            isHolding = false;
            CompleteShooting();
        }
        
    }
    void PlayHoldMagicBubbleAnimation(){
        if(isPressingShoot&&PressingShootTimer<=magicConchHoldTime){
            isHolding = true;
            ChangeAnimationState(animationHoldMagicBubble);
            float magicBubbleStartDelay = _animator.GetCurrentAnimatorStateInfo(0).length;
            Invoke("PlayHoldBubbleConchAnimation",magicBubbleStartDelay-0.2f);
            Debug.Log("is playing hold21");
        }
        else{
            isHolding = false;
            CompleteShooting();
        }
        
    }
    void CompleteShooting(){
        isShooting = false;
    }
#endregion
#region ChangeMagic
    void PlusMagicStatus(){
        PlusMagicStatusPress = true;
    }
    void MinusMagicStatus(){
        MinusMagicStatusPress = true;
    }
#endregion
#region Talk
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
    public void SetCanTalkStatus(bool cantalkbool){
        canTalk = cantalkbool;
    }
    public void GetDialogue(Dialogue _dialogue){
        dialogue = _dialogue;
    }
#endregion
#region Trigger

    private void OnTriggerEnter(Collider other) {
         _isGrounded = true;
         if(isJumping){
            
         }
         isJumping = false;
         canJumpLevel2 = false;
    }
    private void OnTriggerStay(Collider other) {
        _isGrounded = true;
    }
    private void OnTriggerExit(Collider other) {
         _isGrounded = false;
    }
#endregion

}
