using UnityEngine;

public class SpikeballTrap : MonoBehaviour
{
    [SerializeField] private float moveDistance;
    [SerializeField] private float speed;
    [SerializeField] private float rotationSpeed;
    [SerializeField] private float damage;

    private float leftPos, rightPos;
    [SerializeField] private bool isMovingRight = true;
    private Transform spikeObject;

    private void Awake()
    {
        spikeObject = transform.GetChild(0);
        rightPos = transform.position.x + moveDistance;
        leftPos = transform.position.x - moveDistance;
    }

    private void Update()
    {
        spikeObject.Rotate(0, 0, rotationSpeed);

        Movement();
    }

    private void Movement()
    {
        if (isMovingRight)
        {
            if (transform.position.x < rightPos)
                transform.position = new Vector3(transform.position.x + speed * Time.deltaTime, transform.position.y, transform.position.z);
            else
                isMovingRight = false;
        }
        else
        {
            if (transform.position.x > leftPos)
                transform.position = new Vector3(transform.position.x - speed * Time.deltaTime, transform.position.y, transform.position.z);
            else
                isMovingRight = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
            other.GetComponent<Player>().DamageBehaviour(damage);
    }
}
