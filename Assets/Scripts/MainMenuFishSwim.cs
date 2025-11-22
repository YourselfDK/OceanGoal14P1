using UnityEngine;

public class MainMenuFishSwim : MonoBehaviour
{
    [Header("Horizontal Movement")]
    [SerializeField] private float speed = 2f;
    [SerializeField] private float leftLimit = -10f;
    [SerializeField] private float rightLimit = 10f;

    [Header("Vertical Wobble")]
    [SerializeField] private float verticalAmplitude = 0.2f;
    [SerializeField] private float verticalFrequency = 1f;

    private float startY;
    private float randomOffset;
    private bool movingRight = true;
    private SpriteRenderer sr;

    private void Start()
    {
        startY = transform.position.y;
        randomOffset = Random.Range(0f, 100f);
        sr = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        // Horizontal movement
        float direction = movingRight ? 1f : -1f;
        transform.position += new Vector3(direction * speed * Time.deltaTime, 0f, 0f);

        // Turn around at limits
        if (movingRight && transform.position.x > rightLimit)
        {
            movingRight = false;
            FlipSprite();
        }
        else if (!movingRight && transform.position.x < leftLimit)
        {
            movingRight = true;
            FlipSprite();
        }

        // Vertical wobble
        float wobble = Mathf.Sin((Time.time + randomOffset) * verticalFrequency) * verticalAmplitude;
        transform.position = new Vector3(transform.position.x, startY + wobble, transform.position.z);
    }

    private void FlipSprite()
    {
        // Flip the fish visually
        if (sr != null)
            sr.flipX = !sr.flipX;
    }
}