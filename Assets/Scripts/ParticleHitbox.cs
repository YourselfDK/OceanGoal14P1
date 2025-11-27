using UnityEngine;

public class ParticleHitbox : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Object entered particle hitbox: " + other.name);
    }
}
