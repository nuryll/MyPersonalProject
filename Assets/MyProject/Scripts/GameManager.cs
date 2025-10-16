using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance; 

    public TextMeshProUGUI scoreText;
    private int score = 0;

    public GameOverUI gameOverUI;

    public static bool skipMainMenu = false;

    void Start()
    {
        UpdateScoreUI();
    }

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void AddScore(int points)
    {
        score += points;
        UpdateScoreUI();
    }

    void UpdateScoreUI()
    {
        if (scoreText != null)
            scoreText.text = "Score: " + score;
    }

    public void GameOver()
    {
        Debug.Log("GAME OVER!");
        Time.timeScale = 0f;

        if (gameOverUI != null)
        {
            gameOverUI.ShowGameOver(score);
            Debug.Log("GameOverUI found and activated.");
        }
        else
        {
            Debug.LogWarning("GameOverUI not assigned in inspector!");
        }
    }
    public void ResetForRestart()
    {
        skipMainMenu = true; // tell MainMenuUI to skip menu on next scene load
        score = 0;
    }
}
