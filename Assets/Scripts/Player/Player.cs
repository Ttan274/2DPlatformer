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

    [Header("Teleport Parameters")]
    [SerializeField] private float teleportCooldown;
    private GameObject currentPortal;
    private float teleportCooldownTimer;

    [Header("Puzzle Parameters")]
    [SerializeField] private float puzzleCooldown;
    private float puzzleCooldownTimer;
    private GameObject currentPuzzle;

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
        attackState = new PlayerAttackState(this, stateMachine, "Aim");             //new anim will be added and character will throw spear
        strikeState = new PlayerStrikeState(this, stateMachine, "Strike");
        deadState = new PlayerDeadState(this, stateMachine, "Die");
    }

    protected override void Start()
    {
        base.Start();
        playerAttack = GetComponent<PlayerAttack>();

        stateMachine.Initialize(idleState);
    }

    protected override void Update()
    {
        base.Update();
        stateMachine.currentState.Update();
        DashInputCheck();
        InputCheck();
    }

    #region OtherBehaviours
    private void InputCheck()
    {
        teleportCooldownTimer -= Time.deltaTime;
        puzzleCooldown -= Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.E) && currentPortal != null && teleportCooldownTimer <= 0)
        {
            transform.position = currentPortal.GetComponent<Portal>().GetTargetPortal().position;
            teleportCooldownTimer = teleportCooldown;
        }

        if (Input.GetKeyDown(KeyCode.E) && currentPuzzle != null && puzzleCooldown <= 0)
        {
            stateMachine.ChangeState(idleState);
            currentPuzzle.GetComponent<Puzzle>().StartPuzzleSystem();
        }
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
    #endregion

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Portal"))
            currentPortal = other.gameObject;

        if (other.CompareTag("Puzzle"))
            currentPuzzle = other.gameObject;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Portal") && other.gameObject == currentPortal)
            currentPortal = null;

        if (other.CompareTag("Puzzle") && other.gameObject == currentPuzzle)
            currentPuzzle = null;
    }
}
