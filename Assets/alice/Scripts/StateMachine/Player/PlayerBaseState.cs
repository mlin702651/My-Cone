﻿

public abstract class PlayerBaseState
{
    private bool _isRootState = false;
    private PlayerStateMachine _ctx;
    private PlayerStateFactory _factory;
    private PlayerBaseState _currentSubState = null;
    private PlayerBaseState _currentSuperState;

    protected bool IsRootState {set{_isRootState = value;}}
    protected PlayerStateMachine Ctx {get{return _ctx;}}
    protected PlayerStateFactory Factory {get{return _factory;}}
    protected PlayerBaseState CurrentSuperState{get{return _currentSuperState;}}



    public PlayerBaseState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory){
        _ctx = currentContext;
        _factory = playerStateFactory;
    }

    public abstract void EnterState();
    public abstract void UpdateState();
    public abstract void ExitState();
    public abstract void CheckSwitchStates();
    public abstract void InitializedSubState();


    public void UpdateStates(){
        UpdateState();
        if(_currentSubState != null){
            _currentSubState.UpdateStates();
        }
        
    }
    protected void SwitchState(PlayerBaseState newState){
        // current state exits state
        ExitState();

        //new state enters state
        newState.EnterState();

        // switch current state of context
        if(_isRootState)_ctx.CurrentState = newState;
        else if(_currentSuperState != null) _currentSuperState.SetSubState(newState); // set the current super states sub state to the new state
    }
    protected void SetSuperState(PlayerBaseState newSuperState){
        _currentSuperState = newSuperState;
    }
    protected void SetSubState(PlayerBaseState newSubState){
        _currentSubState = newSubState;
        newSubState.SetSuperState(this);
    }

    protected void ChangeAnimationState(int newAnimationState)
    {
       if(newAnimationState == _ctx.CurrentAnimationState) {
            return; //一樣的話就不重新開始播ㄌ
        }
        _ctx.Animator.Play(newAnimationState);
        
        _ctx.CurrentAnimationState = newAnimationState;
    }


}