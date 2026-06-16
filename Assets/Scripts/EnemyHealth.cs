using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    public int maxHealth = 100;
    int currentHealth;

    SpriteRenderer spriteRenderer;
    Animator animator;

    public enemymovement Enemymovement;
    public EnemyRespawn spawner;

    [Header("Boss Health Bar")]
    public Image HealthFill;
    public float smoothspeed = 5f;

    float TargetFillAmount;

    UiManager uiManager;

    bool Isdead = false;

    public CircleCollider2D enemyCollider;

    void Start()
    {
        currentHealth = maxHealth;

        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        uiManager = FindObjectOfType<UiManager>();


        
        if (gameObject.CompareTag("Boss") &&
            HealthFill != null)
        {
            TargetFillAmount = 1f;
            HealthFill.fillAmount = 1f;
        }
    }

    void Update()
    {
        
        if (gameObject.CompareTag("Boss") &&
            HealthFill != null)
        {
            HealthFill.fillAmount =
                Mathf.Lerp(
                    HealthFill.fillAmount,
                    TargetFillAmount,
                    smoothspeed * Time.deltaTime
                );
        }
    }

    public void TakeDamage(int damage)
    {
        if (Isdead)
            return;

        currentHealth -= damage;

        currentHealth =
            Mathf.Clamp(currentHealth, 0, maxHealth);

        
        if (gameObject.CompareTag("Boss") &&
            HealthFill != null)
        {
            UpdateHealthBar();
        }

        Enemymovement.hasSeenPlayer = true;

        uiManager.bulletsHit++;

        StartCoroutine(hitflash());

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void UpdateHealthBar()
    {
        TargetFillAmount =
            (float)currentHealth / maxHealth;
    }

    void Die()
    {
        Isdead = true;

        animator.SetBool("IsDead", true);

        enemyCollider.enabled = false;

        Enemymovement.enabled = false;

        uiManager.enemiesKilled++;

        if (spawner != null)
        {
            spawner.killedEnemies();
        }

        
        if (gameObject.CompareTag("Boss"))
        {
            uiManager.showBosswinPanel();
        }

        Destroy(gameObject, 2f);
    }

    IEnumerator hitflash()
    {
        spriteRenderer.color = Color.red;

        yield return new WaitForSeconds(0.2f);

        spriteRenderer.color = Color.white;
    }
}