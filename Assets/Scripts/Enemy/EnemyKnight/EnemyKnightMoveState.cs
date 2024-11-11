using UnityEngine;

public class EnemyKnightMoveState : EnemyKnightGroundedState
{
    public EnemyKnightMoveState(Enemy _enemy, EnemyStateMachine _stateMachine, string _animBoolName, EnemyKnight _knight) : base(_enemy, _stateMachine, _animBoolName, _knight)
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

        knight.SetVelocity(knight.moveSpeed * knight.facingDir, knight.rb.linearVelocityY);

        if(knight.OnWall() || !knight.IsGrounded())
        {
            stateMachine.ChangeState(knight.idleState);
            knight.Flip();
        }
    }
}
