using System;
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
    public float dashDir { get; private set; } 

    [Header("Collision Parameters")]
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundCheckDistance;
    [SerializeField] private LayerMask groundMask;

    //Flip Parameters
    private bool isFacingRight = true;
    public int facingDir { get; private set; } = 1;

    //Components
    public Rigidbody2D rb { get; private set; }
    public Animator anim { get; private set; }

    //States
    public PlayerStateMachine stateMachine { get; private set; }
    public PlayerIdleState idleState { get; private set; }
    public PlayerMoveState moveState { get; private set; }
    public PlayerAirState airState { get; private set; }
    public PlayerJumpState jumpState { get; private set; }
    public PlayerDashState dashState { get; private set; }
    public PlayerWallSlideState wallSlideState { get; private set; }
    public PlayerWallJumpState wallJumpState { get; private set; }

    private void Awake()
    {
        stateMachine = new PlayerStateMachine();

        idleState = new PlayerIdleState(this, stateMachine, "Idle");
        moveState = new PlayerMoveState(this, stateMachine, "Move");
        airState = new PlayerAirState(this, stateMachine, "Jump");
        jumpState = new PlayerJumpState(this, stateMachine, "Jump");
        dashState = new PlayerDashState(this, stateMachine, "Jump");    //animation might be change
        wallSlideState = new PlayerWallSlideState(this, stateMachine, "Jump");
        wallJumpState = new PlayerWallJumpState(this, stateMachine, "Jump");
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponentInChildren<Animator>();

        stateMachine.Initialize(idleState);
    }

    private void Update()
    {
        stateMachine.currentState.Update();
        DashInputCheck();
    }

    private void DashInputCheck()
    {
        if(Input.GetKeyDown(KeyCode.LeftShift))
        {
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

    private void Flip()
    {
        isFacingRight = !isFacingRight;
        facingDir = facingDir * -1;
        transform.Rotate(0, 180, 0);
    }

    public bool IsGrounded() => Physics2D.Raycast(groundCheck.position, Vector2.down, groundCheckDistance, groundMask);

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green
            ;
        Gizmos.DrawLine(groundCheck.position, new Vector3(groundCheck.position.x, groundCheck.position.y - groundCheckDistance));
    }
}
