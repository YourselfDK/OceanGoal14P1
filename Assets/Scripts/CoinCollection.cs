using TMPro;
using UnityEngine;

public class CoinCollection : MonoBehaviour
{
    public int Coin = 0;
    public TextMeshProUGUI coinText;

    [SerializeField] private AudioClip coinCollectSound;

    private void Start()
    {
        int totalCoins = Coin + MainManager.Instance.CoinCount;
        coinText.text = totalCoins.ToString();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {

        if (other.transform.tag == "Coin")
        {
            Coin++;
            int totalCoins = Coin + MainManager.Instance.CoinCount;
            coinText.text = totalCoins.ToString();
            Debug.Log(Coin);
            SoundFXManager.Instance.PlaySoundFXClip(coinCollectSound, transform, 0.2f);

            Destroy(other.gameObject);


        }

    }


}

