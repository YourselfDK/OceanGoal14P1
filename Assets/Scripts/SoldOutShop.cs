using System.Linq.Expressions;
using UnityEngine;
using UnityEngine.UI;
//using System.Collections;

public class ShopItemVisual : MonoBehaviour
{
    public Button buyButton;         // The button for purchasing
    public GameObject soldOutPanel;  // The red panel covering the whole item
    
    public int ItemPrice;
    public GameObject[] CleanUpType;
    public string ItemType = "Fisher, Oil, MPlastic, ODeprived, ToxicWaste or Vote";

    private bool isSoldOut = false;

    [SerializeField] ShopCoinCounter shopCoinCounter;
    [SerializeField] AudioSource shopAudioSource;

    private void Start()
    {
        soldOutPanel.SetActive(false);        // Hide panel initially
        buyButton.onClick.AddListener(PurchaseItem);
        foreach (GameObject go in CleanUpType)
        {
            print(go);
            if(MainManager.Instance.VoteBought == true)
            {
                go.SetActive(true);
            }
            else
            {
                go.SetActive(false);
            }

            if (ItemType == "ProtectedArea" && MainManager.Instance.ProtectedAreaBought)
            {
                isSoldOut = true;
                UpdateUI();
            }

            if (ItemType == "BurnOil" && MainManager.Instance.BurnOilBought)
            {
                isSoldOut = true;
                UpdateUI();
            }
            if (ItemType == "MechanicalRemoval" && MainManager.Instance.MechanicalRemovalBought)
            {
                isSoldOut = true;
                UpdateUI();
            }

            if (ItemType == "PublicTransport" && MainManager.Instance.PublicTransportBought)
            {
                isSoldOut = true;
                UpdateUI();
            }
            if (ItemType == "WashingMachineLaw" && MainManager.Instance.WashingMachineLawBought)
            {
                isSoldOut = true;
                UpdateUI();
            }

            if (ItemType == "OrganicFarming" && MainManager.Instance.OrganicFarmingBought)
            {
                isSoldOut = true;
                UpdateUI();
            }
            if (ItemType == "WastewaterCleanup" && MainManager.Instance.WasteWaterCleanupBought)
            {
                isSoldOut = true;
                UpdateUI();
            }

            if (ItemType == "MinisterOfTheEnvironment" && MainManager.Instance.MinisterOfTheEnvironmentBought)
            {
                isSoldOut = true;
                UpdateUI();
            }
            if (ItemType == "Vote" && MainManager.Instance.VoteBought)
            {
                isSoldOut = true;
                UpdateUI();
            }
        }

        //CleanUpType = new GameObject[7];
        //CleanUpType[1] = new GameObject();
    }

    public void PurchaseItem()
    {
        if (isSoldOut) return;
        if (MainManager.Instance.CoinCount - ItemPrice < 0) return;

        // Add your purchase logic here (currency deduction, inventory, etc.)
        Debug.Log("Item purchased!");
        MainManager.Instance.CoinCount -= ItemPrice;
        shopCoinCounter.UpdateBankAccount(MainManager.Instance.CoinCount);
        shopAudioSource.Play();

        if (ItemType == "ProtectedArea")
        {
            MainManager.Instance.FisherCleanup++;
            MainManager.Instance.ProtectedAreaBought = true;
        }

        if (ItemType == "BurnOil")
        {
            MainManager.Instance.OilCleanup++;
            MainManager.Instance.BurnOilBought = true;
        }
        if (ItemType == "MechanicalRemoval")
        {
            MainManager.Instance.OilCleanup++;
            MainManager.Instance.MechanicalRemovalBought = true;
        }

        if (ItemType == "PublicTransport")
        {
            MainManager.Instance.MPlasticCleanup++;
            MainManager.Instance.PublicTransportBought = true;
        }
        if (ItemType == "WashingMachineLaw")
        {
            MainManager.Instance.MPlasticCleanup++;
            MainManager.Instance.WashingMachineLawBought = true;
        }

        if (ItemType == "OrganicFarming")
        {
            MainManager.Instance.ODeprivedCleanup++;
            MainManager.Instance.OrganicFarmingBought = true;
        }
        if (ItemType == "WastewaterCleanup")
        {
            MainManager.Instance.ODeprivedCleanup++;
            MainManager.Instance.WasteWaterCleanupBought = true;
        }

        if (ItemType == "MinisterOfTheEnvironment")
        {
            MainManager.Instance.ToxicWasteCleanup++;
            MainManager.Instance.MinisterOfTheEnvironmentBought = true;
        }
        if (ItemType == "Vote")
        {
            MainManager.Instance.ToxicWasteCleanup++;
            MainManager.Instance.VoteBought = true;
            foreach (GameObject go in CleanUpType)
            {
                go.SetActive(true);
                Debug.Log(go+"Was succesfully found");
            }
        }

        isSoldOut = true;
        UpdateUI();
    }

    private void UpdateUI()
    {
        buyButton.interactable = !isSoldOut;      // Disable button
        soldOutPanel.SetActive(isSoldOut);        // Show red panel
    }
}
