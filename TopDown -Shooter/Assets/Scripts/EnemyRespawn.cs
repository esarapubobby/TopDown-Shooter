using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyRespawn : MonoBehaviour
{
    public GameObject enemyPrefab;
    public Transform player;
    public float spawnInterval = 3f,minDistancefromplayer=10f;
    int maxEnemies = 5,killedenemies=0;
    int currentwave = 1;


    public float XspawnRange=10f,yspawnRange= 10f;

    float timer;

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= spawnInterval && maxEnemies>0)
        {
            SpawnEnemy();
            maxEnemies--;
            timer = 0f;
        }
        if (maxEnemies <= 0 && killedenemies>=currentwave*5)
        {
            currentwave++;

            maxEnemies = currentwave*5;
            killedenemies = 0;

            Debug.Log("Wave="+currentwave);
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
        while (Vector2.Distance(randomPos, player.position) < minDistancefromplayer);
        GameObject enemy = Instantiate(enemyPrefab,randomPos,Quaternion.identity);
        enemy.GetComponent<EnemyHealth>().spawner= this;
    }

    public void killedEnemies()
    {
        killedenemies++;
    }
}
