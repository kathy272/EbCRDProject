using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FruitsUI : MonoBehaviour
{
    public GameObject FruitUI;
    public Image powerUpImage; // Image component to show the fruit icon
    public TextMeshProUGUI timerText; // Text to show the time left
    public Sprite lemonSprite;
    public Sprite grapesSprite;
    public Sprite berrySprite;
    public Sprite emptySprite;

    private Coroutine currentTimerCoroutine;


    public void ShowPowerUpUI(PowerUpType type, float duration)
    {
        // Set the corresponding image based on the power-up type
        switch (type)
        {
            case PowerUpType.SpeedBoost:
                powerUpImage.sprite = grapesSprite;
                break;
            case PowerUpType.HighJump:
                powerUpImage.sprite = berrySprite;
                break;
            case PowerUpType.SpeedDecrease:
                powerUpImage.sprite = lemonSprite;
                break;
        }

        // Start the timer countdown
        if (currentTimerCoroutine != null)
        {
            StopCoroutine(currentTimerCoroutine);
        }
        currentTimerCoroutine = StartCoroutine(StartTimer(duration));
    }

    private IEnumerator StartTimer(float duration)
    {
        float timeLeft = duration;
        while (timeLeft > 0)
        {
            timerText.text = timeLeft.ToString("F1") + "s"; // Update timer text
            timeLeft -= Time.deltaTime;
            yield return null;
        }

        // Hide the UI elements when the timer ends
        
        timerText.text = "";
        powerUpImage.sprite = emptySprite;


    }
}