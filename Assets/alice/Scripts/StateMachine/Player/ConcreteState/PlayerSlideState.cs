using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSlideState : PlayerBaseState
{
    public PlayerSlideState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory)
    :base (currentContext, playerStateFactory){} //把這個傳去base state
    public override void EnterState(){
        ChangeAnimationState(Ctx.AnimationSlideStart);
        //Debug.Log("Start Slide");

    }

    public override void UpdateState(){
        
        CheckSwitchStates();
        Ctx.CurrentSpeed = Ctx.SlideSpeed;
        //if(Ctx.IsAiming)Ctx.CurrentSpeed = Ctx.WalkingSpeed;
        if(Ctx.IsJumping) Ctx.CurrentSpeed  = Ctx.CurrentSpeed * Ctx.JumpingFriction;

        if(Ctx.CurrentMovement.magnitude >0){
            Ctx.AppliedMovementX = (Ctx.MoveDir*Ctx.CurrentSpeed).x;
            Ctx.AppliedMovementZ = (Ctx.MoveDir*Ctx.CurrentSpeed).z;
        }
        else{
            Ctx.AppliedMovementX = 0;
            Ctx.AppliedMovementZ = 0;
        }

        Ctx.AppliedMovementY = -1.5f; 

        

        if(Ctx.Animator.GetCurrentAnimatorStateInfo(0).shortNameHash == Ctx.AnimationSlideStart) return; //如果還在降落就播完動畫
        ChangeAnimationState(Ctx.AnimationSlide);
        

    }
    public override void ExitState(){}
    public override void CheckSwitchStates(){
        
        Debug.Log(Ctx.IsSliding);
        if(!Ctx.IsSliding){
            SwitchState(Factory.Idle());
            //Debug.Log("slide to idle");
        }
        // if(Ctx.IsDashing){
        //     SwitchState(Factory.Dash());
        // }
        // else if(Ctx.IsWalking){
        //     SwitchState(Factory.Walk());
        // }
        // else if(!Ctx.IsWalking && !Ctx.IsRunning){ 
        //     //if(Ctx.Animator.GetCurrentAnimatorStateInfo(0).shortNameHash == Ctx.AnimationJumpEnd) return; //如果還在降落就播完動畫
        //     SwitchState(Factory.Idle());
        // }
    }
    public override void InitializedSubState(){}
}
