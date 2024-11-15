using UnityEngine;
using UnityEngine.UI;

public class EnemyTurret : MonoBehaviour, IHealth
{
    [Header("Player Detection Parameters")]
    [SerializeField] private BoxCollider2D coll;
    [SerializeField] private float collDistance;
    [SerializeField] private LayerMask playerLayer;

    [Header("Attack Parameters")]
    [SerializeField] private float attackRange;
    [SerializeField] private float attackCooldown;
    private float attackCooldownTimer = Mathf.Infinity;
    [SerializeField] private GameObject bullet;
    [SerializeField] private Transform attackPoint;
    [SerializeField] private bool isRightTurret;
    private Vector3 attackDir;

    [Header("Health Parameters")]
    [SerializeField] private float maxHealth;
    [SerializeField] private Slider slider;
    [SerializeField] private GameObject explosionPrefab;
    public float currentHealth { get; set; }

    private void Start()
    {
        currentHealth = maxHealth;
        attackDir = isRightTurret ? Vector3.right : Vector3.left;
    }

    private void Update()
    {
        attackCooldownTimer += Time.deltaTime; 

        if (IsPlayerInSight()) 
        {
            if(attackCooldownTimer >= attackCooldown)
            {
                attackCooldownTimer = 0;
                Instantiate(bullet, attackPoint.position, Quaternion.identity).GetComponent<Projectile>().SetupDirection(attackDir);
            }
        }
    }

    private bool IsPlayerInSight()
    {
        RaycastHit2D hit = Physics2D.BoxCast(coll.bounds.center + transform.right * attackRange * transform.localScale.x * collDistance,
            new Vector3(coll.bounds.size.x * attackRange, coll.bounds.size.y, coll.bounds.size.z), 0, Vector2.left, 0, playerLayer);
        
        return hit.collider != null && !hit.collider.GetComponent<Player>().isDead;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(coll.bounds.center + transform.right * attackRange * transform.localScale.x * collDistance,
            new Vector3(coll.bounds.size.x * attackRange, coll.bounds.size.y, coll.bounds.size.z));
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        UpdateHealth();

        if (currentHealth <= 0)
            Die();
    }

    private void UpdateHealth()
    {
        slider.maxValue = maxHealth;
        slider.value = currentHealth;
    }

    public void Die()
    {
        Instantiate(explosionPrefab, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
