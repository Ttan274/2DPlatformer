using UnityEngine;

public class Projectile : MonoBehaviour
{
    private Vector3 attackDirection;
    [SerializeField] private float speed;

    private void Update()
    {
        transform.Translate(attackDirection * speed * Time.deltaTime);
    }

    public void SetupDirection(Vector3 direction)
    {
        attackDirection = direction;
    }
}
