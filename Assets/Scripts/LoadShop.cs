using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadShop : MonoBehaviour
{
    public int WhatIsNextLevel = 1;
    [SerializeField] CoinCollection CoinManager;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {}

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            MainManager.Instance.CoinCount = MainManager.Instance.CoinCount + CoinManager.Coin;
            Debug.Log(MainManager.Instance.CoinCount);
            if (CoinManager.Coin==CoinManager.Coin)
            {
                MainManager.Instance.LevelCount = WhatIsNextLevel;
                MainManager.Instance.InShop = true;
                SceneManager.LoadScene("Shop");
            }
        }
    }

    // Update is called once per frame
    void Update(){}


}

