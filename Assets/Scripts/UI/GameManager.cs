using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [SerializeField] private GameObject gameOverPanel;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        // Optional: keep across scenes if you add more scenes later
        // DontDestroyOnLoad(gameObject);

        if (gameOverPanel != null)
            gameOverPanel.SetActive(false);
    }

    public void GameOver()
    {
        // Show the panel
        if (gameOverPanel != null)
            gameOverPanel.SetActive(true);

        // Pause gameplay
        Time.timeScale = 0f;
    }

    public void Retry()
    {
        Time.timeScale = 1f; // unpause
        Scene current = SceneManager.GetActiveScene();
        SceneManager.LoadScene(current.buildIndex);
    }
}