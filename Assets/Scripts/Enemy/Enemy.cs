using UnityEngine;

public class Enemy : Entity
{
    [Header("Enemy Parameters")]
    public float idleTime;
    public float moveSpeed;
    public float battleTime;
    public float attackDistance;
    public float attackCooldown;
    [HideInInspector] public float lastTimeAttacked;
    [SerializeField] private LayerMask playerLayer;

    //States
    public EnemyStateMachine stateMachine { get; private set; }

    protected override void Awake()
    {
        base.Awake();
        stateMachine = new EnemyStateMachine();
    }

    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        base.Update();
        stateMachine.currentState.Update();
    }

    public virtual RaycastHit2D IsPlayerDetected() => Physics2D.Raycast(wallCheck.position, Vector2.right * facingDir, 20f, playerLayer);
    public virtual void AnimationTrigger() => stateMachine.currentState.FinishAnimationTrigger();
}
