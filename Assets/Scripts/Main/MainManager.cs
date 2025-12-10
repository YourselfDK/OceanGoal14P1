using TMPro;
using UnityEngine;

public class MainManager : MonoBehaviour
{

    public static MainManager Instance;
    public int HealthCount;
    public int CoinCount; //Counts coins that dont reset between scenes, coins added to this wont dissapear on death!
    public int LevelCount;
    public bool InShop = false;

    public int FisherCleanup = 0; //Protected Area
    public int OilCleanup = 0; // Burn Oil + Mechanical Removal
    public int MPlasticCleanup = 0; //Public Transport + Washing Machine Law
    public int ODeprivedCleanup = 0; //Organic Farming + Wastewater Cleanup
    public int ToxicWasteCleanup = 0; //Minister of the Environment + Vote

    public bool ProtectedAreaBought = false;
    public bool BurnOilBought = false;
    public bool MechanicalRemovalBought = false;
    public bool PublicTransportBought = false;
    public bool WashingMachineLawBought = false;
    public bool OrganicFarmingBought = false;
    public bool WasteWaterCleanupBought = false;
    public bool MinisterOfTheEnvironmentBought = false;
    public bool VoteBought = false;

    public bool PlayerIsRed = false;

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
