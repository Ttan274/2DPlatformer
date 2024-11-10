using UnityEngine;

public class PlayerStrikeState : PlayerState
{
    public PlayerStrikeState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }

    public override void EnterState()
    {
        base.EnterState();
        horInput = 0;

        float attackDir = player.facingDir;
        if (horInput != 0)
            attackDir = horInput;

        player.SetVelocity(player.attackMovement.x * attackDir, player.attackMovement.y);

        stateTimer = 0.3f;  //timer can change
    }

    public override void ExitState()
    {
        base.ExitState();
        player.StartCoroutine("BusyRoutine", 0.15f);
    }

    public override void Update()
    {
        base.Update();

        if (stateTimer < 0)
        {
            player.SetVelocity(0, 0);
            stateMachine.ChangeState(player.idleState);
        }       
    }
}
