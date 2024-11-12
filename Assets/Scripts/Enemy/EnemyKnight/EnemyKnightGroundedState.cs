using UnityEngine;

public class EnemyKnightGroundedState : EnemyState
{
    protected EnemyKnight knight;
    protected Transform player;

    public EnemyKnightGroundedState(Enemy _enemy, EnemyStateMachine _stateMachine, string _animBoolName, EnemyKnight _knight) : base(_enemy, _stateMachine, _animBoolName)
    {
        knight = _knight;
    }

    public override void EnterState()
    {
        base.EnterState();
        player = PlayerManager.instance.player.transform;
    }

    public override void ExitState()
    {
        base.ExitState();
    }

    public override void Update()
    {
        base.Update();

        if (knight.IsPlayerDetected())
            stateMachine.ChangeState(knight.battleState);
    }
}
