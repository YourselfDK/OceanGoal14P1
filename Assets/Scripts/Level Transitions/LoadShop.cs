using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadShop : MonoBehaviour
{
    public int WhatIsNextLevel = 1;
    [SerializeField] int WhatIsFinalLevel = 6;
    [SerializeField] CoinCollection CoinManager;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {}

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            MainManager.Instance.CoinCount = MainManager.Instance.CoinCount + CoinManager.Coin;
            Debug.Log("Coin Count = "+MainManager.Instance.CoinCount);
            MainManager.Instance.LevelCount = WhatIsNextLevel;
            if (WhatIsNextLevel != WhatIsFinalLevel)
            {
                MainManager.Instance.InShop = true;
                SceneManager.LoadScene("Shop");
            }
            else
            {
                SceneManager.LoadScene("Level 6");
            }
        }
    }

    // Update is called once per frame
    void Update(){}


}

