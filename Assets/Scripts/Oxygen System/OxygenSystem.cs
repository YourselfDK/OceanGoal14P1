using System;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Rigidbody2D), typeof(Collider2D))]
public class OxygenSystem : MonoBehaviour
{
    [Header("Oxygen")]
    public float maxOxygen = 100f;
    public float oxygen = 100f;

    [Header("Rates")]
    public float regenWhileMoving = 12f;
    public float drainWhileStill = 6f;
    public float damagePerSecondAtZero = 10f;

    [Header("Movement")]
    public float movementThreshold = 0.1f;

    [Header("UI (optional)")]
    [Tooltip("Image must be set to Image.Type = Filled")]
    public Image oxygenFill;

    Rigidbody2D rb;
    object playerHealth;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        playerHealth = GetComponent("PlayerHealth");
    }

    void Start()
    {
        oxygen = Mathf.Clamp(oxygen, 0f, maxOxygen);
        UpdateUI();
    }

    void Update()
    {
        float dt = Time.deltaTime;

        // Use the new linearVelocity API for newest Unity versions
        Vector2 vel = rb != null ? rb.linearVelocity : Vector2.zero;
        float speed = vel.magnitude;
        bool moving = speed > movementThreshold;

        if (moving)
            oxygen += regenWhileMoving * dt;
        else
            oxygen -= drainWhileStill * dt;

        oxygen = Mathf.Clamp(oxygen, 0f, maxOxygen);
        UpdateUI();

        if (oxygen <= 0f)
            ApplyZeroOxygenDamage(dt);
    }

    void ApplyZeroOxygenDamage(float dt)
    {
        int damage = Mathf.CeilToInt(damagePerSecondAtZero * dt);
        if (damage <= 0) return;

        if (playerHealth != null)
        {
            Type t = playerHealth.GetType();
            var takeDamageMethod = t.GetMethod("TakeDamage", new Type[] { typeof(int) });
            if (takeDamageMethod != null)
            {
                try
                {
                    takeDamageMethod.Invoke(playerHealth, new object[] { damage });
                }
                catch (Exception e)
                {
                    Debug.LogWarning($"Failed to invoke TakeDamage on PlayerHealth: {e.Message}");
                }
            }
            else
            {
                Debug.LogWarning("PlayerHealth.TakeDamage(int) not found on component.");
            }
        }
    }

    public void AddOxygen(float amount)
    {
        oxygen = Mathf.Clamp(oxygen + amount, 0f, maxOxygen);
        UpdateUI();
    }

    void UpdateUI()
    {
        float normalized = (maxOxygen > 0f) ? Mathf.Clamp01(oxygen / maxOxygen) : 0f;
        if (oxygenFill != null)
            oxygenFill.fillAmount = normalized;
    }
}