using UnityEngine;

public class PlayerJumpState : PlayerState
{
    public PlayerJumpState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }

    public override void EnterState()
    {
        base.EnterState();
        player.rb.linearVelocity = new Vector2(player.rb.linearVelocityX, player.jumpForce);
    }

    public override void ExitState()
    {
        base.ExitState();
    }

    public override void Update()
    {
        base.Update();

        if (player.rb.linearVelocityY < 0)
            stateMachine.ChangeState(player.airState);
    }
}
