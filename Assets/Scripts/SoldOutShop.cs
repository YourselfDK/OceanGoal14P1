using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ShopItemVisual : MonoBehaviour
{
    public Button buyButton;         // The button for purchasing
    public GameObject soldOutPanel;  // The red panel covering the whole item
    public int ItemPrice;
    public int[] CleanupType = new int[4];

    private bool isSoldOut = false;

    private void Start()
    {
        soldOutPanel.SetActive(false);        // Hide panel initially
        buyButton.onClick.AddListener(PurchaseItem);
        foreach (int value in CleanupType)
        {
            print(value);
        }

        CleanupType[0] = 0;     //MainManager.Instance.FisherCleanup
        CleanupType[1] = 0;     //MainManager.Instance.OilCleanup
        CleanupType[2] = 0;     //MainManager.Instance.MPlasticCleanup
        CleanupType[3] = 0;     //MainManager.Instance.ODeprivedCleanup
        CleanupType[4] = 0;     //MainManager.Instance.ToxicWasteCleanup
    }

    public void PurchaseItem()
    {
        if (isSoldOut) return;
        if (MainManager.Instance.CoinCount - ItemPrice < 0) return;

        // Add your purchase logic here (currency deduction, inventory, etc.)
        Debug.Log("Item purchased!");
        MainManager.Instance.CoinCount -= ItemPrice;

        isSoldOut = true;
        UpdateUI();
    }

    private void UpdateUI()
    {
        buyButton.interactable = !isSoldOut;      // Disable button
        soldOutPanel.SetActive(isSoldOut);        // Show red panel
    }
}
