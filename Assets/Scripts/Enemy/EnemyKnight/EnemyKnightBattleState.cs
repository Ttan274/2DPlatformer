using UnityEngine;

public class EnemyKnightBattleState : EnemyState
{
    private EnemyKnight knight;
    private Player player;
    private int moveDir;

    public EnemyKnightBattleState(Enemy _enemy, EnemyStateMachine _stateMachine, string _animBoolName, EnemyKnight _knight) : base(_enemy, _stateMachine, _animBoolName)
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

        if (player.isDead)
            stateMachine.ChangeState(knight.idleState);

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
            if (stateTimer < 0 || Vector2.Distance(knight.transform.position, player.transform.position) > 8f)
                stateMachine.ChangeState(knight.idleState);
        }
        
        //Movement
        if (player.transform.position.x > knight.transform.position.x)
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
