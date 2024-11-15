using UnityEngine;

public class EnemyKnightDeadState : EnemyState
{
    EnemyKnight knight;

    public EnemyKnightDeadState(Enemy _enemy, EnemyStateMachine _stateMachine, string _animBoolName, EnemyKnight _knight) : base(_enemy, _stateMachine, _animBoolName)
    {
        knight = _knight;
    }

    public override void EnterState()
    {
        base.EnterState();

        knight.anim.SetBool(knight.lastAnimBoolName, true);
        knight.anim.speed = 0;
        knight.coll.enabled = false;

        stateTimer = 0.1f;
    }

    public override void ExitState()
    {
        base.ExitState();
    }

    public override void Update()
    {
        base.Update();

        if (stateTimer > 0)
            knight.rb.linearVelocity = new Vector2(0, 10f);
    }
}
