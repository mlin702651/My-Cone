

public class PlayerStateFactory
{
    PlayerStateMachine _context;
    

    public PlayerStateFactory(PlayerStateMachine currentContext){
        _context = currentContext;
    }

    public PlayerBaseState Idle(){
        return new PlayerIdleState(_context, this);
    }
    public PlayerBaseState Walk(){
        return new PlayerWalkState(_context, this);
    }
    public PlayerBaseState Run(){
        return new PlayerRunState(_context, this);
    }
    public PlayerBaseState Jump(){
        return new PlayerJumpState(_context, this);
    }
    public PlayerBaseState Grounded(){
        return new PlayerGroundedState(_context, this);
    }
    public PlayerBaseState Shoot(){
        return new PlayerShootState(_context, this);
    }
    public PlayerBaseState Dash(){
        return new PlayerDashState(_context, this);
    }
    public PlayerBaseState Slide(){
        return new PlayerSlideState(_context, this);
    }
}

/*
I thought of and implemented a small refactor that should provide a nice performance boost to the code. 
When you use the StateFactory, you're creating a new instance of each state to be applied. 
It clearly works great, but discarding those classes after you don't need them anymore causes some work for Garbage Collection, 
which can cause issues down the road. 
So I went back and in the constructor of the factory I create an instance of each state and store them in a dictionary. 
Then instead of returning a new state, I just fetch the same state from the dictionary. 
If you do that, and move the initialize substate method to the Enter functions of the states, then everything works the same. 
You could even go a step further 
and have the switch state method take in a state enum instead of having different methods for each state.
*/
