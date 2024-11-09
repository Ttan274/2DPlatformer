using UnityEngine;

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

        if (player.IsGrounded())
            stateMachine.ChangeState(player.idleState);

        if (horInput != 0)
            player.SetVelocity(player.moveSpeed * 0.8f * horInput, player.rb.linearVelocityY);
    }
}
