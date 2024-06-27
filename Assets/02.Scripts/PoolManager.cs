using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    public GameObject[] prefabs;
    List<GameObject>[] pools;

    void Awake()
    {
        pools = new List<GameObject>[prefabs.Length];

        for (int i = 0; i < pools.Length; i++)
        {
            pools[i] = new List<GameObject>();
        }
    }

    public GameObject GetPool(int index, Transform trans)
    {
        GameObject obj = null;

        foreach (GameObject pool in pools[index])
        {
            if (!pool.activeSelf)
            {
                obj = pool;
                obj.SetActive(true);
                break;
            }
        }

        if (!obj)
        {
            obj = Instantiate(prefabs[index], trans);
            pools[index].Add(obj);
        }

        return obj;
    }
}