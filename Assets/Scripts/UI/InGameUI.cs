using UnityEngine;
using UnityEngine.UI;

public class InGameUI : MonoBehaviour
{
    [SerializeField] private Slider slider;
    private Player player;

    private void Start()
    {
        player = PlayerManager.instance.player;

        if (player != null)
            player.onHealthChange += UpdateHealth;
    }

    private void UpdateHealth()
    {
        slider.maxValue = player.maxHealth;
        slider.value = player.currentHealth;
    }

    //Timer will be added soon
    //Gold Counter will be added soon
}
