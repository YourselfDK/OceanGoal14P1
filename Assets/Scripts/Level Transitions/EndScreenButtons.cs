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
        MainManager.Instance.Timer = 0;

        MainManager.Instance.ProtectedAreaBought = false;
        MainManager.Instance.BurnOilBought = false;
        MainManager.Instance.MechanicalRemovalBought = false;
        MainManager.Instance.PublicTransportBought = false;
        MainManager.Instance.WashingMachineLawBought = false;
        MainManager.Instance.OrganicFarmingBought = false;
        MainManager.Instance.WasteWaterCleanupBought = false;
        MainManager.Instance.MinisterOfTheEnvironmentBought = false;
        MainManager.Instance.VoteBought = false;
        MainManager.Instance.PlayerIsRed = false;
    }

    public void QuitGame()
    {
        Debug.Log("Quitting game...");
        Application.Quit();
    }
}
