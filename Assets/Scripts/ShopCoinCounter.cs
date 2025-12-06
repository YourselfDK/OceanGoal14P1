using TMPro;
using UnityEngine;

public class ShopCoinCounter : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI bankAccountShower;
 
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        bankAccountShower.text = "Coins = " + MainManager.Instance.CoinCount.ToString();
    }
    public void UpdateBankAccount(int money)
    {
        bankAccountShower.text = "Coins = " + money.ToString();
    }
}
