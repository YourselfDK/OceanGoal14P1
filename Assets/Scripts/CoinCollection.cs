using TMPro;
using UnityEngine;

public class CoinCollection : MonoBehaviour
{
    private int Coin = 0;
    public TextMeshProUGUI coinText; 



    private void OnTriggerEnter2D(Collider2D other) 
    { 

    if (other.transform.tag == "Coin")
    {

            
        Coin++;
        coinText.text = "=" + Coin.ToString();
        Debug.Log(Coin);
        Destroy(other.gameObject);

       
        }

    }

    }

