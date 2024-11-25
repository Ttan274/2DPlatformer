using UnityEngine;

public class EnemyDrop : MonoBehaviour
{
    [SerializeField] private GameObject goldPrefab;

    public void DropItem()
    {
        GameObject dropObject = Instantiate(goldPrefab, transform.position, Quaternion.identity);
        Vector2 randomVelocity = new Vector2(Random.Range(-5, 5), Random.Range(15, 20));
        dropObject.GetComponent<Rigidbody2D>().AddForce(randomVelocity);
    }
}
