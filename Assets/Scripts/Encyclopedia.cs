using UnityEngine;
using UnityEngine.SceneManagement;

public class Changescene : MonoBehaviour
{
  public void LoadScene(int sceneID)
{
    SceneManager.LoadScene("Encyclopedia");
}
}
