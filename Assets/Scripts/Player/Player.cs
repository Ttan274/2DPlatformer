using System;
using System.Collections;
using UnityEngine;

//Player class
public class Player : MonoBehaviour
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

    [Header("Collision Parameters")]
    [SerializeField] private Transform groundCheck;
    [SerializeField] private Transform wallCheck;
    [SerializeField] private float groundCheckDistance;
    [SerializeField] private float wallCheckDistance;
    [SerializeField] private LayerMask groundMask;

    [Header("Attack Parameters")]
    public Vector2 attackMovement;

    //Flip Parameters
    private bool isFacingRight = true;
    public int facingDir { get; private set; } = 1;

    //Busy
    public bool isBusy { get; private set; }

    //Components
    public Rigidbody2D rb { get; private set; }
    public Animator anim { get; private set; }
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

    private void Awake()
    {
        stateMachine = new PlayerStateMachine();

        idleState = new PlayerIdleState(this, stateMachine, "Idle");
        moveState = new PlayerMoveState(this, stateMachine, "Move");
        airState = new PlayerAirState(this, stateMachine, "Jump");
        jumpState = new PlayerJumpState(this, stateMachine, "Jump");
        dashState = new PlayerDashState(this, stateMachine, "Jump");    //animation might be change
        wallSlideState = new PlayerWallSlideState(this, stateMachine, "Jump"); //animation might be change
        wallJumpState = new PlayerWallJumpState(this, stateMachine, "Jump");
        attackState = new PlayerAttackState(this, stateMachine, "Aim");
        strikeState = new PlayerStrikeState(this, stateMachine, "Strike");
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponentInChildren<Animator>();
        playerAttack = GetComponent<PlayerAttack>();    

        stateMachine.Initialize(idleState);
    }

    private void Update()
    {
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

    public void SetVelocity(float horVelocity, float verVelocity)
    {
        rb.linearVelocity = new Vector2(horVelocity, verVelocity);
        FlipController(horVelocity);
    }

    private void FlipController(float horVelocity)
    {
        if (horVelocity > 0 && !isFacingRight)
            Flip();
        else if (horVelocity < 0 && isFacingRight)
            Flip();
    }

    public void Flip()
    {
        isFacingRight = !isFacingRight;
        facingDir = facingDir * -1;
        transform.Rotate(0, 180, 0);
    }

    public bool IsGrounded() => Physics2D.Raycast(groundCheck.position, Vector2.down, groundCheckDistance, groundMask);
    public bool OnWall() => Physics2D.Raycast(wallCheck.position, Vector2.right * facingDir, wallCheckDistance, groundMask);

    public IEnumerator BusyRoutine(float seconds)
    {
        isBusy = true;

        yield return new WaitForSeconds(seconds);

        isBusy = false;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green
            ;
        Gizmos.DrawLine(groundCheck.position, new Vector3(groundCheck.position.x, groundCheck.position.y - groundCheckDistance));
        Gizmos.DrawLine(wallCheck.position, new Vector3(wallCheck.position.x + wallCheckDistance, wallCheck.position.y));
    }
}
