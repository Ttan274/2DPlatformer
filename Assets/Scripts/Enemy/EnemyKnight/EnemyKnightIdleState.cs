using UnityEngine;

public class EnemyKnightIdleState : EnemyKnightGroundedState
{
    public EnemyKnightIdleState(Enemy _enemy, EnemyStateMachine _stateMachine, string _animBoolName, EnemyKnight _knight) : base(_enemy, _stateMachine, _animBoolName, _knight)
    {
    }

    public override void EnterState()
    {
        base.EnterState();
        stateTimer = knight.idleTime;
    }

    public override void ExitState()
    {
        base.ExitState();
    }

    public override void Update()
    {
        base.Update();

        if (stateTimer < 0)
            stateMachine.ChangeState(knight.moveState);
    }
}
