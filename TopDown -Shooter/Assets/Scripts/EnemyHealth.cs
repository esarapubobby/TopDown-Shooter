using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int maxHealth = 50;
    int currentHealth;
    SpriteRenderer spriteRenderer;

    void Start()
    {
        currentHealth = maxHealth;
        spriteRenderer  = GetComponent<SpriteRenderer>();
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        StartCoroutine(hitflash());

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Destroy(gameObject);
    }
    IEnumerator hitflash()
    {
       spriteRenderer.color = Color.red;
       yield return new WaitForSeconds(0.2f);
       spriteRenderer.color = Color.white;
    }
}
