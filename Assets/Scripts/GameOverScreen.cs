using UnityEngine;
using TMPro;

public class GameOverScreen : MonoBehaviour
{
  
    public GameObject gameOverOverlay;
    public UICoinCollect coinCountUI;

    [SerializeField] public TextMeshProUGUI highScoreText;
    [SerializeField] public TextMeshProUGUI scoreText;


    private void Start()
    {
        // Make sure the game over screen is initially inactive
        HideGameOverScreen();
    }

    public void ShowGameOverScreen()
    {
        // Activate the game over screen
        gameOverOverlay.SetActive(true);
        Debug.Log("Game Over Screen Activated");


        // Update the score and high score on the game over screen
        if (coinCountUI != null)
        {
            scoreText.text = "Score: " + coinCountUI.GetScore();
        }
        highScoreText.text = "Best: " + PlayerPrefs.GetInt("HighScore", 0);
    }
    public void HideGameOverScreen()
    {
        gameOverOverlay.SetActive(false);
    }
}
