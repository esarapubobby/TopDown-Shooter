using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int maxHealth = 100;
    int currentHealth;
    SpriteRenderer spriteRenderer;

    Animator animator;

    public enemymovement Enemymovement;
    public EnemyRespawn spawner;
    UiManager uiManager;

    bool Isdead= false;
    public CircleCollider2D enemyCollider;

    void Start()
    {
        currentHealth = maxHealth;
        spriteRenderer  = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        uiManager = FindObjectOfType<UiManager>();
    }

    public void TakeDamage(int damage)
    {
        if(Isdead)
            return;
    
        currentHealth -= damage;
        Enemymovement.hasSeenPlayer = true;
        uiManager.bulletsHit++;
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
        enemyCollider.enabled = false;
        Enemymovement.enabled = false;
        uiManager.enemiesKilled++;


        spawner.killedEnemies();

        if(gameObject.CompareTag("Boss"))
            {
                FindObjectOfType<UiManager>().ChallengeVictory();
            }
                    
        
        Destroy(gameObject,2f);
    }
    IEnumerator hitflash()
    {
       spriteRenderer.color = Color.red;
       yield return new WaitForSeconds(0.2f);
       spriteRenderer.color = Color.white;
    }
}
