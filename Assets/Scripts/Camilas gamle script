using TMPro;
using UnityEngine;

public class CoinCollection : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    
    private int Coin = 0;

    public TextMeshProUGUI coinText;

    private void OnTriggerEnter2D(Collider2D other)

}
{
    
       
        if (other.transform.tag == "Coin")
        {
            Coin++;
            coinText.text = "Coins:" + Coin.ToString();
            Debug.Log(Coin);
            Destroy (other.gameObject);
        }

        
    
}
