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
    public float drainWhileStill = 20f;
    public float damagePerSecondAtZero = 10f;

    [Header("Movement")]
    public float movementThreshold = 0.1f;

    [Header("UI (optional)")]
    [Tooltip("Image must be set to Image.Type = Filled")]
    public Image oxygenFill;

    Rigidbody2D rb;
    object playerHealth;

    float timeAtZeroOxygen = 0f;

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

        Vector2 vel = rb != null ? rb.linearVelocity : Vector2.zero;
        float speed = vel.magnitude;
        bool moving = speed > movementThreshold;

        if (moving)
        {
            oxygen += regenWhileMoving * dt;
        }
        else
        {
            // Base drain
            float drain = drainWhileStill;

            // ✅ Adjust drain based on ODeprivedCleanup
            if (MainManager.Instance != null)
            {
                if (MainManager.Instance.ODeprivedCleanup == 1)
                {
                    drain -= 5f;
                }
                else if (MainManager.Instance.ODeprivedCleanup == 2)
                {
                    drain -= 10f;
                }

                // Safety: prevent negative drain
                drain = Mathf.Max(0f, drain);
            }

            oxygen -= drain * dt;
        }

        oxygen = Mathf.Clamp(oxygen, 0f, maxOxygen);
        UpdateUI();

        if (oxygen <= 0f)
        {
            timeAtZeroOxygen += dt;
            ApplyZeroOxygenDamage(dt);
        }
        else
        {
            timeAtZeroOxygen = 0f;
        }
    }

    void ApplyZeroOxygenDamage(float dt)
    {
        float scaledDamage = damagePerSecondAtZero * (1f + timeAtZeroOxygen);
        int damage = Mathf.CeilToInt(scaledDamage * dt);

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