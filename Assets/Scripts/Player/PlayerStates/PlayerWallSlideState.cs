using UnityEngine;

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

        if(Input.GetKeyDown(KeyCode.Space))
        {
            stateMachine.ChangeState(player.wallJumpState);
            return;
        }

        if (verInput < 0)
            player.rb.linearVelocity = new Vector2(0, player.rb.linearVelocityY);
        else
            player.rb.linearVelocity = new Vector2(0, player.rb.linearVelocityY * 0.5f);

        if (player.IsGrounded())
            stateMachine.ChangeState(player.idleState);
    }
}
