using UnityEngine;
using UnityEngine.UI;

public class ShopItemVisual : MonoBehaviour
{
    public Button buyButton;         // The button for purchasing
    public GameObject soldOutPanel;  // The red panel covering the whole item
    public int ItemPrice;

    private bool isSoldOut = false;

    private void Start()
    {
        soldOutPanel.SetActive(false);        // Hide panel initially
        buyButton.onClick.AddListener(PurchaseItem);
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
