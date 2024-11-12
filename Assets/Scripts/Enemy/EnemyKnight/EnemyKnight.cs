using UnityEngine;

public class EnemyKnight : Enemy
{
    //States
    public EnemyKnightIdleState idleState { get; private set; }
    public EnemyKnightMoveState moveState { get; private set; }
    public EnemyKnightBattleState battleState { get; private set; }
    public EnemyKnightAttackState attackState { get; private set; }

    protected override void Awake()
    {
        base.Awake();
        idleState = new EnemyKnightIdleState(this, stateMachine, "Idle", this);
        moveState = new EnemyKnightMoveState(this, stateMachine, "Move", this);
        battleState = new EnemyKnightBattleState(this, stateMachine, "Move", this);
        attackState = new EnemyKnightAttackState(this, stateMachine, "Attack", this);
    }

    protected override void Start()
    {
        base.Start();
        stateMachine.Initialize(idleState);
    }

    protected override void Update()
    {
        base.Update();
    }
}
