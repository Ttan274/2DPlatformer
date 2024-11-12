using UnityEngine;

public class EnemyKnightAttackState : EnemyState
{
    private EnemyKnight knight;

    public EnemyKnightAttackState(Enemy _enemy, EnemyStateMachine _stateMachine, string _animBoolName, EnemyKnight _knight) : base(_enemy, _stateMachine, _animBoolName)
    {
        knight = _knight;
    }

    public override void EnterState()
    {
        base.EnterState();
    }

    public override void ExitState()
    {
        base.ExitState();

        knight.lastTimeAttacked = Time.time;
    }

    public override void Update()
    {
        base.Update();

        knight.SetVelocity(0, 0);

        if (triggerCalled)
            stateMachine.ChangeState(knight.battleState);
    }
}
