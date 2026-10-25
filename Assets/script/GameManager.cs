
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public static bool IsGameOver { get; private set; }

    [SerializeField] private GameObject gameOverPanel;

    private void Awake()
    {
        Instance = this;
        IsGameOver = false;
        Time.timeScale = 1f;

        if (gameOverPanel) gameOverPanel.SetActive(false);
    }

    public void GameOver()
    {
        if (IsGameOver) return;

        IsGameOver = true;

        if (gameOverPanel) gameOverPanel.SetActive(true);

        Time.timeScale = 0f;
        Debug.Log("GAME OVER!");
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        UnityEngine.SceneManagement.SceneManager.LoadScene(
            UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex
        );
    }
}
