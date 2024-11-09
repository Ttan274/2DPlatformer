using UnityEngine;

//Player class
public class Player : MonoBehaviour
{
    [Header("Move Parameters")]
    public float moveSpeed;

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

    private void Awake()
    {
        stateMachine = new PlayerStateMachine();

        idleState = new PlayerIdleState(this, stateMachine, "Idle");
        moveState = new PlayerMoveState(this, stateMachine, "Move");
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
}
