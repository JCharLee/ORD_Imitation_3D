using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public Transform enemyList;
    public float spawnTime = 1f;
    public float timer;
    public int enemyCount = 20;
    public int level;

    void Start()
    {
        timer = 1f;
        level = 0;
    }

    void Update()
    {
        if (!GameManager.instance.gameStart) return;

        timer += Time.deltaTime;

        if (level != GameManager.instance.level)
        {
            level++;
            enemyCount = 20;
        }

        if (timer > spawnTime && enemyCount != 0)
        {
            timer = 0;
            enemyCount--;
            Spawn();
            GameManager.instance.enemyCount++;
        }
    }

    void Spawn()
    {
        GameObject enemy = GameManager.instance.pool.GetPool(0, enemyList);
        enemy.transform.position = transform.position;
        enemy.GetComponent<Enemy>().Init();
    }
}