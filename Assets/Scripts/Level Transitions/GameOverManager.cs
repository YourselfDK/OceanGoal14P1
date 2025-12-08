using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour
{
    public GameObject gameOverUI;   // Assign your Game Over panel in Inspector

    public static bool GameIsOver = false;

    void Start()
    {
        // Make sure game starts unpaused
        Time.timeScale = 1f;
        GameIsOver = false;
        gameOverUI.SetActive(false);
    }

    public void GameOver()
    {
        if (GameIsOver) return;

        GameIsOver = true;
        gameOverUI.SetActive(true);
        Time.timeScale = 0f;  // Pause game
        Debug.Log("Player has died");
    }

    public void RestartLevel()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void LoadMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Main Menu");
    }

    public void QuitGame()
    {
        Debug.Log("Quit Game...");
        Application.Quit();
    }
}

