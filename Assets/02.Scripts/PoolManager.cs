using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    public GameObject[] prefabs;
    public List<GameObject>[] pools;

    void Awake()
    {
        pools = new List<GameObject>[prefabs.Length];

        // 프리팹의 갯수만큼 오브젝트 풀 리스트 할당.
        for (int i = 0; i < pools.Length; i++)
        {
            pools[i] = new List<GameObject>();
        }
    }

    public GameObject GetPool(int index, Transform trans)
    {
        // obj 선언.
        GameObject obj = null;

        // 활성화 된 오브젝트가 있는지 풀에서 탐색.
        foreach (GameObject pool in pools[index])
        {
            // 활성화가 안 되있다면,
            if (!pool.activeSelf)
            {
                // obj를 풀로 할당하고 활성화.
                obj = pool;
                obj.SetActive(true);
                break;
            }
        }

        // 활성화가 되있다면 obj = null.
        if (!obj)
        {
            // obj에 프리팹을 새로 생성하여 할당.
            obj = Instantiate(prefabs[index], trans);
            // 해당 프리팹을 프리팹의 풀에 추가.
            pools[index].Add(obj);
        }

        // obj 반환.
        return obj;
    }
}