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
        Debug.Log("Restart pressed, restarting gameplay...");

        // Resume time
        Time.timeScale = 1f;

        // Reload the same scene (starts game fresh)
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}


