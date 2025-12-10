using System;
using System.Collections;
using Unity.Hierarchy;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour

{
    public int health;
    public int maxHealth = 100;
    public GameOverManager gameOverManager;
    float colorChangeTimer = 0.25f;
    

    [SerializeField] private AudioClip playerTakeDamage;

    [Header("UI (optional)")]
    [Tooltip("Image must be set to Image.Type = Filled")]
    public Image healthFill;

    [SerializeField] SpriteRenderer p_SpriteRenderer;
    //[SerializeField] Color p_NewColor;

    void Start()
    {
        health = maxHealth;
        health = Mathf.Clamp(health, 0, maxHealth);
        UpdateUI();
    }

    public void Update()
    {

        if (MainManager.Instance.PlayerIsRed == true)
        {
            if(colorChangeTimer > 0)
            {
                Debug.Log("Color Change worked");
                p_SpriteRenderer.color = Color.red;
                colorChangeTimer -= Time.deltaTime;
            }
            if(colorChangeTimer <= 0)
            {
                p_SpriteRenderer.color = Color.white;
                colorChangeTimer = 0.25f;
                MainManager.Instance.PlayerIsRed = false;
            }
        }
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
        //Debug.Log("Void TakeDamage Activated");
        //StartCoroutine(ChangeFishColor());
    }

    //private IEnumerator ChangeFishColor()
    //{
    //    float tick = 0f;
    //    while (p_SpriteRenderer.color != Color.red)
    //    {
    //        p_SpriteRenderer.color = Color.red;
    //        yield return null;
    //    }
    //}

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