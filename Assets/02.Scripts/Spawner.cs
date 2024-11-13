using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public Transform spawnPoint;
    public float spawnTime = 1f;
    public float timer;
    public int enemyCount = 20;
    public int level;

    void Start()
    {
        level = 0;
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (level != GameManager.instance.level)
        {
            level++;
            if (timer > spawnTime && enemyCount != 0)
            {
                timer = 0;
                enemyCount--;
                Spawn();
            }
        }
    }

    void Spawn()
    {
        GameManager.instance.pool.GetPool(0, spawnPoint);
    }
}