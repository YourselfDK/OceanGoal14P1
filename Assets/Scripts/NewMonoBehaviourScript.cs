using UnityEngine;

public class HitboxTester : MonoBehaviour
{
  
    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Object entered particle hitbox: " + other.name);
    }
    
}

