using System.Collections;
using UnityEngine;

//Player class
public class Player : Entity
{
    [Header("Move Parameters")]
    public float moveSpeed;
    public float jumpForce;

    [Header("Dash Parameters")]
    public float dashSpeed;
    public float dashDuration;
    [SerializeField] private float dashCooldown;
    private float dashCooldownTimer;
    public float dashDir { get; private set; } 

    [Header("Attack Parameters")]
    public Vector2 attackMovement;

    //Gold Parameters
    public int goldCounter {get; private set; }
    public System.Action onGoldCollected;

    //Busy
    public bool isBusy { get; private set; }

    //Components
    public PlayerAttack playerAttack { get; private set; }

    //States
    public PlayerStateMachine stateMachine { get; private set; }
    public PlayerIdleState idleState { get; private set; }
    public PlayerMoveState moveState { get; private set; }
    public PlayerAirState airState { get; private set; }
    public PlayerJumpState jumpState { get; private set; }
    public PlayerDashState dashState { get; private set; }
    public PlayerWallSlideState wallSlideState { get; private set; }
    public PlayerWallJumpState wallJumpState { get; private set; }
    public PlayerAttackState attackState { get; private set; }
    public PlayerStrikeState strikeState { get; private set; }
    public PlayerDeadState deadState { get; private set; }

    protected override void Awake()
    {
        base.Awake();
        stateMachine = new PlayerStateMachine();

        idleState = new PlayerIdleState(this, stateMachine, "Idle");
        moveState = new PlayerMoveState(this, stateMachine, "Move");
        airState = new PlayerAirState(this, stateMachine, "Jump");
        jumpState = new PlayerJumpState(this, stateMachine, "Jump");
        dashState = new PlayerDashState(this, stateMachine, "Jump");                //animation might be change
        wallSlideState = new PlayerWallSlideState(this, stateMachine, "Jump");      //animation might be change
        wallJumpState = new PlayerWallJumpState(this, stateMachine, "Jump");
        attackState = new PlayerAttackState(this, stateMachine, "Aim");
        strikeState = new PlayerStrikeState(this, stateMachine, "Strike");
        deadState = new PlayerDeadState(this, stateMachine, "Die");
    }

    protected override void Start()
    {
        base.Start();
        playerAttack = GetComponent<PlayerAttack>();
        goldCounter = 0;

        stateMachine.Initialize(idleState);
    }

    protected override void Update()
    {
        base.Update();
        stateMachine.currentState.Update();
        DashInputCheck();
    }

    private void DashInputCheck()
    {
        if (OnWall())
            return;

        dashCooldownTimer -= Time.deltaTime;

        if(Input.GetKeyDown(KeyCode.LeftShift) && dashCooldownTimer <= 0)
        {
            dashCooldownTimer = dashCooldown;

            dashDir = Input.GetAxisRaw("Horizontal");

            if (dashDir == 0)
                dashDir = facingDir;

            stateMachine.ChangeState(dashState);
        }
    }

    public IEnumerator BusyRoutine(float seconds)
    {
        isBusy = true;
        yield return new WaitForSeconds(seconds);
        isBusy = false;
    }

    public override void Die()
    {
        base.Die();
        stateMachine.ChangeState(deadState);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        //Gold Collection
        if (other.gameObject.CompareTag("Gold"))
        {
            Destroy(other.gameObject);
            goldCounter++;
            onGoldCollected?.Invoke();
        }
    }
}
