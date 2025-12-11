using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LeaveShop : MonoBehaviour
{
    public Button exitButton;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        exitButton.onClick.AddListener(ExitShop);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            ExitShop();
        }
    }
    private void ExitShop()
    {
            MainManager.Instance.InShop = false;
            SceneManager.LoadSceneAsync(MainManager.Instance.LevelCount);
    }

}

