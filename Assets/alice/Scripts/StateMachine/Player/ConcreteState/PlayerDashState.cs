using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDashState : PlayerBaseState
{
    public PlayerDashState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory)
    :base (currentContext, playerStateFactory){} //把這個傳去base state
    public override void EnterState(){
        //Debug.Log("Start Dash");
        //InputSystem.instance.IsDashPressed = false;
        if(!Ctx.IsRunning&&!Ctx.IsWalking){
            FinishedDash();
        }
        else if(Ctx.IsJumping){
            ChangeAnimationState(Ctx.AnimationDashInAir);
            FunctionTimer.Create(() => FinishedDash(),Ctx.Animator.GetCurrentAnimatorStateInfo(0).length,"AIR DASH");
        }
        else{
            ChangeAnimationState(Ctx.AnimationDash);
            FunctionTimer.Create(() => FinishedDash(),Ctx.Animator.GetCurrentAnimatorStateInfo(0).length,"DASH");
        }
    }
    public override void UpdateState(){
        
        CheckSwitchStates();
        if(!Ctx.IsRunning&&!Ctx.IsWalking){ //Idle
            Ctx.AppliedMovementX = 0;
            Ctx.AppliedMovementZ = 0;
        }
        else{
            Ctx.CurrentSpeed = Ctx.DashSpeed;
            if(Ctx.IsJumping) Ctx.CurrentSpeed = Ctx.CurrentSpeed*Ctx.JumpingFriction*0.8f;
            if(Ctx.IsAiming)Ctx.CurrentSpeed *= 0.5f;
            Ctx.AppliedMovementX = (Ctx.MoveDir*Ctx.CurrentSpeed).x;
            Ctx.AppliedMovementZ = (Ctx.MoveDir*Ctx.CurrentSpeed).z;
        }


    }
    public override void ExitState(){
        Ctx.IsDashing = false;
    }
    public override void CheckSwitchStates(){
        
        if(Ctx.IsDashing) return;
        if(Ctx.IsRunning){
            SwitchState(Factory.Run());
        }
        else if(Ctx.IsWalking){
            SwitchState(Factory.Walk());
        }
        else {
            SwitchState(Factory.Idle());
        }
    }
    public override void InitializedSubState(){}

    void FinishedDash(){
        //Debug.Log("Finished dash");
        Ctx.IsDashing = false;
        CheckSwitchStates();
    }
}
