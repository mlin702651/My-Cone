using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJumpState : PlayerBaseState
{
    public PlayerJumpState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory)
    :base (currentContext, playerStateFactory){
        IsRootState = true;
        InitializedSubState();
    } //把這個傳去base state
    public override void EnterState(){
        HandleJump();
        Debug.Log("Start Jump");

    }
    public override void UpdateState(){
        CheckSwitchStates();
        HandleSecondJump();
        HandleGravity();

    }
    public override void ExitState(){
            Ctx.IsJumping = false;

    }
    public override void CheckSwitchStates(){
        if(Ctx.CharacterController.isGrounded){
            SwitchState(Factory.Grounded());
        }
    }
    public override void InitializedSubState(){
        if(Ctx.IsRunning){
            SetSubState(Factory.Run());
        }
        else if(Ctx.IsWalking){
            SetSubState(Factory.Walk());
        }
        else { 
            //if(Ctx.Animator.GetCurrentAnimatorStateInfo(0).shortNameHash == Ctx.AnimationJumpEnd) return; //如果還在降落就播完動畫
            SetSubState(Factory.Idle());
        }

    }
    void HandleJump(){
        
        
        
        ChangeAnimationState(Ctx.AnimationJumpStart);
        InputSystem.instance.IsJumpPressed = false;
            Ctx.IsJumping = true;
            Ctx.CanSecondJump = true;
            Ctx.GravityMovementY = Ctx.InitialJumpVelocity; 
            Ctx.AppliedMovementY = Ctx.InitialJumpVelocity; 
    }

    void HandleGravity(){
        Ctx.IsFalling = Ctx.GravityMovementY <= -3.7 && !Ctx.CharacterController.isGrounded; //|| !InputSystem.instance.isJumpPressed)
        
        
        if(Ctx.IsFalling){
            ChangeAnimationState(Ctx.AnimationJumpEnd);
            float previousYVelocity = Ctx.GravityMovementY;
            Ctx.GravityMovementY = Ctx.GravityMovementY + (Ctx.Gravity* Ctx.FallMultiplier * Time.deltaTime);
            Ctx.AppliedMovementY = Mathf.Max((previousYVelocity + Ctx.GravityMovementY) * 0.5f, -20.0f); //從高處掉下來的時候不要掉太快
        }
        else{
            float previousYVelocity = Ctx.GravityMovementY;
            Ctx.GravityMovementY = Ctx.GravityMovementY + (Ctx.Gravity * Time.deltaTime);
            Ctx.AppliedMovementY = (previousYVelocity + Ctx.GravityMovementY) * 0.5f;

        }
    }

    void HandleSecondJump(){
        if(Ctx.CanSecondJump && InputSystem.instance.IsJumpPressed){
            InputSystem.instance.IsJumpPressed = false;
            ChangeAnimationState(Ctx.AnimationJumpLevel2);
            Ctx.CanSecondJump = false;
            Ctx.IsSecondJumping = true;
            Ctx.GravityMovementY = Ctx.InitialSecondJumpVelocity; 
            Ctx.AppliedMovementY = Ctx.InitialSecondJumpVelocity; 
            Debug.Log("Start jump 2!");
        }
        else if(Ctx.CharacterController.isGrounded){
            Ctx.CanSecondJump = false;
            Ctx.IsSecondJumping = false;
        }
    }
}
