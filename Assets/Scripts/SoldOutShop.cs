using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ShopItemVisual : MonoBehaviour
{
    public Button buyButton;         // The button for purchasing
    public GameObject soldOutPanel;  // The red panel covering the whole item
    public int ItemPrice;
    //public int[] CleanupType = new int[4];
    public string ItemType = "Fisher, Oil, MPlastic, ODeprived or ToxicWaste";

    private bool isSoldOut = false;

    private void Start()
    {
        soldOutPanel.SetActive(false);        // Hide panel initially
        buyButton.onClick.AddListener(PurchaseItem);
        //oreach (int value in CleanupType)
        //{    print(value);}

        //CleanupType[0] = 0;     //MainManager.Instance.FisherCleanup
        //CleanupType[1] = 1;     //MainManager.Instance.OilCleanup
        //CleanupType[2] = 2;     //MainManager.Instance.MPlasticCleanup
        //CleanupType[3] = 3;     //MainManager.Instance.ODeprivedCleanup
        //CleanupType[4] = 4;     //MainManager.Instance.ToxicWasteCleanup
    }

    public void PurchaseItem()
    {
        if (isSoldOut) return;
        if (MainManager.Instance.CoinCount - ItemPrice < 0) return;

        // Add your purchase logic here (currency deduction, inventory, etc.)
        Debug.Log("Item purchased!");
        MainManager.Instance.CoinCount -= ItemPrice;
        if (ItemType == "Fisher")
        {
            MainManager.Instance.FisherCleanup++;
        }
        if (ItemType == "Oil")
        {
            MainManager.Instance.OilCleanup++;
        }
        if (ItemType == "MPlastic")
        {
            MainManager.Instance.MPlasticCleanup++;
        }
        if (ItemType == "ODeprived")
        {
            MainManager.Instance.ODeprivedCleanup++;
        }
        if (ItemType == "ToxicWasteCleanup")
        {
            MainManager.Instance.ToxicWasteCleanup++;
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
