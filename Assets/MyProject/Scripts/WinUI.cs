using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class WinUI : MonoBehaviour
{
    public GameObject winPanel;          // The UI panel
    public TextMeshProUGUI scoreText;   // Text to show score
    private bool hasWon = false;

    void Start()
    {
        if (winPanel != null)
            winPanel.SetActive(false);  // hide panel at start
    }

    public void ShowWin(int finalScore)
    {
        if (hasWon) return;
        hasWon = true;

        Time.timeScale = 0f;

        if (winPanel != null)
            winPanel.SetActive(true);

        if (scoreText != null)
            scoreText.text = $"SCORE: {finalScore}";
    }



    public void OnRestartPressed()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); // reload scene
    }
}

