using UnityEngine;
using UnityEngine.SceneManagement;

public class Encyclopedia : MonoBehaviour
{
  public void LoadScene(int sceneID)
{
    SceneManager.LoadScene("Encyclopedia");
}
}
