using UnityEngine;

public class NewMonoBehaviourScript : MonoBehaviour
{
    public GameObject hitbox;

    // This MUST be public and have NO parameters
    public void EnableHitbox()
    {
        if (hitbox != null)
            hitbox.SetActive(true);
        else
            Debug.LogWarning("Hitbox not assigned!");
    }

    public void DisableHitbox()
    {
        if (hitbox != null)
            hitbox.SetActive(false);
    }







    public class HitboxTester : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Hitbox triggered by: " + other.name);
    }
}
}
