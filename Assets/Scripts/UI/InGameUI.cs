using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InGameUI : MonoBehaviour
{
    [SerializeField] private Slider slider;
    [SerializeField] private TextMeshProUGUI timerTxt;
    [SerializeField] private GameObject pausePanel;
    private Player player;

    private float secondsCount;
    private int minuteCount;
    private bool isGameStopped = false;
    private float timeScale;

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

    private void Update()
    {
        if (isGameStopped)
            return;

        TimerBehaviour();

        if (Input.GetKeyDown(KeyCode.P) && !isGameStopped)
            StopGame();
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

    private void StopGame()
    {
        isGameStopped = true;
        pausePanel.SetActive(true);

        timeScale = Time.timeScale;
        Time.timeScale = 0f;
    }

    public void ContinueGame()
    {
        isGameStopped = false;
        pausePanel.SetActive(false);

        Time.timeScale = timeScale;
    }
}
