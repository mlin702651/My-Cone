using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRunState : PlayerBaseState
{
    public PlayerRunState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory)
    :base (currentContext, playerStateFactory){} //把這個傳去base state
    public override void EnterState(){
        //ChangeAnimationState(Ctx.AnimationRun);
        Debug.Log("Start Run");

    }
    public override void UpdateState(){
        CheckSwitchStates();
        Ctx.CurrentSpeed = Ctx.RunningSpeed;
        if(Ctx.IsJumping) Ctx.CurrentSpeed  = Ctx.CurrentSpeed * Ctx.JumpingFriction;
        Ctx.AppliedMovementX = (Ctx.MoveDir*Ctx.CurrentSpeed).x;
        Ctx.AppliedMovementZ = (Ctx.MoveDir*Ctx.CurrentSpeed).z;

        if(Ctx.CurrentAnimationState == Ctx.AnimationHoldMagicConch) ChangeAnimationState(Ctx.AnimationHoldMagicConchRun);
        if(Ctx.IsShooting) return;

        if(Ctx.IsJumping) return;
        else if(Ctx.Animator.GetCurrentAnimatorStateInfo(0).shortNameHash == Ctx.AnimationJumpEnd && Ctx.IsJumping) return; //如果還在降落就播完動畫
        ChangeAnimationState(Ctx.AnimationRun);

    }
    public override void ExitState(){}
    public override void CheckSwitchStates(){
        if(Ctx.IsWalking){
            SwitchState(Factory.Walk());
        }
        else if(!Ctx.IsWalking && !Ctx.IsRunning){ 
            //if(Ctx.Animator.GetCurrentAnimatorStateInfo(0).shortNameHash == Ctx.AnimationJumpEnd) return; //如果還在降落就播完動畫
            SwitchState(Factory.Idle());
        }
    }
    public override void InitializedSubState(){}
}
