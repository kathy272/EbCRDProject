using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public UICoinCollect coinCountUI;

    public int score = 0;

    [Header("UI Elements")]
    [SerializeField] public GameObject coinCountUIObject;
    [SerializeField] public TextMeshProUGUI highScoreText;
    [SerializeField] public TextMeshProUGUI scoreText;

    [Header("Game Over Screen")]
    [SerializeField] public GameObject gameOverScreen;
    [SerializeField] public TextMeshProUGUI gameOverScoreText;
    [SerializeField] public TextMeshProUGUI gameOverHighScoreText;
    [SerializeField] public GameOverScreen gameOverScript;

    private void Awake()
    {

        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {

        if (gameOverScreen != null)
        {
            gameOverScreen.SetActive(false);
        }

        if (coinCountUIObject != null)
        {
            coinCountUIObject.SetActive(true);
        }
      
    }


    public void StartGame()
    {
    
        ResetGame();
        gameOverScript.HideGameOverScreen();
        SceneManager.LoadScene("Game");
    }
    public void MainMenu()
    {

        SceneManager.LoadScene("Menu");
    }

    public void GameOver()
    {
        Debug.Log("Game Over");
        gameOverScript.ShowGameOverScreen();
        gameOverScreen.SetActive(true);
    }


    public void IncreaseCollectedCoins(int increaseBy)
    {
        score += increaseBy;
        if (coinCountUI != null)
        {
            coinCountUI.AddScore(increaseBy);
        }
    }

    public void ResetGame()
    {
        score = 0;
        if (coinCountUIObject != null)
        {
            coinCountUI.ResetScore(score);
        }
    }
}
