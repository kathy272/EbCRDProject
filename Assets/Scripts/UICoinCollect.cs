using TMPro;
using UnityEngine;

public class UICoinCollect : MonoBehaviour
{
    public TextMeshProUGUI highScoreText;
    public TextMeshProUGUI scoreText;

    private int currentScore = 0;
    private int highScore = 0;

    void Start()
    {
        highScore = PlayerPrefs.GetInt("HighScore", 0);
        UpdateCoinCount(0);
    }

    public void AddScore(int score)
    {
        currentScore += score;

        if (currentScore > highScore)
        {
            highScore = currentScore;
            PlayerPrefs.SetInt("HighScore", highScore);
        }

        UpdateCoinCount(currentScore);
    }

    public void ResetScore(int score)
    {
        currentScore = 0;
        UpdateCoinCount(currentScore);
    }

    public void UpdateCoinCount(int count)
    {
        scoreText.text = "Score: " + currentScore;
        highScoreText.text = "Best: " + highScore;
    }
    public int GetScore()
    {
        return currentScore;
    }

}
