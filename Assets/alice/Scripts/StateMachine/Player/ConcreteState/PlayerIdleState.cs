using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIdleState :PlayerBaseState
{
    public PlayerIdleState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory)
    :base (currentContext, playerStateFactory){} //把這個傳去base state
    public override void EnterState(){
        
        
        //Debug.Log("Start Idle");
        
    }
    public override void UpdateState(){
        CheckSwitchStates();
        Ctx.AppliedMovementX = 0;
        Ctx.AppliedMovementZ = 0;
        if(Ctx.IsShooting||Ctx.CurrentAnimationState == Ctx.AnimationHoldMagicBubble ||Ctx.CurrentAnimationState == Ctx.AnimationEndMagicBomb||Ctx.CurrentAnimationState == Ctx.AnimationEndMagicConch) return;
        if(Ctx.CurrentAnimationState == Ctx.AnimationJumpEnd || Ctx.IsJumping) return; //如果還在降落就播完動畫

        ChangeAnimationState(Ctx.AnimationIdle);
    }
    public override void ExitState(){}
    public override void CheckSwitchStates(){
        if(Ctx.CurrentAnimationState == Ctx.AnimationStartMagicConch || Ctx.CurrentAnimationState == Ctx.AnimationStartMagicBubble ){
            return;
        }
        if(Ctx.IsSliding&&!Ctx.CharacterController.isGrounded){
            SwitchState(Factory.Slide());
        }
        
        if(Ctx.IsDashing){
            SwitchState(Factory.Dash());
        }
        else if(Ctx.IsRunning){
            SwitchState(Factory.Run());
        }
        else if(Ctx.IsWalking){
            SwitchState(Factory.Walk());
        }
    }
    public override void InitializedSubState(){}
}
