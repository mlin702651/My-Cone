using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
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
        Vector3 _currentMoveMent;
        Vector3 _gravityMoveMent = new Vector3(0,0,0);
        Vector3 _appliedMoveMent = new Vector3(0,0,0);  //跟跳躍有關的
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
    
    private void Awake() {
        mainCamera = Camera.main.transform;
        _characterController = GetComponent<CharacterController>();
        _characterController.detectCollisions = enabled;
        _animator = GetComponent<Animator>();
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
        _currentMoveMent = InputSystem.instance.GetCurrentMovement();
        handleAnimation();
        //handleRoatation();
        
        if(_currentMoveMent.magnitude>0.7){
            _currentSpeed = _runningSpeed;
            _isRunning = true;
            _isWalking = false;
        }
        else if(_currentMoveMent.magnitude>0){
            _currentSpeed = _walkingSpeed;
            _isRunning = false;
            _isWalking = true;
        }
        else{
            _currentSpeed = 0;
            _isWalking = false;
            _isRunning = false;
        }

        
           
        //_currentMoveMent = _currentMoveMent.normalized;
        _targetAngle = Mathf.Atan2(_currentMoveMent.x, _currentMoveMent.z) * Mathf.Rad2Deg + mainCamera.eulerAngles.y;
        
        if(_currentMoveMent.magnitude>0)handleRoatation();
        
        moveDir = Quaternion.Euler(0f, _targetAngle, 0f) * Vector3.forward;
        if(_isJumping) _currentSpeed  = _currentSpeed * _jumpingFriction;
        _appliedMoveMent.x = (moveDir.normalized*_currentSpeed).x;
        _appliedMoveMent.z = (moveDir.normalized*_currentSpeed).z;
        
        _characterController.Move(_appliedMoveMent*Time.deltaTime);

        handleGravity();
        handleJump();

        
        
    }

    
    void handleGravity(){

        _isFalling = _gravityMoveMent.y <= -7.5 && !_characterController.isGrounded; //|| !InputSystem.instance.isJumpPressed)
        
        if(_characterController.isGrounded){
            
            _gravityMoveMent.y = _groundGravity;
            _appliedMoveMent.y = _groundGravity;
        }
        else if(_isFalling){
            float previousYVelocity = _gravityMoveMent.y;
            _gravityMoveMent.y = _gravityMoveMent.y + (_gravity* _fallMultiplier * Time.deltaTime);
            _appliedMoveMent.y = Mathf.Max((previousYVelocity + _gravityMoveMent.y) * 0.5f, -20.0f); //從高處掉下來的時候不要掉太快
        }
        else{
            float previousYVelocity = _gravityMoveMent.y;
            _gravityMoveMent.y = _gravityMoveMent.y + (_gravity * Time.deltaTime);
            _appliedMoveMent.y = (previousYVelocity + _gravityMoveMent.y) * 0.5f;

        }
    }

    void setUpJumpVariables(){
        float timeToApex = _maxJumpTime/2;
        _gravity = (-2 * _maxJumpHeight) / Mathf.Pow(timeToApex, 2);  // h = 1/2 * g * t平方 (t = 到達最高點需要的時間)
        _initialJumpVelocity = (2 * _maxJumpHeight) / timeToApex; //v = gt 約分之後長這樣 應該也可寫成 gravity * timeToApex
        float timeToSecondApex = _maxSecondJumpTime/2;
        //gravity = (-2 * maxSecondJumpHeight) / Mathf.Pow(timeToSecondApex, 2);  // h = 1/2 * g * t平方 (t = 到達最高點需要的時間)
        _initialSecondJumpVelocity = (2 * _maxSecondJumpHeight) / timeToSecondApex; //v = gt 約分之後長這樣 應該也可寫成 gravity * timeToApex
    }

    void handleJump(){
        if(_canSecondJump && InputSystem.instance.IsJumpPressed){
            InputSystem.instance.IsJumpPressed = false;
            _canSecondJump = false;
            _isSecondJumping = true;
            _gravityMoveMent.y = _initialSecondJumpVelocity; 
            _appliedMoveMent.y = _initialSecondJumpVelocity; 
            Debug.Log("jump 2!");
        }
        else if(_characterController.isGrounded){
            _canSecondJump = false;
            _isSecondJumping = false;
        }

        if(!_isJumping && _characterController.isGrounded && InputSystem.instance.IsJumpPressed){
            InputSystem.instance.IsJumpPressed = false;
            _isJumping = true;
            _canSecondJump = true;
            _gravityMoveMent.y = _initialJumpVelocity; 
            _appliedMoveMent.y = _initialJumpVelocity; 
        }
        else if(_isJumping && _characterController.isGrounded && !InputSystem.instance.IsJumpPressed){
            _isJumping = false;
        }


        
    }
    
    // void handleRoatation(){
    //     Vector3 positionToLookAt;
    //     positionToLookAt = currentMoveMent;
    //     positionToLookAt.y = 0;
    //     //current rotation
    //     Quaternion currentRotation = transform.rotation;
    //     //new rotation where player is pressing
    //     Quaternion targetRotation = Quaternion.LookRotation(positionToLookAt);
    //     if(InputSystem.instance.isMovementPressed) transform.rotation = Quaternion.Slerp(currentRotation,targetRotation,rotationFactorPerFrame*Time.deltaTime);
    // }

    void handleRoatation(){
        float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, _targetAngle, ref _turnSmoothVelocity, _turnSmoothTime);
        transform.rotation = Quaternion.Euler(0f, angle, 0f);
    }
    void handleAnimation(){
        if(_isFalling){
            ChangeAnimationState(animationJumpEnd);
        }
        else if(_isSecondJumping  && !_characterController.isGrounded){
            ChangeAnimationState(animationJumpLevel2);
        }
        else if(_isJumping && !_characterController.isGrounded){
            ChangeAnimationState(animationJumpStart);
        }
        else if(_isRunning){
            ChangeAnimationState(animationRun);
        }
        else if(_isWalking){
            ChangeAnimationState(animationWalk);
        }
        else { 
            if(_animator.GetCurrentAnimatorStateInfo(0).shortNameHash == animationJumpEnd) return; //如果還在降落就播完動畫
            ChangeAnimationState(animationIdle);
        }
    }

    void ChangeAnimationState(int newAnimationState)
    {
       if(newAnimationState == currentAnimationState) {
            return; //一樣的話就不重新開始播ㄌ
        }
        _animator.Play(newAnimationState);
        
        currentAnimationState = newAnimationState;
    }

    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        Rigidbody body = hit.collider.attachedRigidbody;

        // no rigidbody
        if (body == null || body.isKinematic)
            return;

        // We dont want to push objects below us
        if (hit.moveDirection.y < -0.3f)
            return;

        // Calculate push direction from move direction,
        // we only push objects to the sides never up and down
        Vector3 pushDir = new Vector3(hit.moveDirection.x, 0, hit.moveDirection.z);

        // If you know how fast your character is trying to move,
        // then you can also multiply the push velocity by that.

        // Apply the push
        body.velocity = pushDir * _pushPower;
    }
}
