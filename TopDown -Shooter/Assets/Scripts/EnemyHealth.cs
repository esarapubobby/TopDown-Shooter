using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int maxHealth = 50;
    int currentHealth;
    SpriteRenderer spriteRenderer;

    Animator animator;

    public enemymovement Enemymovement;

    bool Isdead= false;

    void Start()
    {
        currentHealth = maxHealth;
        spriteRenderer  = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
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
        Isdead = true;
        animator.SetBool("IsDead",Isdead);

        Enemymovement.enabled = false;
        
        Destroy(gameObject,2f);
    }
    IEnumerator hitflash()
    {
       spriteRenderer.color = Color.red;
       yield return new WaitForSeconds(0.2f);
       spriteRenderer.color = Color.white;
    }
}
