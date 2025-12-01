using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class NewMonoBehaviourScript : MonoBehaviour
{
    public GameObject hitbox;

    public PlayerHealth playerHealth;
    public int damage = 20;

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





    private SpriteRenderer sr;
    private Rigidbody2D rb;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
    }


    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            Debug.Log("Collided with Player");
            playerHealth.TakeDamage(damage);
        }
    }
}

