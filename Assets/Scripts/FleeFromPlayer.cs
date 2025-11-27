using UnityEngine;

public class FleeFromPlayer : MonoBehaviour

{
    [Header("Flee Settings")]
    public Transform player;         // Assign your Player object here
    public float fleeSpeed = 5f;     // How fast the fish swims away
    public float fleeDistance = 5f;  // How close the player must be before fish flees

    [Tooltip("How long (in seconds) the fish keeps fleeing after being triggered")]
    public float fleeDuration = 2f;  // <-- how long to keep fleeing
    private float fleeTimer = 0f;    // <-- counts down

    [Header("Idle Swim - Horizontal Movement")]
    [SerializeField] private float speed = 2f;
    [SerializeField] private float leftLimit = -10f;
    [SerializeField] private float rightLimit = 10f;

    [Header("Idle Swim - Vertical Wobble")]
    [SerializeField] private float verticalAmplitude = 0.2f;
    [SerializeField] private float verticalFrequency = 1f;

    [Header("Player Interaction")]
    [SerializeField] private int healthAmount = 10;  // Set per fish in Inspector

    private float startY;
    private float randomOffset;
    private bool movingRight = true;

    private SpriteRenderer sr;
    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();

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

        // If player is close enough, (re)start the flee timer
        if (distance < fleeDistance)
        {
            fleeTimer = fleeDuration;
        }

        // Count down the flee timer
        if (fleeTimer > 0f)
        {
            fleeTimer -= Time.deltaTime;
            FleePlayer();
        }
        else
        {
            IdleSwim();
        }
    }

    private void FleePlayer()
    {
        // Direction away from player
        Vector2 direction = (Vector2)(transform.position - player.position);
        direction = direction.normalized;

        // Move via Rigidbody
        rb.linearVelocity = direction * fleeSpeed;

        // Flip sprite depending on direction.x
        if (direction.x > 0f && !movingRight)
        {
            movingRight = true;
            FlipSprite();
        }
        else if (direction.x < 0f && movingRight)
        {
            movingRight = false;
            FlipSprite();
        }
    }

    private void IdleSwim()
    {
        // When idle, stop physics movement
        rb.linearVelocity = Vector2.zero;

        // Horizontal movement
        float dir = movingRight ? 1f : -1f;
        Vector3 pos = transform.position;
        pos.x += dir * speed * Time.deltaTime;

        // Turn around at limits
        if (movingRight && pos.x > rightLimit)
        {
            movingRight = false;
            FlipSprite();
        }
        else if (!movingRight && pos.x < leftLimit)
        {
            movingRight = true;
            FlipSprite();
        }

        // Vertical wobble
        float wobble = Mathf.Sin((Time.time + randomOffset) * verticalFrequency) * verticalAmplitude;
        pos.y = startY + wobble;

        transform.position = pos;
    }

    private void FlipSprite()
    {
        if (sr != null)
            sr.flipX = !sr.flipX;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            PlayerHealth playerHealth = collision.collider.GetComponent<PlayerHealth>();

            if (playerHealth != null)
            {
                playerHealth.Heal(healthAmount);
            }

            Destroy(gameObject); // Fish is eaten
        }
    }

}