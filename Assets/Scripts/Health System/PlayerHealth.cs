using System;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour

{
    public int health;
    public int maxHealth = 100;
    public GameOverManager gameOverManager;

    [SerializeField] private AudioClip playerTakeDamage;

    [Header("UI (optional)")]
    [Tooltip("Image must be set to Image.Type = Filled")]
    public Image healthFill;

    void Start()
    {
        health = maxHealth;
        health = Mathf.Clamp(health, 0, maxHealth);
        UpdateUI();
    }

    public void TakeDamage(int amount)
    {
        SoundFXManager.Instance.PlaySoundFXClip(playerTakeDamage, transform, 0.2f);
        health -= amount;
        health = Mathf.Clamp(health, 0, maxHealth);
        UpdateUI();

        if (health <= 0)
            gameOverManager.GameOver();
    }

    // Optional overload if other systems pass float damage
    public void TakeDamage(float amount)
    {
        int dmg = Mathf.CeilToInt(amount);
        TakeDamage(dmg);
    }

    public void Heal(int amount)
    {
        health += amount;
        health = Mathf.Clamp(health, 0, maxHealth);
        Debug.Log("Player healed. Current health: " + health);
        UpdateUI();
    }

    void Die()
    {
        // Handle player death (e.g., play animation, restart level, etc.)
        Debug.Log("Player has died.");
    }

    void UpdateUI()
    {
        if (healthFill == null) return;

        float normalized = (maxHealth > 0) ? Mathf.Clamp01(health / (float)maxHealth) : 0f;
        healthFill.fillAmount = normalized;
    }
}