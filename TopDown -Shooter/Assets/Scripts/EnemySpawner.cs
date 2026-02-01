using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRespawn : MonoBehaviour
{
     public GameObject enemyPrefab;
    public float spawnInterval = 3f;
    public int maxEnemies = 5;

    float timer;
    int currentEnemies;

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= spawnInterval && currentEnemies < maxEnemies)
        {
            SpawnEnemy();
            timer = 0f;
        }
    }

    void SpawnEnemy()
    {
        Instantiate(enemyPrefab, transform.position, Quaternion.identity);
        currentEnemies++;
    }
}
