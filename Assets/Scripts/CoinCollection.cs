using TMPro;
using UnityEngine;

public class CoinCollection : MonoBehaviour
{
    // If you want to show coin text right on the player HUD,
    // you can still have a local Text reference, but the CoinManager
    // will send events to update UI. That keeps logic separated.

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Coin"))
        {
            // Add one coin via the manager
            if (CoinManager.Instance != null)
            {
                CoinManager.Instance.AddCoins(50);
            }
            else
            {
                Debug.LogWarning("CoinManager instance not found!");
             
            }
            

            Destroy(other.gameObject);
        }
    }
}

