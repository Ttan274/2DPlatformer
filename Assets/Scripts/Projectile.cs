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
        CalculateRotation(direction);        
    }

    private void CalculateRotation(Vector3 direction)
    {
        float angle = Vector3.Angle(direction, transform.right);

        if (direction.y < 0)
            angle = -angle;

        gameObject.transform.GetChild(0).transform.Rotate(0, 0, angle);
    }
}
