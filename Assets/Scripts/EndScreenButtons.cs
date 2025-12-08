using UnityEngine;
using UnityEngine.SceneManagement;

public class EndScreenButtons : MonoBehaviour
{
    public void LoadMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
        MainManager.Instance.CoinCount = 0;
        MainManager.Instance.LevelCount = 1;
        MainManager.Instance.FisherCleanup = 0;
        MainManager.Instance.ODeprivedCleanup = 0;
        MainManager.Instance.MPlasticCleanup = 0;
        MainManager.Instance.ODeprivedCleanup = 0;
        MainManager.Instance.ToxicWasteCleanup = 0;
    }

    public void QuitGame()
    {
        Debug.Log("Quitting game...");
        Application.Quit();
    }
}
