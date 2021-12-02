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
        //[SerializeField]float rotationFactorPerFrame = 1.0f;
        [SerializeField]private float turnSmoothTime = 0.1f;
        Vector3 currentMoveMent;
        Vector3 gravityMoveMent = new Vector3(0,0,0);
        float currentSpeed = 2.0f;
        private float turnSmoothVelocity;
    #endregion

    #region Jump
        [Header("Jump")]
        [SerializeField]float maxJumpHeight = 1.0f;
        [SerializeField]float maxJumpTime = 0.5f;
        [SerializeField]float fallMultiplier = 2.0f;
        float groundGravity = -0.5f;
        float gravity = -9.8f;
        float initialJumpVelocity;
        bool isJumping = false;

        bool isFalling = false;
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
        }
        else if(currentMoveMent.magnitude>0){
            currentSpeed = walkingSpeed;
        }
        else currentSpeed = 0;

        if(currentMoveMent.magnitude>0.05){
            currentMoveMent = currentMoveMent.normalized;
            float targetAngle = Mathf.Atan2(currentMoveMent.x, currentMoveMent.z) * Mathf.Rad2Deg + mainCamera.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);
            
            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            _characterController.Move(moveDir.normalized*Time.deltaTime*currentSpeed);
        }

        _characterController.Move(gravityMoveMent*Time.deltaTime);
        handleGravity();
        handleJump();

        
        
    }

    
    void handleGravity(){

        isFalling = gravityMoveMent.y <= -10 && !_characterController.isGrounded; //|| !InputSystem.instance.isJumpPressed)
        
        if(_characterController.isGrounded){
            
            gravityMoveMent.y = groundGravity;
        }
        else if(isFalling){
            float previousYVelocity = gravityMoveMent.y;
            float newYVelocity = gravityMoveMent.y + (gravity* fallMultiplier * Time.deltaTime);
            float nextYVelocity = Mathf.Max((previousYVelocity + newYVelocity) * 0.5f, -20.0f); //從高處掉下來的時候不要掉太快
            gravityMoveMent.y = nextYVelocity;
        }
        else{
            float previousYVelocity = gravityMoveMent.y;
            float newYVelocity = gravityMoveMent.y + (gravity * Time.deltaTime);
            float nextYVelocity = (previousYVelocity + newYVelocity) * 0.5f;
            gravityMoveMent.y = nextYVelocity;
            //gravityMoveMent.y += gravity*Time.deltaTime;

        }
        //_characterController.Move(gravityMoveMent*Time.deltaTime);
    }

    void setUpJumpVariables(){
        float timeToApex = maxJumpTime/2;
        gravity = (-2 * maxJumpHeight) / Mathf.Pow(timeToApex, 2);  // h = 1/2 * g * t平方 (t = 到達最高點需要的時間)
        initialJumpVelocity = (2 * maxJumpHeight) / timeToApex; //v = gt 約分之後長這樣 應該也可寫成 gravity * timeToApex
    }

    void handleJump(){
        if(!isJumping && _characterController.isGrounded && InputSystem.instance.isJumpPressed){
            isJumping = true;
            gravityMoveMent.y = initialJumpVelocity * 0.5f; //還不知道為甚麼要除以二
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
        else if(isJumping && !_characterController.isGrounded){
            ChangeAnimationState(animationJumpStart);
        }
        else if(InputSystem.instance.isMovementPressed && currentMoveMent.magnitude>0.7){
            ChangeAnimationState(animationRun);
        }
        else if(InputSystem.instance.isMovementPressed){
            ChangeAnimationState(animationWalk);
        }
        else { 
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
