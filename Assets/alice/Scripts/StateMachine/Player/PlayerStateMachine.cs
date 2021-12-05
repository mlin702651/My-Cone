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
        private int animationStartMagicBubble;
        private int animationHoldMagicBubble;
        private int animationEndMagicBubble;
        private int animationMagicBomb;
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
        //gravity
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
        animationStartMagicBubble = Animator.StringToHash("Player_StartMagicBubble");
        animationHoldMagicBubble = Animator.StringToHash("Player_HoldMagicBubble");
        animationEndMagicBubble = Animator.StringToHash("Player_EndMagicBubble");
        animationMagicBomb = Animator.StringToHash("Player_MagicBomb");
        
        //= Animator.StringToHash("Player_");
        #endregion
    
        setUpJumpVariables();
    }
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        _currentMovement = InputSystem.instance.GetCurrentMovement();

        
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

        _currentMovement = _currentMovement.normalized;
        _targetAngle = Mathf.Atan2(_currentMovement.x, _currentMovement.z) * Mathf.Rad2Deg + mainCamera.eulerAngles.y;
        
        if(_currentMovement.magnitude>0)handleRoatation();

        
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

    
}
