using UnityEngine;

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
