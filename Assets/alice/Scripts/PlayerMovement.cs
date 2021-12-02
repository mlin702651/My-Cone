using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    
    Transform mainCamera;
    #region Player
        CharacterController _characterController;
        [Header("Interact with rigid body")]
        [SerializeField] float pushPower = 2.0F;
    #endregion
    #region Move
    [Header("Movement")]
        [SerializeField]float walkingSpeed = 2.0f;
        [SerializeField]float runningSpeed = 5.0f;
        [SerializeField]float jumpingFriction = 0.8f;
        //[SerializeField]float rotationFactorPerFrame = 1.0f;
        [SerializeField]private float turnSmoothTime = 0.1f;
        Vector3 currentMoveMent;
        Vector3 gravityMoveMent = new Vector3(0,0,0);
        Vector3 appliedMoveMent = new Vector3(0,0,0);  //跟跳躍有關的
        float currentSpeed = 2.0f;
        private float turnSmoothVelocity;
        bool isRunning = false;
        bool isWalking = false;

    #endregion

    #region Jump
        [Header("Jump")]
        [SerializeField]float maxJumpHeight = 1.0f;
        [SerializeField]float maxJumpTime = 0.5f;
        [SerializeField]float maxSecondJumpHeight = 1.0f;
        [SerializeField]float maxSecondJumpTime = 0.5f;
        [SerializeField]float fallMultiplier = 2.0f;
        float groundGravity = -0.5f;
        float gravity = -9.8f;
        float initialJumpVelocity;
        float initialSecondJumpVelocity;
        bool isJumping = false;
        bool isSecondJumping = false;

        bool isFalling = false;
        bool canSecondJump = false;
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
        currentMoveMent = InputSystem.instance.GetCurrentMovement();
        handleAnimation();
        //handleRoatation();
        
        if(currentMoveMent.magnitude>0.7){
            currentSpeed = runningSpeed;
            isRunning = true;
            isWalking = false;
        }
        else if(currentMoveMent.magnitude>0.05){
            currentSpeed = walkingSpeed;
            isRunning = false;
            isWalking = true;
        }
        else{
            currentSpeed = 0;
            isWalking = false;
            isRunning = false;
        }

        if(currentMoveMent.magnitude>0.05){
            currentMoveMent = currentMoveMent.normalized;
            float targetAngle = Mathf.Atan2(currentMoveMent.x, currentMoveMent.z) * Mathf.Rad2Deg + mainCamera.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);
            
            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            if(isJumping) currentSpeed  = currentSpeed * jumpingFriction;
            _characterController.Move(moveDir.normalized*Time.deltaTime*currentSpeed);
        }

        _characterController.Move(appliedMoveMent*Time.deltaTime);
        handleGravity();
        handleJump();

        
        
    }

    
    void handleGravity(){

        isFalling = gravityMoveMent.y <= -7.5 && !_characterController.isGrounded; //|| !InputSystem.instance.isJumpPressed)
        
        if(_characterController.isGrounded){
            
            gravityMoveMent.y = groundGravity;
            appliedMoveMent.y = groundGravity;
        }
        else if(isFalling){
            float previousYVelocity = gravityMoveMent.y;
            gravityMoveMent.y = gravityMoveMent.y + (gravity* fallMultiplier * Time.deltaTime);
            appliedMoveMent.y = Mathf.Max((previousYVelocity + gravityMoveMent.y) * 0.5f, -20.0f); //從高處掉下來的時候不要掉太快
        }
        else{
            float previousYVelocity = gravityMoveMent.y;
            gravityMoveMent.y = gravityMoveMent.y + (gravity * Time.deltaTime);
            appliedMoveMent.y = (previousYVelocity + gravityMoveMent.y) * 0.5f;

        }
    }

    void setUpJumpVariables(){
        float timeToApex = maxJumpTime/2;
        gravity = (-2 * maxJumpHeight) / Mathf.Pow(timeToApex, 2);  // h = 1/2 * g * t平方 (t = 到達最高點需要的時間)
        initialJumpVelocity = (2 * maxJumpHeight) / timeToApex; //v = gt 約分之後長這樣 應該也可寫成 gravity * timeToApex
        float timeToSecondApex = maxSecondJumpTime/2;
        //gravity = (-2 * maxSecondJumpHeight) / Mathf.Pow(timeToSecondApex, 2);  // h = 1/2 * g * t平方 (t = 到達最高點需要的時間)
        initialSecondJumpVelocity = (2 * maxSecondJumpHeight) / timeToSecondApex; //v = gt 約分之後長這樣 應該也可寫成 gravity * timeToApex
    }

    void handleJump(){
        if(canSecondJump && InputSystem.instance.isJumpPressed){
            InputSystem.instance.isJumpPressed = false;
            canSecondJump = false;
            isSecondJumping = true;
            gravityMoveMent.y = initialSecondJumpVelocity; 
            appliedMoveMent.y = initialSecondJumpVelocity; 
            Debug.Log("jump 2!");
        }
        else if(_characterController.isGrounded){
            canSecondJump = false;
            isSecondJumping = false;
        }

        if(!isJumping && _characterController.isGrounded && InputSystem.instance.isJumpPressed){
            InputSystem.instance.isJumpPressed = false;
            isJumping = true;
            canSecondJump = true;
            gravityMoveMent.y = initialJumpVelocity; 
            appliedMoveMent.y = initialJumpVelocity; 
        }
        else if(isJumping && _characterController.isGrounded && !InputSystem.instance.isJumpPressed){
            isJumping = false;
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
    void handleAnimation(){
        if(isFalling){
            ChangeAnimationState(animationJumpEnd);
        }
        else if(isSecondJumping  && !_characterController.isGrounded){
            ChangeAnimationState(animationJumpLevel2);
        }
        else if(isJumping && !_characterController.isGrounded){
            ChangeAnimationState(animationJumpStart);
        }
        else if(isRunning){
            ChangeAnimationState(animationRun);
        }
        else if(isWalking){
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
        body.velocity = pushDir * pushPower;
    }
}
