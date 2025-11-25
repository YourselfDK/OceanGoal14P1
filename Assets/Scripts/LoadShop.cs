using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadShop : MonoBehaviour
{

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {}

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            SceneManager.LoadScene("Shop");
        }
    }

    // Update is called once per frame
    void Update(){}


    }

