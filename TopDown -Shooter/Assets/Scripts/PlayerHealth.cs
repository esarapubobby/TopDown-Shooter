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

    public bool isdead = false;

    public Audiomanager audiomanager;

    Animator animator;

    public UiManager uiManager;

    float TargetFillAmount;
    int healamount = 25;
    void Start()
    {
        animator = GetComponent<Animator>();
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

        currentHealth = Mathf.Clamp(currentHealth,0,maxHealth);

        TargetFillAmount = (float)currentHealth/maxHealth;

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        isdead = true;
        uiManager.gameoverPanel.SetActive(true);
        uiManager.hudPanel.SetActive(false);
        uiManager.challengeHUD.SetActive(false);
    

        audiomanager.audioSource.PlayOneShot(audiomanager.DeathSound);
        audiomanager.BackGroundmusicSource.Stop();

        gameOverdetails();

        Time.timeScale = 0f;

        Debug.Log("player Died");

        animator.SetBool("IsDead",isdead);


        GetComponent<PlayerController>().enabled = false;
        GetComponent<MouseControlller>().enabled = false;
        GetComponent<PlayerShoot>().enabled = false;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("HealthPack"))
        {
            audiomanager.playHealthPickUpSound();
            currentHealth += healamount;

            currentHealth = Mathf.Clamp(currentHealth,0,maxHealth);

            TargetFillAmount = (float)currentHealth/maxHealth;

            Destroy(collision.gameObject);
        }
    }

    public void gameOverdetails()
    {
        //Enemies count
        uiManager.EnemiesKilledTxt.text = uiManager.enemiesKilled.ToString();


        //Accuracy
        float accuracy = 0f;
        if (uiManager.bulletsShot > 0)
        {
            accuracy = ((float)uiManager.bulletsHit/uiManager.bulletsShot)*100;
        }
        uiManager.AccuracyTxt.text = accuracy.ToString("F0")+"%";



        //survival Time
        int minutes =
        Mathf.FloorToInt(uiManager.survivalTime / 60);
        int seconds =
        Mathf.FloorToInt(uiManager.survivalTime % 60);

        uiManager.SurvivalTimeTxt.text =
        minutes.ToString("00") + ":" +
        seconds.ToString("00");



        //score
        int score = 
        (uiManager.enemiesKilled * 100)
        + Mathf.RoundToInt(uiManager.survivalTime)
        + Mathf.RoundToInt(accuracy / 2);
        uiManager.scoreTxt.text = score.ToString();
    }
}
