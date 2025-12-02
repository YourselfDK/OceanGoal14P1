using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadShop : MonoBehaviour
{
    public int WhatIsNextLevel = 1;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {}

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            MainManager.Instance.LevelCount = WhatIsNextLevel;
            MainManager.Instance.InShop = true;
            SceneManager.LoadScene("Shop");
        }
    }

    // Update is called once per frame
    void Update(){}


}

