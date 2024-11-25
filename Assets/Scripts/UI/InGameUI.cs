using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InGameUI : MonoBehaviour
{
    [SerializeField] private Slider slider;
    [SerializeField] private TextMeshProUGUI timerTxt;
    [SerializeField] private TextMeshProUGUI goldTxt;
    private Player player;

    private float secondsCount;
    private int minuteCount;

    private void Start()
    {
        player = PlayerManager.instance.player;

        if (player != null)
        {
            player.onHealthChange += UpdateHealth;
            player.onGoldCollected += GoldBehaviour;
        }
    }

    private void UpdateHealth()
    {
        slider.maxValue = player.maxHealth;
        slider.value = player.currentHealth;
    }

    private void Update()
    {
        TimerBehaviour();
    }

    private void TimerBehaviour()
    {
        secondsCount += Time.deltaTime;
        if(secondsCount >= 60)
        {
            minuteCount++;
            secondsCount = 0;
        }
        timerTxt.text = String.Format("{0:00}:{1:00}", minuteCount, secondsCount);
    }

    private void GoldBehaviour()
    {
        goldTxt.text = player.goldCounter.ToString();
    }
}
