using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    
    #region Player
    CharacterController _woomiCharacterController;
    #endregion
    Transform mainCamera;
    #region Move
    [Header("Movement")]
    Vector3 currentMoveMent;
    Vector3 gravityMoveMent = new Vector3(0,0,0);
    float currentSpeed = 2.0f;
    [SerializeField]float walkingSpeed = 2.0f;
    [SerializeField]float runningSpeed = 5.0f;
    [SerializeField]float rotationFactorPerFrame = 1.0f;
    private float turnSmoothVelocity;
    [SerializeField]private float turnSmoothTime = 0.1f;
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
        _woomiCharacterController = GetComponent<CharacterController>();
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
    }
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        currentMoveMent = InputSystem.instance.GetCurrentMovement();
        handleGravity();
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
            Debug.Log(currentSpeed);
            _woomiCharacterController.Move(moveDir.normalized*Time.deltaTime*currentSpeed);
        }
        
        
    }

    
    void handleGravity(){
        if(_woomiCharacterController.isGrounded){
            float groundGravity = -0.5f;
            gravityMoveMent.y = groundGravity;
        }
        else{
            float gravity = -9.8f;
            gravityMoveMent.y += gravity;

        }
        _woomiCharacterController.Move(gravityMoveMent);
    }
    
    void handleRoatation(){
        Vector3 positionToLookAt;
        positionToLookAt = currentMoveMent;
        positionToLookAt.y = 0;
        //current rotation
        Quaternion currentRotation = transform.rotation;
        //new rotation where player is pressing
        Quaternion targetRotation = Quaternion.LookRotation(positionToLookAt);
        if(InputSystem.instance.isMovementPressed) transform.rotation = Quaternion.Slerp(currentRotation,targetRotation,rotationFactorPerFrame*Time.deltaTime);
    }
    void handleAnimation(){
        if(InputSystem.instance.isMovementPressed && currentMoveMent.magnitude>0.7){
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
}
