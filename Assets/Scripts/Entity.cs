using System.Collections;
using UnityEngine;

public class Entity : MonoBehaviour
{
    //Components
    public Animator anim {  get; private set; }
    public Rigidbody2D rb { get; private set; }
    public SpriteRenderer sr { get; private set; }

    //Flip Parameters
    private bool isFacingRight = true;
    public int facingDir { get; private set; } = 1;

    [Header("Collision Detection Properties")]
    [SerializeField] protected Transform groundCheck;
    [SerializeField] protected Transform wallCheck;
    public Transform attackCheck;
    [SerializeField] protected float groundCheckDistance;
    [SerializeField] protected float wallCheckDistance;
    public float attackCheckRadius;
    [SerializeField] protected LayerMask groundMask;

    [Header("Knocback Paramaters")]
    [SerializeField] protected Vector2 knockbackDirection;
    [SerializeField] protected float knockbackDuration;
    protected bool isKnocked;

    [Header("FX Parameters")]
    [SerializeField] private float effectDuration;
    [SerializeField] private Material hitMat;
    [SerializeField] private Color hitColor;
    private Material originalMat;

    protected virtual void Awake()
    {

    }

    protected virtual void Start()
    {
        anim = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponentInChildren<SpriteRenderer>();
        originalMat = sr.material;
    }

    protected virtual void Update()
    {

    }

    //Collision Methods
    public virtual bool IsGrounded() => Physics2D.Raycast(groundCheck.position, Vector2.down, groundCheckDistance, groundMask);
    public virtual bool OnWall() => Physics2D.Raycast(wallCheck.position, Vector2.right * facingDir, wallCheckDistance, groundMask);

    //Velocity Methods
    public virtual void SetVelocity(float horVelocity, float verVelocity)
    {
        if (isKnocked)
            return;

        rb.linearVelocity = new Vector2(horVelocity, verVelocity);
        FlipController(horVelocity);
    }

    //Flip Methods
    public virtual void FlipController(float horVelocity)
    {
        if (horVelocity > 0 && !isFacingRight)
            Flip();
        else if (horVelocity < 0 && isFacingRight)
            Flip();
    }

    public virtual void Flip()
    {
        isFacingRight = !isFacingRight;
        facingDir = facingDir * -1;
        transform.Rotate(0, 180, 0);
    }

    public virtual void DamageImpact() => StartCoroutine("HitRoutine");

    private IEnumerator HitRoutine()
    {
        isKnocked = true;

        rb.linearVelocity = new Vector2(knockbackDirection.x * -facingDir, knockbackDirection.y);
        yield return new WaitForSeconds(knockbackDuration);

        isKnocked = false;
    }

    public virtual void StartFX() => StartCoroutine("FlashFX");

    private IEnumerator FlashFX()
    {
        sr.material = hitMat;

        Color currentColor = sr.color;
        sr.color = Color.white;
        yield return new WaitForSeconds(effectDuration);
        
        sr.color = currentColor;
        sr.material = originalMat;
    }

    protected virtual void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawLine(groundCheck.position, new Vector3(groundCheck.position.x, groundCheck.position.y - groundCheckDistance));
        Gizmos.DrawLine(wallCheck.position, new Vector3(wallCheck.position.x + wallCheckDistance * facingDir, wallCheck.position.y));
        Gizmos.color = Color.black;
        Gizmos.DrawWireSphere(attackCheck.position, attackCheckRadius);
    }
}
