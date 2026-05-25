using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EnemyRespawn : MonoBehaviour
{
    public GameObject enemyPrefab, healthPackprefab;

    public Transform player;
    public TextMeshProUGUI waveText;

    public Audiomanager audiomanager;

    public LayerMask obstacleLayer;

    public float spawnInterval = 3f;

    public float minDistancefromplayer = 10f;

    public float XspawnRange = 10f;

    public float yspawnRange = 10f;

    int currentWave = 1;

    int enemiesToSpawn;

    int aliveEnemies = 0;

    int spawnedEnemies = 0;

    float timer;
    bool canSpawn = false;

    void Start()
    {
        enemiesToSpawn = currentWave * 5;
    }

    void Update()
    {
        timer += Time.deltaTime;

        
        if (canSpawn && timer >= spawnInterval &&
            spawnedEnemies < enemiesToSpawn)
        {
            SpawnEnemy();

            spawnedEnemies++;

            aliveEnemies++;

            timer = 0f;
        }

        
        if (aliveEnemies <= 0 &&
            spawnedEnemies >= enemiesToSpawn)
        {
            currentWave++;

            spawnhealthPack();

            StartCoroutine(ShowWaveText());

            enemiesToSpawn = currentWave * 5;

            spawnedEnemies = 0;

            Debug.Log("Wave = " + currentWave);
        }
    }

void SpawnEnemy()
{
    Vector2 randomPos;

    bool validPosition = false;

    int attempts = 0;

    while(!validPosition && attempts < 20)
    {
        randomPos = new Vector2(
            Random.Range(-XspawnRange, XspawnRange),
            Random.Range(-yspawnRange, yspawnRange)
        );

        
        if(Vector2.Distance(
            randomPos,
            player.position)
            < minDistancefromplayer)
        {
            attempts++;

            continue;
        }

        
        Collider2D hit =
        Physics2D.OverlapCircle(
            randomPos,
            1f,
            obstacleLayer
        );

        
        if(hit == null)
        {
            GameObject enemy =
            Instantiate(
                enemyPrefab,
                randomPos,
                Quaternion.identity
            );

            enemy.GetComponent<EnemyHealth>().spawner = this;

            validPosition = true;
        }

        attempts++;
    }
}
    void spawnhealthPack()
    {
        bool validPosition = false;
        while (!validPosition)
        {
            Vector2 randomPos = new Vector2(Random.Range(-XspawnRange,XspawnRange),
                                            Random.Range(-yspawnRange,yspawnRange));

            Collider2D hit = Physics2D.OverlapCircle(randomPos ,1f,obstacleLayer);
            if(hit == null )
            {
                Instantiate(healthPackprefab,randomPos,Quaternion.identity);
                validPosition =true;
            }
            
        }
    }

    public IEnumerator ShowWaveText()
    {

        canSpawn = false;

        audiomanager.playWaveSound();

        waveText.gameObject.SetActive(true);

        waveText.text = "WAVE " + currentWave;

        yield return new WaitForSeconds(2f);

        waveText.gameObject.SetActive(false);

        canSpawn = true;
    }

    public void killedEnemies()
    {
        aliveEnemies--;
    }
}