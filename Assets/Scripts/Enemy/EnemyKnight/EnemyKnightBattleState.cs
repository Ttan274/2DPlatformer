using UnityEngine;

public class EnemyKnightBattleState : EnemyState
{
    private EnemyKnight knight;
    private Transform player;
    private int moveDir;

    public EnemyKnightBattleState(Enemy _enemy, EnemyStateMachine _stateMachine, string _animBoolName, EnemyKnight _knight) : base(_enemy, _stateMachine, _animBoolName)
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

        if(knight.IsPlayerDetected().collider != null)
        {
            stateTimer = knight.battleTime;
            if(knight.IsPlayerDetected().distance < knight.attackDistance)
            {
                if (CanAttack())
                    stateMachine.ChangeState(knight.attackState);
            }
        }
        else
        {
            if (stateTimer < 0 || Vector2.Distance(knight.transform.position, player.position) > 8f)
                stateMachine.ChangeState(knight.idleState);
        }
        
        //Movement
        if (player.position.x > knight.transform.position.x)
            moveDir = 1;
        else
            moveDir = -1;
        
        knight.SetVelocity(knight.moveSpeed * moveDir, knight.rb.linearVelocityY);
    }

    private bool CanAttack()
    {
        if(Time.time >= knight.lastTimeAttacked + knight.attackCooldown)
        {
            knight.lastTimeAttacked = Time.time;
            return true;
        }

        return false;
    }
}
