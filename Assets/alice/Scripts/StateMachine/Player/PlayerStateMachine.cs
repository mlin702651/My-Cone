using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateMachine : MonoBehaviour
{
    Transform mainCamera;
    #region Player
        CharacterController _characterController;
        [Header("Interact with rigid body")]
        [SerializeField] float _pushPower = 2.0F;
        [SerializeField]RespawnPoint currentRespawnPoint;
        [SerializeField] bool minusTest = false;
        [SerializeField] bool addTest = false;
        private int health = 100;
        private int crystalAmount = 0;
        private int spiritAmount = 0; 
    #endregion
    #region Move
    [Header("Movement")]
        [SerializeField]float _walkingSpeed = 2.0f;
        [SerializeField]float _runningSpeed = 5.0f;
        [SerializeField]float _jumpingFriction = 0.8f;
        //[SerializeField]float rotationFactorPerFrame = 1.0f;
        [SerializeField]private float _turnSmoothTime = 0.1f;
        Vector3 _currentMovement;
        Vector3 _gravityMovement = new Vector3(0,0,0);
        Vector3 _appliedMovement = new Vector3(0,0,0);  //跟跳躍有關的
        float _targetAngle;
        Vector3 moveDir;
        float _currentSpeed = 2.0f;
        private float _turnSmoothVelocity;
        bool _isRunning = false;
        bool _isWalking = false;

    #endregion

    #region Jump
        [Header("Jump")]
        [SerializeField]float _maxJumpHeight = 1.0f;
        [SerializeField]float _maxJumpTime = 0.5f;
        [SerializeField]float _maxSecondJumpHeight = 1.0f;
        [SerializeField]float _maxSecondJumpTime = 0.5f;
        [SerializeField]float _fallMultiplier = 2.0f;
        float _groundGravity = -0.5f;
        float _gravity = -9.8f;
        float _initialJumpVelocity;
        float _initialSecondJumpVelocity;
        bool _isJumping = false;
        bool _isSecondJumping = false;

        bool _isFalling = false;
        bool _canSecondJump = false;
    #endregion
    #region Shoot
        [Header("Shoot")]
        [SerializeField]float _magicConchMaxHoldingTime = 3.0f;
        [SerializeField]float _magicConchMinHoldingTime = 0.5f;
        public Dictionary<string, int> PlayerMagicDic =
        new Dictionary<string, int>()
        {
            {"Magic1", 1}, 
            {"Magic2", 2},
            {"Magic3", 3}, 
            //{"Magic1L2", 4}
        };
        
        int _currentMagic = 0; //1
        bool _isShooting;
        bool _isHolding;
        bool _isEnding;
        float _holdingTime = .0f;
        int _currentBombLeft = 5;
        public bool IsShooting {get{return _isShooting;} set{_isShooting = value;}}
        public bool IsHolding {get{return _isHolding;} set{_isHolding = value;}}
        public bool IsEnding {get{return _isEnding;} set{_isEnding = value;}}
        
        public float MagicConchMaxHoldingTime {get{return _magicConchMaxHoldingTime;}}
        public float MagicConchMinHoldingTime {get{return _magicConchMinHoldingTime;}}
        public float HoldingTime {get{return _holdingTime;} set{_holdingTime = value;}}
        public int CurrentBombCount {get{return _currentBombLeft;} set{_currentBombLeft = value;}}
        //海螺
        public int AnimationStartMagicConch {get{return animationStartMagicConch;}}
        public int AnimationHoldMagicConch {get{return animationHoldMagicConch;}}
        public int AnimationEndMagicConch {get{return animationEndMagicConch;}}
        public int AnimationHoldMagicConchRun {get{return animationHoldMagicConchRun;}}
        //泡泡
        public int AnimationStartMagicBubble {get{return animationStartMagicBubble;}}
        public int AnimationHoldMagicBubble {get{return animationHoldMagicBubble;}}
        public int AnimationMagicBubbleRun {get{return animationMagicBubbleRun;}}
        public int AnimationEndMagicBubble {get{return animationEndMagicBubble;}}
        //炸彈
        public int AnimationStartMagicBomb {get{return animationStartMagicBomb;}}
        public int AnimationEndMagicBomb {get{return animationEndMagicBomb;}}
        public int AnimationEndMagicBombWalk {get{return animationEndMagicBombWalk;}}
        public int AnimationEndMagicBombRun {get{return animationEndMagicBombRun;}}
    #endregion

    #region Camera
        [Header("Camera")]
        [SerializeField]float _cameraSensitivity = 0.8f;
        [SerializeField]private GameObject _freeCamera;
        [SerializeField]private GameObject _aimCamera;
        [SerializeField]private GameObject _aimReticle;

        [SerializeField]private GameObject _aimCamTarget;
        [SerializeField] private GameObject raycastHitPoint;
        [SerializeField] private LayerMask aimColliderMask = new LayerMask();
        [SerializeField] private Transform _firepoint;
        private Vector2 screenCenterPoint = new Vector2(Screen.width/2,Screen.height/2);
        private int _currentCamera = 0; //0 free 1 aim
        private Ray centerRay;

        private bool _isAiming = false;
        public bool IsAiming {get{return _isAiming;}}

        


    #endregion
    #region Dash
        [SerializeField] private float _dashSpeed = 20;
        private bool _isDashing = false;
        public bool IsDashing {get{return _isDashing;} set{_isDashing = value;}}
        public float DashSpeed {get{return _dashSpeed;}}
        public int AnimationDash{get{return animationDash;}}
        public int AnimationDashInAir{get{return animationDashInAir;}}
    #endregion
    #region Animation
        Animator _animator;
        private int currentAnimationState;
        private int animationIdle;
        private int animationWalk;
        private int animationRun;
        private int animationJumpStart;
        private int animationJumpLevel2;
        private int animationJumpEnd;
        private int animationJumpEnd_End;
        private int animationDash;
        private int animationDashInAir;
        private int animationDashInAirLevel2;
        private int animationTalk;
        private int animationStartMagicConch;
        private int animationHoldMagicConch;
        private int animationEndMagicConch;
        private int animationHoldMagicConchRun;
        private int animationStartMagicBubble;
        private int animationHoldMagicBubble;
        private int animationEndMagicBubble;
        private int animationMagicBubbleRun;
        private int animationStartMagicBomb;
        private int animationEndMagicBomb;
        private int animationEndMagicBombWalk;
        private int animationEndMagicBombRun;
        private int animationSlide;
        private int animationStayInAir;
    #endregion
    
    #region State Machine
        PlayerBaseState _currentState;
        PlayerStateFactory _states;
    #endregion

    #region Getters & Setters
        public PlayerBaseState CurrentState {get{return _currentState; } set {_currentState = value;} }
        
        #region player
            public Transform MainCamera {get {return mainCamera;}}
            public CharacterController CharacterController {get{ return _characterController;}}
            public Animator Animator {get{return _animator;}set{_animator = value;}}
            public int CurrentAnimationState {get{return currentAnimationState;} set {currentAnimationState = value;}}
            public Vector3 CurrentMovement {get{return _currentMovement;}}
            public float CurrentMovementX {get{return _currentMovement.x;}}
            public float CurrentMovementZ {get{return _currentMovement.z;}}
        #endregion
        #region Movement
            public float AppliedMovementX {get{ return _appliedMovement.x;}set{_appliedMovement.x = value;}}
            public float AppliedMovementY {get{ return _appliedMovement.y;}set{_appliedMovement.y = value;}}
            public float AppliedMovementZ {get{ return _appliedMovement.z;}set{_appliedMovement.z = value;}}
            public float CurrentSpeed {get{ return _currentSpeed;}set{_currentSpeed = value;}}
            public float WalkingSpeed {get{ return _walkingSpeed;}}
            public float RunningSpeed {get{ return _runningSpeed;}}           
            public float JumpingFriction {get{ return _jumpingFriction;}}           
            public float TargetAngle {get{return _targetAngle;} set{_targetAngle = value;}}
            public Vector3 MoveDir {get{return moveDir; } set{moveDir = value;}}
        #endregion
        #region gravity
            public float GroundGravity {get{return _groundGravity;}}
            public float Gravity {get{return _gravity;}}
            public float FallMultiplier{get{return _fallMultiplier;}}
        #endregion
        #region jump
            public bool IsJumping {get{return _isJumping;}set{_isJumping = value;}}
            public bool IsFalling {get{return _isFalling;} set{_isFalling = value;}}
            public bool IsSecondJumping {get{return _isSecondJumping;} set{_isSecondJumping = value;}}
            public bool CanSecondJump {get{return _canSecondJump;}set{_canSecondJump = value;}}
            public float GravityMovementY {get{ return _gravityMovement.y;}set{_gravityMovement.y = value;}}
            public float InitialJumpVelocity {get{ return _initialJumpVelocity;}}
            public float InitialSecondJumpVelocity {get{ return _initialSecondJumpVelocity;}}

            public int AnimationJumpStart{ get{return animationJumpStart;}}
            public int AnimationJumpEnd{ get{return animationJumpEnd;}}
            public int AnimationJumpLevel2{ get{return animationJumpLevel2;}}
            
        #endregion
        #region Walk
            public int AnimationWalk{ get{return animationWalk;}}
            public bool IsWalking{get{return _isWalking;}}

        #endregion
        #region Run
            public int AnimationRun{ get{return animationRun;}}
            public bool IsRunning{get{return _isRunning;}}

        #endregion
        #region Idle
            public int AnimationIdle{ get{return animationIdle;}}
        #endregion
        #region Shoot
            public int CurrentMagic {get{return _currentMagic;}}

        #endregion

    #endregion
    private void Awake() {
        mainCamera = Camera.main.transform;
        _characterController = GetComponent<CharacterController>();
        _characterController.detectCollisions = enabled;
        _animator = GetComponent<Animator>();

        //setup state
        _states = new PlayerStateFactory(this);
        _currentState = _states.Grounded();
        _currentState.EnterState();

        #region Set Animation HashID
        animationIdle = Animator.StringToHash("Player_Idle");
        animationWalk = Animator.StringToHash("Player_Walk");
        animationRun = Animator.StringToHash("Player_Run");
        animationStayInAir = Animator.StringToHash("Player_StayInAir");
        animationJumpStart = Animator.StringToHash("Player_JumpStart");
        animationJumpLevel2 = Animator.StringToHash("Player_JumpLevel2");
        animationJumpEnd = Animator.StringToHash("Player_JumpEnd");
        animationJumpEnd_End = Animator.StringToHash("Player_JumpEnd_End");
        animationDash = Animator.StringToHash("Player_Dash");
        animationDashInAir = Animator.StringToHash("Player_DashInAir");
        animationDashInAirLevel2 = Animator.StringToHash("Player_DashInAirLevel2");
        animationTalk = Animator.StringToHash("Player_Talk");
        animationSlide = Animator.StringToHash("Player_Slide");
        animationStartMagicConch = Animator.StringToHash("Player_StartMagicConch");
        animationHoldMagicConch = Animator.StringToHash("Player_HoldMagicConch");
        animationEndMagicConch = Animator.StringToHash("Player_EndMagicConch");
        animationHoldMagicConchRun = Animator.StringToHash("Player_HoldMagicConch_Run");
        animationStartMagicBubble = Animator.StringToHash("Player_StartMagicBubble");
        animationHoldMagicBubble = Animator.StringToHash("Player_HoldMagicBubble");
        animationEndMagicBubble = Animator.StringToHash("Player_EndMagicBubble");
        animationMagicBubbleRun = Animator.StringToHash("Player_MagicBubbleRun");
        animationStartMagicBomb = Animator.StringToHash("Player_StartMagicBomb");
        animationEndMagicBomb = Animator.StringToHash("Player_EndMagicBomb");
        animationEndMagicBombWalk = Animator.StringToHash("Player_EndMagicBombWalk");
        animationEndMagicBombRun = Animator.StringToHash("Player_EndMagicBombRun");
        
        //= Animator.StringToHash("Player_");
        #endregion
    
        setUpJumpVariables();
    }
    
    // Start is called before the first frame update
    void Start()
    {
        _currentMagic = PlayerMagicDic["Magic1"];
        //set player position
        Debug.Log(currentRespawnPoint.respawnPosition);
        transform.position = currentRespawnPoint.respawnPosition;
        transform.rotation = currentRespawnPoint.respawnRotation;
        Debug.Log("reset!!");
    }

    // Update is called once per frame
    void Update()
    {
        #region test
        if(minusTest){
            minusTest = false;
            SetPlayerHealth(-20);
            SetCrystalAmount(-20);
            SetSpiritAmount(-100);
        }
        if(addTest){
            addTest = false;
            SetPlayerHealth(40);
            SetCrystalAmount(100);
            SetSpiritAmount(50);
        }
        #endregion

        
        if(FreezeCheck()) return;

        
        HandleCamera();
        if(InputSystem.instance.IsMagicPlusStatusPressed){
            InputSystem.instance.IsMagicPlusStatusPressed = false;
            ChangeMagicState(true);
        }
        else if(InputSystem.instance.IsMagicMinusStatusPressed){
            InputSystem.instance.IsMagicMinusStatusPressed = false;
            ChangeMagicState(false);
        }

        if(_currentBombLeft<=0){
            _currentBombLeft = 5;
            MagicCDManager.instance.StartBombCD();
        }
        
        _currentMovement = InputSystem.instance.GetCurrentMovement();
        
        if(InputSystem.instance.IsDashPressed){
            InputSystem.instance.IsDashPressed = false;
            _isDashing = true;
        }
        if(_currentMovement.magnitude>0.7){
            _isRunning = true;
            _isWalking = false;
        }
        else if(_currentMovement.magnitude>0){
            _isRunning = false;
            _isWalking = true;
        }
        else{
            _isWalking = false;
            _isRunning = false;
        }
        
        //camera + movingDirection
        if(_currentCamera == 1){ //Aim
            _isAiming = true;
            centerRay = Camera.main.ScreenPointToRay(screenCenterPoint);
            if(Physics.Raycast(centerRay, out RaycastHit raycastHit, 999f,aimColliderMask )){
                raycastHitPoint.transform.position = raycastHit.point;
            }
            _firepoint.transform.LookAt(raycastHitPoint.transform);


            //相機轉不了 轉woomi
            
            _aimCamTarget.transform.eulerAngles = _aimCamTarget.transform.eulerAngles + new Vector3( InputSystem.instance.CurrentAimCameraMovement.y*_cameraSensitivity,0,0);
            transform.eulerAngles = transform.eulerAngles + new Vector3(0,InputSystem.instance.CurrentAimCameraMovement.x*_cameraSensitivity,0);
            //-10 - 25
            
            //設定上下的範圍限制
           
            //if(transform.eulerAngles.x >= 20 && transform.eulerAngles.x <= 340) transform.eulerAngles = new Vector3(340,transform.eulerAngles.y,transform.eulerAngles.z);
            if(_aimCamTarget.transform.eulerAngles.x >= 30 && _aimCamTarget.transform.eulerAngles.x <= 330) _aimCamTarget.transform.eulerAngles = new Vector3(330,_aimCamTarget.transform.eulerAngles.y,_aimCamTarget.transform.eulerAngles.z);
            //else if(transform.eulerAngles.x >= 5 && transform.eulerAngles.x < 20) transform.eulerAngles = new Vector3(5,transform.eulerAngles.y,transform.eulerAngles.z);
            else if(_aimCamTarget.transform.eulerAngles.x >= 25 && _aimCamTarget.transform.eulerAngles.x < 30) _aimCamTarget.transform.eulerAngles = new Vector3(25,_aimCamTarget.transform.eulerAngles.y,_aimCamTarget.transform.eulerAngles.z);
            
        }
        else{ //free look
            _isAiming = false;
            _firepoint.transform.eulerAngles = transform.eulerAngles;
            transform.eulerAngles = new Vector3(0,transform.eulerAngles.y,transform.eulerAngles.z);

            if(_currentMovement.magnitude>0)handleRoatation();
        }

        _currentMovement = _currentMovement.normalized;
            _targetAngle = Mathf.Atan2(_currentMovement.x, _currentMovement.z) * Mathf.Rad2Deg + mainCamera.eulerAngles.y;
            
            

            
            moveDir = Quaternion.Euler(0f, _targetAngle, 0f) * Vector3.forward;
            moveDir = moveDir.normalized;
            // _appliedMovement.x = (moveDir.normalized*_currentSpeed).x;
            // _appliedMovement.z = (moveDir.normalized*_currentSpeed).z;

        

        //state update
        _currentState.UpdateStates();

        _characterController.Move(_appliedMovement*Time.deltaTime);
    }


    void setUpJumpVariables(){
        float timeToApex = _maxJumpTime/2;
        _gravity = (-2 * _maxJumpHeight) / Mathf.Pow(timeToApex, 2);  // h = 1/2 * g * t平方 (t = 到達最高點需要的時間)
        _initialJumpVelocity = (2 * _maxJumpHeight) / timeToApex; //v = gt 約分之後長這樣 應該也可寫成 gravity * timeToApex
        float timeToSecondApex = _maxSecondJumpTime/2;
        //gravity = (-2 * maxSecondJumpHeight) / Mathf.Pow(timeToSecondApex, 2);  // h = 1/2 * g * t平方 (t = 到達最高點需要的時間)
        _initialSecondJumpVelocity = (2 * _maxSecondJumpHeight) / timeToSecondApex; //v = gt 約分之後長這樣 應該也可寫成 gravity * timeToApex
    }

    void handleRoatation(){
        float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, _targetAngle, ref _turnSmoothVelocity, _turnSmoothTime);
        transform.rotation = Quaternion.Euler(0f, angle, 0f);
    }

    void ChangeAnimationState(int newAnimationState)
    {
       if(newAnimationState == currentAnimationState) {
            return; //一樣的話就不重新開始播ㄌ
        }
        _animator.Play(newAnimationState);
        
        currentAnimationState = newAnimationState;
    }

    void ChangeMagicState(bool isPlus){
        if(isPlus){
            //_currentMagic ++;
            _currentMagic = (_currentMagic < PlayerMagicDic.Count) ?_currentMagic +1:PlayerMagicDic["Magic1"];
        }
        else {
            _currentMagic = (_currentMagic > 1) ?_currentMagic -1:PlayerMagicDic["Magic3"];
        }
        MagicCDManager.instance.SetCurrentMagic(_currentMagic);
        ResetShootStatus();
    }

    void ResetShootStatus(){
        _isShooting = false;
        _isHolding = false;
        InputSystem.instance.IsShootPressed = false;
        _holdingTime = 0;
        PlayerMagicController.instance.MagicConchBornTime = 2f;
    }

    void HandleCamera(){
        if (InputSystem.instance.IsAimPressed) //換相機
        {
            _currentCamera = 1;
            _aimCamera.SetActive(true);
            _aimReticle.SetActive(true);
            raycastHitPoint.SetActive(true);
        }
        else
        {
            _currentCamera = 0;
            _aimCamera.SetActive(false);
            _aimReticle.SetActive(false);
            raycastHitPoint.SetActive(false);
        }
    }

    bool FreezeCheck(){
        if(MenuManager.instance.inMenu){
            
            return true; //在選單就不要動!
        }
        else if(InventoryManager.instance.inBackpack){
            return true; //在背包就不要動!
        }
        else if(DialogueManager.instance.inDialogue){
            ChangeAnimationState(animationTalk);
            return true; //在對話就不要動!
        } 
        else return false;
    }

    void SetPlayerHealth(int modifyAmount){
        int originHealth = health;
        health += modifyAmount;
        if(health>=100) health = 100;
        //else if(health <=0) player is dead;

        PlayerInfoManager.instance.StartSetPlayerHealth(originHealth,health);
    }
    void SetCrystalAmount(int modifyAmount){
        int originCrystal = crystalAmount;
        crystalAmount += modifyAmount;
        if(crystalAmount<0) {
            crystalAmount = originCrystal;
            Debug.Log("you don't have enough crystal!");
        }
        else PlayerInfoManager.instance.StartSetCrystalAmount(originCrystal,crystalAmount);
        

    }

    void SetSpiritAmount(int modifyAmount){
        int originSpirit = spiritAmount;
        spiritAmount += modifyAmount;
        if(spiritAmount<0) {
            spiritAmount = originSpirit;
            Debug.Log("you don't have enough spirit!");
        }
        else PlayerInfoManager.instance.StartSetSpiritAmount(originSpirit,spiritAmount);
    }
    
}
