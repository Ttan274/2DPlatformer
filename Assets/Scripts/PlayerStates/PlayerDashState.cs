using UnityEngine;

public class PlayerDashState : PlayerState
{
    public PlayerDashState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }

    public override void EnterState()
    {
        base.EnterState();
        stateTimer = player.dashDuration;
    }

    public override void ExitState()
    {
        base.ExitState();
        player.SetVelocity(0, player.rb.linearVelocityY);
    }

    public override void Update()
    {
        base.Update();

        if (!player.IsGrounded() && player.OnWall())
            stateMachine.ChangeState(player.wallSlideState);

        player.SetVelocity(player.dashSpeed * player.dashDir, 0);

        if (stateTimer < 0)
            stateMachine.ChangeState(player.idleState);
    }
}
