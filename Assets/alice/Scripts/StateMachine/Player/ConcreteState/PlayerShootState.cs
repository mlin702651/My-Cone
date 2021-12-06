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
        Ctx.GravityMovementY = -3f;
        Ctx.AppliedMovementY = -3f;
        Debug.Log("Start Shoot");
        
        switch(Ctx.CurrentMagic){
            case 1:
                CheckMagicConchCD();
                break;
            case 2:
                StartMagicBubble();
                break;
            case 3:
                break;
            default:
                break;
        }
    }
    public override void UpdateState(){
         if(InputSystem.instance.IsShootReleased&&Ctx.CurrentMagic ==1){
             CheckMagicConch();
             InputSystem.instance.IsShootReleased = false;
         }
         if(Ctx.CurrentMagic ==2){
             CheckMagicBubble();
         }
         CheckSwitchStates();
         if(Ctx.IsShooting){
            Ctx.HoldingTime+= Time.deltaTime;
            
            PlayerMagicController.instance.MagicConchBornTime = Mathf.Pow(2, Ctx.HoldingTime) - 1.0f;
         }
         HandleGravity();
    }
    public override void ExitState(){
        //Debug.Log("Exit Shoot");
        Ctx.IsShooting = false;
        Ctx.IsHolding = false;
        Ctx.IsEnding = false;
        InputSystem.instance.IsShootPressed = false;
        Ctx.HoldingTime = 0;
        PlayerMagicController.instance.MagicConchBornTime = 2f;
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
        FunctionTimer.Create(() => CanceledMagicConch(),Ctx.MagicConchMaxHoldingTime-0.6f,"HoldMagicConch"); //如果按住太久就取消
    }

    void CanceledMagicConch(){
        ///Debug.Log("Canceled Shoot");

        InputSystem.instance.IsShootReleased = false;
        FunctionTimer.StopTimer("StartHoldConch");
        FunctionTimer.StopTimer("HoldingMagicConch");
        SwitchState(Factory.Grounded());

        
    }

    void FinishedMagicConch(){
        //Debug.Log("Finished Shoot");
        FunctionTimer.StopTimer("StartHoldConch");
        FunctionTimer.StopTimer("HoldingMagicConch");
        ChangeAnimationState(Ctx.AnimationEndMagicConch);

        PlayerMagicController.instance.MagicConchFinished();
        UIManager.instance.StartAccumulateCD();

        FunctionTimer.Create(()=> SwitchState(Factory.Grounded()),Ctx.Animator.GetCurrentAnimatorStateInfo(0).length);
    }

    void CheckMagicConch(){
        Ctx.IsHolding = false;

        if(Ctx.HoldingTime <= Ctx.MagicConchMaxHoldingTime&& Ctx.HoldingTime >= Ctx.MagicConchMinHoldingTime){
            FinishedMagicConch();
        }
        else CanceledMagicConch();

        PlayerMagicController.instance.CanceledMagicConch();
    }

    void CheckMagicConchCD(){
        if(UIManager.instance.IsAccumulateAttack || UIManager.instance.RecoverAccumulateAttack){
            return;
        }
        else{
            Ctx.IsShooting = true;
            ChangeAnimationState(Ctx.AnimationStartMagicConch);
            FunctionTimer.Create(() => HoldMagicConch(),Ctx.Animator.GetCurrentAnimatorStateInfo(0).length-0.5f,"StartHoldConch");

            PlayerMagicController.instance.MagicConchStart();
        
        }
    }

    void StartMagicBubble(){
        if(Ctx.IsEnding) return;
        //Debug.Log("Start bubble");
        
        Ctx.IsHolding = true;
        Ctx.IsShooting = true;

        InputSystem.instance.IsShootPressed = false;

        PlayerMagicController.instance.MagicBubbleStart();
        if(Ctx.CurrentAnimationState!=Ctx.AnimationMagicBubbleRun||(!Ctx.IsRunning&&!Ctx.IsWalking))
            ChangeAnimationState(Ctx.AnimationHoldMagicBubble);
        //FunctionTimer.Create(() => FinishedMagicBubble(),Ctx.Animator.GetCurrentAnimatorStateInfo(0).length);

    }

    

    void FinishedMagicBubble(){
        
        Ctx.IsShooting = false;
        Ctx.IsEnding = false;
    }

    void CheckMagicBubble(){
        if(Ctx.HoldingTime>=0.5){ //間隔時間太長就結束
            Ctx.IsEnding = true;
            ChangeAnimationState(Ctx.AnimationEndMagicBubble);
            FunctionTimer.Create(() => FinishedMagicBubble(),Ctx.Animator.GetCurrentAnimatorStateInfo(0).length); 
            ///Ctx.IsShooting = false;
        }
        else if(InputSystem.instance.IsShootPressed){
            Ctx.HoldingTime = 0;
            StartMagicBubble();
        }

    }

    void HandleGravity(){
        
        
        float previousYVelocity = Ctx.GravityMovementY;
        Ctx.GravityMovementY = Ctx.GravityMovementY + (Ctx.Gravity* Ctx.FallMultiplier * Time.deltaTime);
        Ctx.AppliedMovementY = Mathf.Max((previousYVelocity + Ctx.GravityMovementY) * 0.5f, -9.8f); //從高處掉下來的時候不要掉太快
        
        
    }


}
