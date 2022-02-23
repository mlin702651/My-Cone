using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGroundedState : PlayerBaseState
{
    
    public PlayerGroundedState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory)
    :base (currentContext, playerStateFactory){
        IsRootState = true;
        InitializedSubState();
    } //把這個傳去base state
    public override void EnterState(){
        Ctx.GravityMovementY = -9.8f;
        Ctx.AppliedMovementY = -9.8f;
        //Debug.Log("Start Grounded");
    }
    public override void UpdateState(){
        CheckSwitchStates();
        HandleGravity();

    }
    public override void ExitState(){}
    public override void InitializedSubState(){
        if(Ctx.IsDashing){
            SetSubState(Factory.Dash());
        }
        else if(Ctx.IsRunning){
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
    public override void CheckSwitchStates(){
        if(InputSystem.instance.IsJumpPressed){
            SwitchState(Factory.Jump());
        }
        if(InputSystem.instance.IsShootPressed){
            SwitchState(Factory.Shoot());
        }
    }

    void HandleGravity(){
        
        
        float previousYVelocity = Ctx.GravityMovementY;
        Ctx.GravityMovementY = Ctx.GravityMovementY + (Ctx.Gravity* Ctx.FallMultiplier * Time.deltaTime);
        Ctx.AppliedMovementY = Mathf.Max((previousYVelocity + Ctx.GravityMovementY) * 0.5f, -9.8f); //從高處掉下來的時候不要掉太快
        
        
    }
}
