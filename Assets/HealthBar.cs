using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public PlayerController player;

    public Image healthBar;
    void Start()
    {
        player = PlayerController.instance;

        healthBar = GetComponent<Image>();
        player.currentHealth = player.maxHealth;
        healthBar.fillAmount = 1; 
    }

    // Update is called once per frame
    void Update()
    {
        float healthPercentage = player.currentHealth / player.maxHealth;
        healthBar.fillAmount = healthPercentage;
    }
}
