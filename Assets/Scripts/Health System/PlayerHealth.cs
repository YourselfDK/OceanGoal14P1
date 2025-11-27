using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int health;
    public int maxHealth = 100;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        health = maxHealth;
    }

 public void TakeDamage(int amount)
    {
        health -= amount;
        if (health <= 0)
        {
            Die();
        }
    }
    public void Heal(int amount)
    {
        health += amount;

        // Clamp between 0 and maxHealth
        if (health > maxHealth)
            health = maxHealth;
        if (health < 0)
            health = 0;

        Debug.Log("Player healed. Current health: " + health);
    }
    void Die()
    {
        // Handle player death (e.g., play animation, restart level, etc.)
        Debug.Log("Player has died.");
    }




}
