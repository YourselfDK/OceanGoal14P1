using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShopItem : MonoBehaviour
{
    public int cost = 50;
    public Button purchaseButton;
    public TextMeshProUGUI costText; // optional, show cost on card

    private void Start()
    {
        if (costText != null) costText.text = cost + " coins";

        if (purchaseButton != null)
            purchaseButton.onClick.AddListener(AttemptPurchase);

        // optional: disable button when not enough coins (subscribe to changes)
        if (CoinManager.Instance != null)
            CoinManager.Instance.OnCoinsChanged += UpdateButtonState;

        UpdateButtonState(CoinManager.Instance != null ? CoinManager.Instance.Coins : 0);
    }

    private void OnDestroy()
    {
        if (CoinManager.Instance != null)
            CoinManager.Instance.OnCoinsChanged -= UpdateButtonState;
    }

    public void AttemptPurchase()
    {
        if (CoinManager.Instance == null) return;

        bool success = CoinManager.Instance.SpendCoins(cost);
        if (success)
        {
            // purchase success: grant item, disable UI, show sold-out etc.
            Debug.Log("Purchased item for " + cost);
            // Example: disable button
            if (purchaseButton != null) purchaseButton.interactable = false;
            // TODO: add logic to add item to player inventory
        }
        else
        {
            Debug.Log("Not enough coins!");
            // TODO: show visual feedback to player (e.g., shake UI, play sound)
        }
    }

    private void UpdateButtonState(int coinAmount)
    {
        if (purchaseButton != null)
            purchaseButton.interactable = coinAmount >= cost;
    }
}