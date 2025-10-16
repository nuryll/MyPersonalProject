using UnityEngine;

public class MainMenuUI : MonoBehaviour
{
    [Header("Panels")]
    public GameObject mainMenuPanel;  // Start + Info buttons
    public GameObject infoPanel;      // Info panel with text

    [Header("Gameplay Objects")]
    public GameObject spawnManager;
    public GameObject fences;
    public PlayerController player;
    public GameObject uiGame;         // The in-game UI (score, speed, etc.)

    [Header("Background Music")]
    public GameObject backgroundMusic; // BackgroundMusicManager object

    private void Start()
    {
        // Show only main menu at first
        mainMenuPanel.SetActive(true);
        infoPanel.SetActive(false);

        // Disable all gameplay-related objects
        if (spawnManager != null) spawnManager.SetActive(false);
        if (fences != null) fences.SetActive(false);
        if (player != null) player.enabled = false;
        if (uiGame != null) uiGame.SetActive(false);

        // Background music off initially
        if (backgroundMusic != null)
            backgroundMusic.SetActive(false);
    }

    // === Called by Start button ===
    public void OnStartGamePressed()
    {
        // Hide menu and info
        mainMenuPanel.SetActive(false);
        infoPanel.SetActive(false);

        // Enable gameplay systems
        if (spawnManager != null) spawnManager.SetActive(true);
        if (fences != null) fences.SetActive(true);
        if (player != null) player.enabled = true;
        if (uiGame != null) uiGame.SetActive(true);

        // Turn ON background music
        if (backgroundMusic != null)
        {
            backgroundMusic.SetActive(true);
            BackgroundMusicManager musicManager = backgroundMusic.GetComponent<BackgroundMusicManager>();
            if (musicManager != null)
                musicManager.PlayMusic();
        }
    }

    // === Called by Info button ===
    public void OnInfoPressed()
    {
        mainMenuPanel.SetActive(false);
        infoPanel.SetActive(true);
    }

    // === Called by Back button ===
    public void OnBackPressed()
    {
        infoPanel.SetActive(false);
        mainMenuPanel.SetActive(true);
    }
}



