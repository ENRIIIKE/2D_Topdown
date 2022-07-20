using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject[] enemyPrefabs;
    public Transform ground;
    public LayerMask layerMask;

    public float spawnDelay;
    private float spawnTime;

    private int enemyAmount;
    private int spawnedEnemies;

    private float outOfPlayerRange;

    void Start()
    {
        spawnTime = Time.time + spawnDelay;
    }

    private void Update()
    {
        if (Time.time > spawnTime)
        {
            SpawnEnemy();
        }
    }
    private void SpawnEnemy()
    {
        spawnTime = Time.time + spawnDelay;

        float groundHeight = (ground.localScale.y / 2) - 2;
        float groundWidth = (ground.localScale.x / 2) - 2;

        Vector2 randomPos = new Vector2(Random.Range(-groundWidth, groundWidth), Random.Range(-groundHeight, groundHeight));

        bool found = Physics2D.OverlapCircle(randomPos, outOfPlayerRange, layerMask);

        if (found)
        {
            spawnTime = Time.time;
            return;
        }
        int index = Random.Range(0, enemyPrefabs.Length); ;
        if (spawnedEnemies <= enemyAmount)
        {
            GameObject enemy = Instantiate(enemyPrefabs[index], randomPos, Quaternion.identity);
            enemy.name = enemyPrefabs[index].name;

            spawnedEnemies++;
        }
    }
    public void SetNumber(int amount)
    {
        enemyAmount = amount;
    }
}
