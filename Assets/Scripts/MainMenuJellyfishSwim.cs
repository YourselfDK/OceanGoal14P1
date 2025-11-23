using UnityEngine;

public class MainMenuJellyfishSwim : MonoBehaviour
{
    [Header("Movement settings")]
    [SerializeField] private float minSpeed = 0.5f;
    [SerializeField] private float maxSpeed = 1.5f;
    [SerializeField] private float changeDirectionTimeMin = 1f;
    [SerializeField] private float changeDirectionTimeMax = 3f;

    [Header("Horizontal + Vertical limits")]
    [SerializeField] private float leftLimit = -8f;
    [SerializeField] private float rightLimit = 8f;
    [SerializeField] private float bottomLimit = -4f;
    [SerializeField] private float topLimit = 4f;

    private Vector2 direction;
    private float speed;
    private float changeTimer;

    private void Start()
    {
        PickNewDirection();
    }

    private void Update()
    {
        // Move
        transform.Translate(direction * speed * Time.deltaTime);

        // Countdown to new direction
        changeTimer -= Time.deltaTime;
        if (changeTimer <= 0f)
        {
            PickNewDirection();
        }

        Vector3 pos = transform.position;

        // --- Keep inside horizontal limits ---
        if (pos.x < leftLimit)
        {
            pos.x = leftLimit;
            direction.x = Mathf.Abs(direction.x); // push right
        }
        else if (pos.x > rightLimit)
        {
            pos.x = rightLimit;
            direction.x = -Mathf.Abs(direction.x); // push left
        }

        // --- Keep inside vertical limits ---
        if (pos.y < bottomLimit)
        {
            pos.y = bottomLimit;
            direction.y = Mathf.Abs(direction.y); // push up
        }
        else if (pos.y > topLimit)
        {
            pos.y = topLimit;
            direction.y = -Mathf.Abs(direction.y); // push down
        }

        transform.position = pos;
    }

    private void PickNewDirection()
    {
        // Pick a random drifting direction
        direction = new Vector2(
            Random.Range(-1f, 1f),
            Random.Range(-1f, 1f)
        ).normalized;

        speed = Random.Range(minSpeed, maxSpeed);
        changeTimer = Random.Range(changeDirectionTimeMin, changeDirectionTimeMax);
    }
}