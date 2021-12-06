using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWalkState : PlayerBaseState
{
    public PlayerWalkState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory)
    :base (currentContext, playerStateFactory){} //把這個傳去base state
    public override void EnterState(){
        //ChangeAnimationState(Ctx.AnimationWalk);
        Debug.Log("Start Walk");
    }
    public override void UpdateState(){
        CheckSwitchStates();
        Ctx.CurrentSpeed = Ctx.WalkingSpeed;
        if(Ctx.IsJumping) Ctx.CurrentSpeed  = Ctx.CurrentSpeed * Ctx.JumpingFriction;
        Ctx.AppliedMovementX = (Ctx.MoveDir*Ctx.CurrentSpeed).x;
        Ctx.AppliedMovementZ = (Ctx.MoveDir*Ctx.CurrentSpeed).z;

        if(Ctx.CurrentAnimationState == Ctx.AnimationHoldMagicConch) ChangeAnimationState(Ctx.AnimationHoldMagicConchRun);
        else if(Ctx.CurrentAnimationState == Ctx.AnimationEndMagicConch){
            Ctx.AppliedMovementX = 0;
            Ctx.AppliedMovementZ = 0;
        } 
        else if(Ctx.CurrentAnimationState == Ctx.AnimationHoldMagicBubble) ChangeAnimationState(Ctx.AnimationMagicBubbleRun);
        else if(Ctx.CurrentAnimationState == Ctx.AnimationEndMagicBubble){
            Ctx.AppliedMovementX = 0;
            Ctx.AppliedMovementZ = 0;
        } 
        
        if(Ctx.IsShooting) return;
        
        if(Ctx.IsJumping) return;
        else if(Ctx.Animator.GetCurrentAnimatorStateInfo(0).shortNameHash == Ctx.AnimationJumpEnd || Ctx.IsJumping) return; //如果還在降落就播完動畫
        ChangeAnimationState(Ctx.AnimationWalk);

    }
    public override void ExitState(){}
    public override void CheckSwitchStates(){
        if(Ctx.IsRunning){
            SwitchState(Factory.Run());
        }
        else if(!Ctx.IsWalking && !Ctx.IsRunning){ 
            //if(Ctx.Animator.GetCurrentAnimatorStateInfo(0).shortNameHash == Ctx.AnimationJumpEnd) return; //如果還在降落就播完動畫
            SwitchState(Factory.Idle());
        }
    }
    public override void InitializedSubState(){}
}
