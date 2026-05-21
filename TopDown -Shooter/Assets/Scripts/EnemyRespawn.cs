using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EnemyRespawn : MonoBehaviour
{
    public GameObject enemyPrefab;

    public Transform player;
    public TextMeshProUGUI waveText;

    public Audiomanager audiomanager;

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

            StartCoroutine(ShowWaveText());

            enemiesToSpawn = currentWave * 5;

            spawnedEnemies = 0;

            Debug.Log("Wave = " + currentWave);
        }
    }

    void SpawnEnemy()
    {
        Vector2 randomPos;

        do
        {
            randomPos = new Vector2(
                Random.Range(-XspawnRange, XspawnRange),
                Random.Range(-yspawnRange, yspawnRange)
            );
        }
        while (
            Vector2.Distance(
                randomPos,
                player.position
            ) < minDistancefromplayer
        );

        GameObject enemy =
            Instantiate(
                enemyPrefab,
                randomPos,
                Quaternion.identity
            );

        enemy.GetComponent<EnemyHealth>().spawner = this;
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