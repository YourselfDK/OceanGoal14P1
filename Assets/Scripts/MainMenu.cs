using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        if (MainManager.Instance.InShop)
        {
            SceneManager.LoadSceneAsync("Shop");
        }
        else
        {
            SceneManager.LoadSceneAsync(MainManager.Instance.LevelCount);
        }
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void OpenEncyclopedia()
    {
        SceneManager.LoadScene("Encyclopedia");
    }
}
