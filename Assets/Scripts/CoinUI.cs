using TMPro;
using UnityEngine;

public class CoinUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI coinText;

    private void OnEnable()
    {
        if (CoinManager.Instance != null)
        {
            // set initial value
            coinText.text = "Coins: " + CoinManager.Instance.Coins.ToString();
            // subscribe to updates
            CoinManager.Instance.OnCoinsChanged += OnCoinsChanged;
        }
    }

    private void OnDisable()
    {
        if (CoinManager.Instance != null)
        {
            CoinManager.Instance.OnCoinsChanged -= OnCoinsChanged;
        }
    }

    private void OnCoinsChanged(int newAmount)
    {
        coinText.text = "Coins: " + newAmount.ToString();
    }
}
