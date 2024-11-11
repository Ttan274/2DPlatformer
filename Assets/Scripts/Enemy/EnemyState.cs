using UnityEngine;

public class EnemyState
{
    //References to other components
    protected EnemyStateMachine stateMachine;
    protected Enemy enemy;

    protected float stateTimer;
    private string animBoolName;

    public EnemyState(Enemy _enemy, EnemyStateMachine _stateMachine, string _animBoolName)
    {
        enemy = _enemy;
        stateMachine = _stateMachine;
        animBoolName = _animBoolName;
    }

    public virtual void EnterState()
    {
        enemy.anim.SetBool(animBoolName, true);
    }

    public virtual void Update()
    {
        stateTimer -= Time.deltaTime;
    }

    public virtual void ExitState()
    {
        enemy.anim.SetBool(animBoolName, false);
    }
}
