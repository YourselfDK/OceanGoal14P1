using UnityEngine;

public class FleeFromPlayer : MonoBehaviour

{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject);
    }
    public Transform player;         // Assign your Player object here
    public float fleeSpeed = 5f;     // How fast the NPC runs away
    public float fleeDistance = 5f;  // How close the player must be before NPC flees

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        float distance = Vector2.Distance(transform.position, player.position);

        // If player is close enough, run away
        if (distance < fleeDistance)
        {
            Vector2 direction = (Vector2)(transform.position - player.position);
            direction = direction.normalized;

            rb.linearVelocity = direction * fleeSpeed;
        }
        else
        {
            // Stop moving when player is far away
            rb.linearVelocity = Vector2.zero;
        }
    }
}


