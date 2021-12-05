using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerShootState : PlayerBaseState
{
    public PlayerShootState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory)
    :base (currentContext, playerStateFactory){
        IsRootState = true;
        InitializedSubState();
    } //把這個傳去base state



    public override void EnterState(){
        Debug.Log("Start Shoot");
        Ctx.IsShooting = true;
        switch(Ctx.CurrentMagic){
            case 1:
                ChangeAnimationState(Ctx.AnimationStartMagicConch);
                FunctionTimer.Create(() => HoldMagicConch(),Ctx.Animator.GetCurrentAnimatorStateInfo(0).length-0.5f,"StartHoldConch");
                
                break;
            case 2:
                break;
            case 3:
                break;
            default:
                break;
        }
    }
    public override void UpdateState(){
         if(InputSystem.instance.IsShootReleased){
             CheckMagicConch();
             InputSystem.instance.IsShootReleased = false;
         }
         CheckSwitchStates();
         Ctx.HoldingTime+= Time.deltaTime;
    }
    public override void ExitState(){
        //Debug.Log("Exit Shoot");
        Ctx.IsShooting = false;
        InputSystem.instance.IsShootPressed = false;
        Ctx.HoldingTime = 0;
    }
    public override void CheckSwitchStates(){
        if(!Ctx.IsShooting){
            //exit
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
            SetSubState(Factory.Idle());
        }
    }

    void HoldMagicConch(){
        //Debug.Log("Start Hold conch magic");
        Ctx.IsHolding = true;
        ChangeAnimationState(Ctx.AnimationHoldMagicConch);
        //這邊firepoint 要放集氣動畫
        FunctionTimer.Create(() => CanceledMagicConch(),Ctx.MagicConchMaxHoldingTime,"HoldMagicConch"); //如果按住太久就取消
    }

    void CanceledMagicConch(){
        ///Debug.Log("Canceled Shoot");

        InputSystem.instance.IsShootReleased = false;
        FunctionTimer.StopTimer("StartHoldConch");
        FunctionTimer.StopTimer("HoldMagicConch");
        SwitchState(Factory.Grounded());
    }

    void FinishedMagicConch(){
        //Debug.Log("Finished Shoot");
        FunctionTimer.StopTimer("StartHoldConch");
        FunctionTimer.StopTimer("HoldMagicConch");
        ChangeAnimationState(Ctx.AnimationEndMagicConch);
        FunctionTimer.Create(()=> SwitchState(Factory.Grounded()),Ctx.Animator.GetCurrentAnimatorStateInfo(0).length);
    }

    void CheckMagicConch(){
        Ctx.IsHolding = false;

        if(Ctx.HoldingTime <= Ctx.MagicConchMaxHoldingTime&& Ctx.HoldingTime >= Ctx.MagicConchMinHoldingTime){
            FinishedMagicConch();
        }
        else CanceledMagicConch();
    }


}
