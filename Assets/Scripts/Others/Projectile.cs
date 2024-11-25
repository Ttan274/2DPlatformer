using UnityEngine;

public class Projectile : MonoBehaviour
{
    private Vector3 attackDirection;
    [SerializeField] private float speed;
    [SerializeField] private BulletType type;
    [SerializeField] private float damage;
    private bool isHit;

    private void Start()
    {
        Destroy(gameObject, 5f);
    }

    private void Update()
    {
        if (isHit)
            return;

        transform.Translate(attackDirection * speed * Time.deltaTime);
    }

    public void SetupDirection(Vector3 direction)
    {
        attackDirection = direction;

        if(type == BulletType.Player)
            CalculateRotation(direction);        
    }

    private void CalculateRotation(Vector3 direction)
    {
        float angle = Vector3.Angle(direction, transform.right);

        if (direction.y < 0)
            angle = -angle;

        gameObject.transform.GetChild(0).transform.Rotate(0, 0, angle);
    }

    private void CheckForEntities(Collider2D other)
    {
        switch (type)
        {
            case BulletType.Player:
                //Enemy Turret
                if (other.gameObject.CompareTag("EnemyTurret"))
                {
                    isHit = true;
                    Destroy(gameObject);
                    other.GetComponent<EnemyTurret>().TakeDamage(damage);
                }
                //Enemy
                if (other.gameObject.CompareTag("Enemy"))
                {
                    isHit = true;
                    Destroy(gameObject);
                    other.GetComponent<Enemy>().DamageBehaviour(damage);
                }
                break;
            case BulletType.Turret:
                if (other.gameObject.CompareTag("Player") && !other.GetComponent<Player>().isDead)
                {
                    isHit = true;
                    Destroy(gameObject);
                    other.GetComponent<Player>().DamageBehaviour(damage);
                }
                break;
            default:
                Debug.Log("You've made a mistake");
                break;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == 6)
        {
            Destroy(gameObject);
        }

        CheckForEntities(other);
    }
}


public enum BulletType
{
    Player,
    Turret,
}