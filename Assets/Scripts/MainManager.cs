using TMPro;
using UnityEngine;

public class MainManager : MonoBehaviour
{

    public static MainManager Instance;
    public int HealthCount;
    public int CoinCount;
 

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

   
}
