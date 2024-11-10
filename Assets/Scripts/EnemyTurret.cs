using System;
using UnityEngine;

public class EnemyTurret : MonoBehaviour
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
    
    private void Update()
    {
        attackCooldownTimer += Time.deltaTime; 

        if (IsPlayerInSight()) 
        {
            if(attackCooldownTimer >= attackCooldown)
            {
                attackCooldownTimer = 0;
                Instantiate(bullet, attackPoint.position, Quaternion.identity).GetComponent<Projectile>().SetupDirection(Vector3.right);
            }
        }
    }

    private bool IsPlayerInSight()
    {
        RaycastHit2D hit = Physics2D.BoxCast(coll.bounds.center + transform.right * attackRange * transform.localScale.x * collDistance,
            new Vector3(coll.bounds.size.x * attackRange, coll.bounds.size.y, coll.bounds.size.z), 0, Vector2.left, 0, playerLayer);

        return hit.collider != null;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(coll.bounds.center + transform.right * attackRange * transform.localScale.x * collDistance,
            new Vector3(coll.bounds.size.x * attackRange, coll.bounds.size.y, coll.bounds.size.z));
    }
}
