using UnityEngine;

public interface IHealth
{
    public float currentHealth {  get; set; }
    public void TakeDamage(float damage);
    public void Die();
}
