using UnityEngine;

public class EnemyKnight : Enemy
{
    public BoxCollider2D coll;

    //States
    public EnemyKnightIdleState idleState { get; private set; }
    public EnemyKnightMoveState moveState { get; private set; }
    public EnemyKnightBattleState battleState { get; private set; }
    public EnemyKnightAttackState attackState { get; private set; }
    public EnemyKnightDeadState deadState { get; private set; }

    protected override void Awake()
    {
        base.Awake();
        idleState = new EnemyKnightIdleState(this, stateMachine, "Idle", this);
        moveState = new EnemyKnightMoveState(this, stateMachine, "Move", this);
        battleState = new EnemyKnightBattleState(this, stateMachine, "Move", this);
        attackState = new EnemyKnightAttackState(this, stateMachine, "Attack", this);
        deadState = new EnemyKnightDeadState(this, stateMachine, "Idle", this);
    }

    protected override void Start()
    {
        base.Start();
        stateMachine.Initialize(idleState);
    }

    protected override void Update()
    {
        base.Update();

        if(transform.position.y <= -20f)
            Destroy(gameObject);
    }

    public override void Die()
    {
        base.Die();
        stateMachine.ChangeState(deadState);
        ItemDrop();
    }

    protected override void ItemDrop()
    {
        base.ItemDrop();

        for (int i = 0; i < dropSize; i++)
        {
            //Burasý düzenlenebilir
            GameObject newObject = Instantiate(goldPrefab, transform.position, Quaternion.identity);
            newObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(Random.Range(-5, 5), Random.Range(5, 15)));
        }

    }
}
