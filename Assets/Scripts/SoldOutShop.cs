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

    private void Start()
    {
        soldOutPanel.SetActive(false);        // Hide panel initially
        buyButton.onClick.AddListener(PurchaseItem);
        foreach (GameObject go in CleanUpType)
        {
            print(go);
            go.SetActive(false);
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
        if (ItemType == "Vote")
        {
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
