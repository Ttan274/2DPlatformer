using UnityEngine;

public class EnemyKnightGroundedState : EnemyState
{
    protected EnemyKnight knight;

    public EnemyKnightGroundedState(Enemy _enemy, EnemyStateMachine _stateMachine, string _animBoolName, EnemyKnight _knight) : base(_enemy, _stateMachine, _animBoolName)
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
    }

    public override void Update()
    {
        base.Update();

        //check for player cause we are going to enter attack state from here
    }
}
