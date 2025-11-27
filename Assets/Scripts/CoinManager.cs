{
    using UnityEngine;
using System;
 
public class CoinManager : MonoBehaviour
{
    public static CoinManager Instance { get; private set; }

    // current coin amount
    private int coins = 0;
    public int Coins => coins; // read-only accessor

    // event fired when coins change (subscribers update UI)
    public event Action<int> OnCoinsChanged;

    // PlayerPrefs key
    private const string PREF_KEY = "PlayerCoins";

    private void Awake()
    {
        // Singleton pattern
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        // load saved coins
        coins = PlayerPrefs.GetInt(PREF_KEY, 0);
    }

    public void AddCoins(int amount)
    {
        if (amount <= 0) return;
        coins += amount;
        Save();
        OnCoinsChanged?.Invoke(coins);
    }

    // Try to spend; returns true if successful
    public bool SpendCoins(int amount)
    {
        if (amount <= 0) return true;
        if (coins >= amount)
        {
            coins -= amount;
            Save();
            OnCoinsChanged?.Invoke(coins);
            return true;
        }
        return false;
    }

    private void Save()
    {
        PlayerPrefs.SetInt(PREF_KEY, coins);
        PlayerPrefs.Save();
    }

    // Optional helper to reset during testing
    [ContextMenu("Reset Coins (Editor Only)")]
    public void ResetCoins()
    {
        coins = 0;
        Save();
        OnCoinsChanged?.Invoke(coins);
    }
}

}
