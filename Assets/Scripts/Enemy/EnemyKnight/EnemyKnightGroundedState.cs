using UnityEngine;

public class EnemyKnightGroundedState : EnemyState
{
    protected EnemyKnight knight;
    protected Player player;

    public EnemyKnightGroundedState(Enemy _enemy, EnemyStateMachine _stateMachine, string _animBoolName, EnemyKnight _knight) : base(_enemy, _stateMachine, _animBoolName)
    {
        knight = _knight;
    }

    public override void EnterState()
    {
        base.EnterState();
        player = PlayerManager.instance.player;
    }

    public override void ExitState()
    {
        base.ExitState();
    }

    public override void Update()
    {
        base.Update();

        if (knight.IsPlayerDetected() && !player.isDead)
            stateMachine.ChangeState(knight.battleState);
    }
}
