using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 100;
    int currentHealth;
    public Image HealthFill;

    public float smoothspeed = 5f;

    float TargetFillAmount;
    void Start()
    {
        currentHealth = maxHealth;
        TargetFillAmount = 1f;
        HealthFill.fillAmount = 1f;

    }
    void Update()
    {
        HealthFill.fillAmount = Mathf.Lerp(HealthFill.fillAmount,TargetFillAmount,smoothspeed*Time.deltaTime);
    }


    public void TakeDamage(int damage)
    {
        currentHealth-=damage;

        Debug.Log(currentHealth);

        currentHealth = Mathf.Clamp(currentHealth,0,maxHealth);

        TargetFillAmount = (float)currentHealth/maxHealth;

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Debug.Log("player Died");
        GetComponent<PlayerController>().enabled = false;
        GetComponent<PlayerShoot>().enabled = false;
    }
}
