using UnityEngine;

// Which way should the fish start swimming?
public enum StartDirection
{
    Left,
    Right,
    Random
}

// Which way is the sprite art facing when flipX = FALSE?
public enum SpriteBaseFacing
{
    Right,
    Left
}

public class FleeFromPlayer : MonoBehaviour
{
    [Header("Flee Settings")]
    public Transform player;
    public float fleeSpeed = 5f;
    public float fleeDistance = 5f;

    [Tooltip("How long (in seconds) the fish keeps fleeing after being triggered")]
    public float fleeDuration = 2f;
    private float fleeTimer = 0f;

    [Header("Idle Swim - Horizontal Movement")]
    [SerializeField] private float speed = 2f;
    [SerializeField] private float leftLimit = -10f;
    [SerializeField] private float rightLimit = 10f;

    [Header("Idle Swim - Vertical Wobble")]
    [SerializeField] private float verticalAmplitude = 0.2f;
    [SerializeField] private float verticalFrequency = 1f;

    [Header("Player Interaction")]
    [SerializeField] private int healthAmount = 10;

    [Header("Starting Direction")]
    [SerializeField] private StartDirection startDirection = StartDirection.Right;

    [Header("Sprite Setup")]
    [Tooltip("Which way does the sprite face when flipX is FALSE?")]
    [SerializeField] private SpriteBaseFacing spriteBaseFacing = SpriteBaseFacing.Left; // DEFAULT = Left

    [Header("Eating Sound")]
    [SerializeField] private AudioClip eatingSound;

    private float startY;
    private float randomOffset;
    private bool movingRight = true;

    private SpriteRenderer sr;
    private Rigidbody2D rb;

    private bool wasFleeing = false;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();

        // Pick initial direction
        switch (startDirection)
        {
            case StartDirection.Left:
                movingRight = false;
                break;

            case StartDirection.Right:
                movingRight = true;
                break;

            case StartDirection.Random:
                movingRight = Random.value > 0.5f;
                break;
        }

        ApplySpriteFacing();

        startY = transform.position.y;
        randomOffset = Random.Range(0f, 100f);
    }

    private void Update()
    {
        if (player == null)
        {
            IdleSwim();
            return;
        }

        float distance = Vector2.Distance(transform.position, player.position);

        // Trigger flee
        if (distance < fleeDistance)
        {
            fleeTimer = fleeDuration;
        }

        bool isFleeing = fleeTimer > 0f;

        if (isFleeing)
        {
            fleeTimer -= Time.deltaTime;
            FleePlayer();
        }
        else
        {
            if (wasFleeing)
            {
                // Prevent snapping back vertically
                float wobble = Mathf.Sin((Time.time + randomOffset) * verticalFrequency) * verticalAmplitude;
                startY = transform.position.y - wobble;
            }

            IdleSwim();
        }

        wasFleeing = isFleeing;
    }

    private void FleePlayer()
    {
        Vector2 direction = (Vector2)(transform.position - player.position);
        direction = direction.normalized;

        // Horizontal fleeing only
        direction.y = 0f;

        rb.linearVelocity = direction * fleeSpeed;

        // Flip sprite
        if (direction.x > 0f && !movingRight)
        {
            movingRight = true;
            ApplySpriteFacing();
        }
        else if (direction.x < 0f && movingRight)
        {
            movingRight = false;
            ApplySpriteFacing();
        }
    }

    private void IdleSwim()
    {
        rb.linearVelocity = Vector2.zero;

        float dir = movingRight ? 1f : -1f;
        Vector3 pos = transform.position;
        pos.x += dir * speed * Time.deltaTime;

        // Left/right bounds
        if (movingRight && pos.x > rightLimit)
        {
            movingRight = false;
            ApplySpriteFacing();
        }
        else if (!movingRight && pos.x < leftLimit)
        {
            movingRight = true;
            ApplySpriteFacing();
        }

        // Wobble
        float wobble = Mathf.Sin((Time.time + randomOffset) * verticalFrequency) * verticalAmplitude;
        pos.y = startY + wobble;

        transform.position = pos;
    }

    /// <summary>
    /// Makes sprite visually face movement direction.
    /// </summary>
    private void ApplySpriteFacing()
    {
        if (sr == null) return;

        bool wantToFaceRight = movingRight;

        if (spriteBaseFacing == SpriteBaseFacing.Right)
        {
            sr.flipX = !wantToFaceRight;
        }
        else // spriteBaseFacing == Left
        {
            sr.flipX = wantToFaceRight;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            PlayerHealth playerHealth = collision.collider.GetComponent<PlayerHealth>();

            if (playerHealth != null)
            {
                playerHealth.Heal(healthAmount);
                if (healthAmount < 0)
                {
                    MainManager.Instance.PlayerIsRed = true;
                }
            }

            SoundFXManager.Instance.PlaySoundFXClip(eatingSound, collision.transform, 0.2f);
            Destroy(gameObject);
        }
    }
}