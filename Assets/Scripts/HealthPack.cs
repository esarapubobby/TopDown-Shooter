using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPack : MonoBehaviour
{
    Audiomanager audiomanager;
    PlayerHealth playerHealth;
    void Start()
    {
        audiomanager = FindAnyObjectByType<Audiomanager>();
        playerHealth = FindAnyObjectByType<PlayerHealth>();
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            audiomanager.playHealthPickUpSound();
            playerHealth.currentHealth += playerHealth.healamount;

            playerHealth.currentHealth = Mathf.Clamp(playerHealth.currentHealth,0,playerHealth.maxHealth);

            playerHealth.TargetFillAmount = (float)playerHealth.currentHealth/playerHealth.maxHealth;

            Destroy(gameObject);
        }
    }
}
