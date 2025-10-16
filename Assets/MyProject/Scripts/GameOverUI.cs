using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameOverUI : MonoBehaviour
{
    [Header("Panels")]
    public GameObject gameOverPanel;
    public TextMeshProUGUI scoreText;

    private bool isGameOver = false;

    void Start()
    {
        if (gameOverPanel != null)
            gameOverPanel.SetActive(false); // hide at start
    }

    public void ShowGameOver(int finalScore)
    {
        if (isGameOver) return;
        isGameOver = true;

        // Freeze game
        Time.timeScale = 0f;

        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(true);
            if (scoreText != null)
                scoreText.text = $"SCORE: {finalScore}";
        }
    }

    // === Restart button pressed ===
    public void OnRestartPressed()
    {
        Debug.Log("Restart pressed, reloading scene...");
        Time.timeScale = 1f;

        // Tell GameManager to skip main menu
        GameManager.Instance.ResetForRestart();

        // Reload current scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

}


