using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthDisplay : MonoBehaviour
{
    public int health;
    public int maxHealth = 100;

    public Sprite fullHealth;
    public Sprite eightyhealth;
    public Sprite sixtyHealth;
    public Sprite fortyHealth;
    public Sprite twentyHealth;
    public Sprite emptyHealth;
    public Image[] healthImages;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
