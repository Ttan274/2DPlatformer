using UnityEngine;

//State holder for all ground related states

//Base grounded state
public class PlayerGroundedState : PlayerState
{
    public PlayerGroundedState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
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

        if (Input.GetKeyDown(KeyCode.Mouse0))
            stateMachine.ChangeState(player.strikeState);

        if (Input.GetKeyDown(KeyCode.Mouse1))
            stateMachine.ChangeState(player.attackState);

        if (!player.IsGrounded())
            stateMachine.ChangeState(player.airState);

        if (Input.GetKeyDown(KeyCode.Space) && player.IsGrounded())
            stateMachine.ChangeState(player.jumpState);
    }
}

//Idle state
public class PlayerIdleState : PlayerGroundedState
{
    public PlayerIdleState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
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

        if (horInput == player.facingDir && player.OnWall())
            return;

        if (horInput != 0 && !player.isBusy)
            stateMachine.ChangeState(player.moveState);
    }
}

//Move state
public class PlayerMoveState : PlayerGroundedState
{
    public PlayerMoveState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
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

        player.SetVelocity(horInput * player.moveSpeed, player.rb.linearVelocityY);

        if (horInput == 0 || player.OnWall())
            stateMachine.ChangeState(player.idleState);
    }
}

//Dead state
public class PlayerDeadState : PlayerState
{
    public PlayerDeadState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
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

        player.SetVelocity(0, 0);
    }
}