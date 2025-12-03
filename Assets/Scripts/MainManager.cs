using TMPro;
using UnityEngine;

public class MainManager : MonoBehaviour
{

    public static MainManager Instance;
    public int HealthCount;
    public int CoinCount;
    public int LevelCount;
    public bool InShop = false;

    public int FisherCleanup = 0; //Protected Area
    public int OilCleanup = 0; // Burn Oil + Mechanical Removal
    public int MPlasticCleanup = 0; //Public Transport + Washing Machine Law
    public int ODeprivedCleanup = 0; //Organic Farming + Wastewater Cleanup
    public int ToxicWasteCleanup = 0; //Minister of the Environment + Vote

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

   
}
