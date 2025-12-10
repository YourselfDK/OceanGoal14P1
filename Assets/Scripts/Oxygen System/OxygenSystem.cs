using System;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Rigidbody2D), typeof(Collider2D))]
public class OxygenSystem : MonoBehaviour
{
    [Header("Oxygen")]
    public float maxOxygen = 100f;
    [SerializeField] private float oxygen = 100f;

    [Header("Rates")]
    public float regenWhileMoving = 12f;
    public float drainWhileStill = 20f;
    public float damagePerSecondAtZero = 10f;

    [Header("Damage Ramp Settings")]
    [Tooltip("Rate at which damage multiplier increases per second at zero oxygen")]
    public float damageRampRate = 0.1f;
    [Tooltip("Maximum multiplier applied to base damage")]
    public float maxDamageMultiplier = 2f;
    [Tooltip("Interval in seconds between damage ticks at zero oxygen")]
    public float damageInterval = 2f; // NEW

    [Header("Movement")]
    public float movementThreshold = 0.1f;

    [Header("UI (optional)")]
    [Tooltip("Image must be set to Image.Type = Filled")]
    public Image oxygenFill;

    Rigidbody2D rb;
    object playerHealth;

    [Header("Runtime Debug Values")]
    [SerializeField] private float timeAtZeroOxygen = 0f;   // accumulated time at zero O2
    [SerializeField] private float currentDrain = 0f;
    [SerializeField] private int currentDamage = 0;
    [SerializeField] private float currentMultiplier = 1f;
    [SerializeField] private float lastZeroOxygenTime = 0f; // game time when oxygen hit zero
    [SerializeField] private float elapsedZeroOxygenTime = 0f; // how long since oxygen hit zero
    [SerializeField] private float lastDamageTime = 0f; // NEW: last time damage was applied

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
            currentDrain = -regenWhileMoving;
        }
        else
        {
            float drain = drainWhileStill;

            if (MainManager.Instance != null)
            {
                if (MainManager.Instance.ODeprivedCleanup == 1)
                    drain -= 5f;
                else if (MainManager.Instance.ODeprivedCleanup == 2)
                    drain -= 10f;

                drain = Mathf.Max(0f, drain);
            }

            currentDrain = drain;
            oxygen -= drain * dt;
        }

        oxygen = Mathf.Clamp(oxygen, 0f, maxOxygen);
        UpdateUI();

        if (oxygen <= 0f)
        {
            if (timeAtZeroOxygen == 0f) // first frame oxygen hits zero
                lastZeroOxygenTime = Time.time;

            timeAtZeroOxygen += dt;
            elapsedZeroOxygenTime = Time.time - lastZeroOxygenTime;

            ApplyZeroOxygenDamage();
        }
        else
        {
            timeAtZeroOxygen = 0f;
            elapsedZeroOxygenTime = 0f;
        }
    }

    void ApplyZeroOxygenDamage()
    {
        // Only apply damage once every damageInterval seconds
        if (Time.time - lastDamageTime < damageInterval)
            return;

        lastDamageTime = Time.time;

        // Ramp damage based on elapsed zero oxygen time
        float multiplier = 1f + elapsedZeroOxygenTime * damageRampRate;
        multiplier = Mathf.Min(multiplier, maxDamageMultiplier);
        currentMultiplier = multiplier;

        // Scale damage to represent per-second rate applied once every interval
        float scaledDamage = damagePerSecondAtZero * multiplier * damageInterval;
        int damage = Mathf.CeilToInt(scaledDamage);
        currentDamage = damage;

        Debug.Log($"[OxygenSystem] Damage Tick: {damage}, Multiplier: {multiplier:F2}, ElapsedZeroO2: {elapsedZeroOxygenTime:F2}s");

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