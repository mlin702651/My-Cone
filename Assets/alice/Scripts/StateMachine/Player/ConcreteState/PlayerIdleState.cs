using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIdleState :PlayerBaseState
{
    public PlayerIdleState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory)
    :base (currentContext, playerStateFactory){} //把這個傳去base state
    public override void EnterState(){
        
        
        
    }
    public override void UpdateState(){
        CheckSwitchStates();
        Ctx.AppliedMovementX = 0;
        Ctx.AppliedMovementZ = 0;
        Debug.Log("now Idle");
        if(Ctx.Animator.GetCurrentAnimatorStateInfo(0).shortNameHash == Ctx.AnimationJumpEnd || Ctx.IsJumping) return; //如果還在降落就播完動畫
        ChangeAnimationState(Ctx.AnimationIdle);
    }
    public override void ExitState(){}
    public override void CheckSwitchStates(){
        if(Ctx.IsRunning){
            SwitchState(Factory.Run());
        }
        else if(Ctx.IsWalking){
            SwitchState(Factory.Walk());
        }
    }
    public override void InitializedSubState(){}
}
