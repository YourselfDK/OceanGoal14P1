using TMPro;
using UnityEngine;

public class MainManager : MonoBehaviour
{

    public static MainManager Instance;
    public float HealthCount;
    private int CoinCount;
    public TextMeshProUGUI coinText;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {

        if (other.transform.tag == "Coin")
        {


            CoinCount++;
            coinText.text = "Coins:" + CoinCount.ToString();
            Debug.Log(CoinCount);
            Destroy(other.gameObject);


        }

    }
}
