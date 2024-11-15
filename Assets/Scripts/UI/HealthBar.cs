using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Slider slider;
    private RectTransform rect;
    private EnemyKnight knight;

    private void Start()
    {
        knight = GetComponentInParent<EnemyKnight>();
        rect = GetComponent<RectTransform>();

        if (knight != null)
        {
            knight.onHealthChange += UpdateHealth;
            knight.onFlipped += FlipHealthbar;
        }
    }

    private void UpdateHealth()
    {
        slider.maxValue = knight.maxHealth;
        slider.value = knight.currentHealth;
    }

    private void FlipHealthbar() => rect.Rotate(0, 180, 0);

    private void OnDisable()
    {
        knight.onHealthChange -= UpdateHealth;
        knight.onFlipped -= FlipHealthbar;
    }
}
