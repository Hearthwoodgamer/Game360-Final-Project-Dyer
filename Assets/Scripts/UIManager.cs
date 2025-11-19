using TMPro;
using Unity.VisualScripting;
using UnityEngine;


public class UIManager : MonoBehaviour
{
    [Header("UI Elements")]
    public TMP_Text scoreText;
    public TMP_Text timerText;
    public TMP_Text stateText;
    public TMP_Text coinText;
    public TMP_Text finalScoreText;
    public GameObject gameOverPanel;
    public GameObject victoryPanel;
    public GameObject pauseMenu;

    private int coinCount = 0;

    void Start()
    {
        Debug.Log("✅ UIManager subscribing to events...");

        // Subscribe to events (OBSERVER PATTERN)
        EventManager.Subscribe("OnScoreChanged", UpdateScore);
        EventManager.Subscribe("OnFinalScoreChanged", updateFinalScore);
        EventManager.Subscribe("OnPlayerStateChanged", UpdateStateDisplay);
        EventManager.Subscribe("OnGameOver", ShowGameOver); // GAME OVER
        EventManager.Subscribe("OnLevelComplete", ShowVictory); // VICTORY
        EventManager.Subscribe("OnCoinCollected", UpdateCoinCount);
        EventManager.Subscribe("PauseGame", showPauseMenu);
        EventManager.Subscribe("ResumeGame", hidePauseMenu);

        // Initialize
        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(false);
            Debug.Log("Game Over Panel disabled");
        }

        if (victoryPanel != null)
        {
            victoryPanel.SetActive(false);
            Debug.Log("Victory Panel disabled");
        }

        if (coinText != null)
        {
            coinText.text = "Cherries: 0";
        }
    }

    void Update()
    {
        // Update timer
        if (timerText && GameManager.Instance != null)
        {
            float time = GameManager.Instance.GetTimeRemaining();
            int minutes = Mathf.FloorToInt(time / 60);
            int seconds = Mathf.FloorToInt(time % 60);
            timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        }
    }

    void OnDestroy()
    {
        // Unsubscribe to prevent errors on scene change
        EventManager.Unsubscribe("OnScoreChanged", UpdateScore);
        EventManager.Unsubscribe("OnFinalScoreChanged", updateFinalScore);
        EventManager.Unsubscribe("OnPlayerStateChanged", UpdateStateDisplay);
        EventManager.Unsubscribe("OnGameOver", ShowGameOver);
        EventManager.Unsubscribe("OnLevelComplete", ShowVictory);
        EventManager.Unsubscribe("OnCoinCollected", UpdateCoinCount);
        EventManager.Unsubscribe("PauseGame", showPauseMenu);
        EventManager.Unsubscribe("ResumeGame", hidePauseMenu);
    }

    void UpdateScore(object scoreData)
    {
        if (scoreText != null)
        {
            
            scoreText.text = "Score: " + scoreData.ToString(); 
        }
    }

    void updateFinalScore(object scoreData)
    {
        if (finalScoreText != null)
        {
            float finalTime = GameManager.Instance.GetTimeRemaining();
            float finalScore = GameManager.Instance.GetScore();
            if (finalScore < 0)
            {
                finalScore = -finalScore;
                finalScore = 100 / (finalScore);
                
                
            }
            finalScoreText.text = "Final Score: " + (finalScore) * finalTime;
            
        }
    }
         

    void UpdateStateDisplay(object stateData)
    {
        if (stateText != null)
        {
            stateText.text = "State: " + stateData.ToString();
        }
    }

    void UpdateCoinCount(object coinValue)
    {
        coinCount++;
        if (coinText != null)
        {
            coinText.text = "Cherries: " + coinCount;
        }
    }

    void ShowGameOver(object finalScore)
    {
        Debug.Log("🔴 SHOWING GAME OVER PANEL");

        // Make sure victory panel is hidden
        if (victoryPanel != null)
        {
            victoryPanel.SetActive(false);
        }

        if (pauseMenu != null)
        {
            pauseMenu.SetActive(false);
        }

        // Show game over panel
        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(true);
            Debug.Log("Game Over Panel activated");
        }
        else
        {
            Debug.LogError("❌ Game Over Panel is NULL!");
        }
    }

    void ShowVictory(object finalScore)
    {
        Debug.Log("🟢 SHOWING VICTORY PANEL");

        // Make sure game over panel is hidden
        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(false);
        }

        if (pauseMenu != null)
        {
            pauseMenu.SetActive(false);
        }

        // Show victory panel
        if (victoryPanel != null)
        {
            victoryPanel.SetActive(true);
            Debug.Log("Victory Panel activated");
        }
        else
        {
            Debug.LogError("❌ Victory Panel is NULL!");
        }
    }

    public void showPauseMenu()
    {
    
            pauseMenu.SetActive(true);
    
    
    }

    public void hidePauseMenu()
    {
        
        pauseMenu.SetActive(false);
    }

    public void OnRestartButton()
    {
        if (GameManager.Instance != null)
        {
            coinCount = 0;
            GameManager.Instance.RestartGame();
        }
    }

    public int GetCoinCount()
    {
        return coinCount;
    }
}