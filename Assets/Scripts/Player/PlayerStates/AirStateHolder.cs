using UnityEngine;

//State holder for air related states

//Jump State
public class PlayerJumpState : PlayerState
{
    public PlayerJumpState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }

    public override void EnterState()
    {
        base.EnterState();
        player.rb.linearVelocity = new Vector2(player.rb.linearVelocityX, player.jumpForce);
        stateTimer = 0.2f;
    }

    public override void ExitState()
    {
        base.ExitState();
    }

    public override void Update()
    {
        base.Update();

        if (stateTimer < 0)
            stateMachine.ChangeState(player.airState);
    }
}


//Air State
public class PlayerAirState : PlayerState
{
    public PlayerAirState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }

    public override void EnterState()
    {
        base.EnterState();
    }

    public override void ExitState()
    {
        base.ExitState();
    }

    public override void Update()
    {
        base.Update();

        if (player.OnWall())
            stateMachine.ChangeState(player.wallSlideState);

        if (player.IsGrounded())
            stateMachine.ChangeState(player.idleState);

        if (horInput != 0)
            player.SetVelocity(player.moveSpeed * 0.8f * horInput, player.rb.linearVelocityY);
    }
}


//Wall Slide State
public class PlayerWallSlideState : PlayerState
{
    public PlayerWallSlideState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }

    public override void EnterState()
    {
        base.EnterState();
    }

    public override void ExitState()
    {
        base.ExitState();
    }

    public override void Update()
    {
        base.Update();

        if (Input.GetKeyDown(KeyCode.Space))
        {
            stateMachine.ChangeState(player.wallJumpState);
            return;
        }

        if (verInput < 0)
            player.rb.linearVelocity = new Vector2(0, player.rb.linearVelocityY);
        else
            player.rb.linearVelocity = new Vector2(0, player.rb.linearVelocityY * 0.5f);

        if (player.IsGrounded() || !player.OnWall() || horInput != player.facingDir)
            stateMachine.ChangeState(player.airState);
    }
}


//Wall Jump State
public class PlayerWallJumpState : PlayerState
{
    public PlayerWallJumpState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }

    public override void EnterState()
    {
        base.EnterState();

        stateTimer = 0.2f;
        player.SetVelocity(5f * -player.facingDir, player.jumpForce);
    }

    public override void ExitState()
    {
        base.ExitState();
    }

    public override void Update()
    {
        base.Update();

        if (stateTimer < 0)
            stateMachine.ChangeState(player.airState);

        if (player.IsGrounded())
            stateMachine.ChangeState(player.idleState);
    }
}