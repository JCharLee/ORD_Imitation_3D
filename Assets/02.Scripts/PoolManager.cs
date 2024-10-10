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

        // �������� ������ŭ ������Ʈ Ǯ ����Ʈ �Ҵ�.
        for (int i = 0; i < pools.Length; i++)
        {
            pools[i] = new List<GameObject>();
        }
    }

    public GameObject GetPool(int index, Transform trans)
    {
        // obj ����.
        GameObject obj = null;

        // Ȱ��ȭ �� ������Ʈ�� �ִ��� Ǯ���� Ž��.
        foreach (GameObject pool in pools[index])
        {
            // Ȱ��ȭ�� �� ���ִٸ�,
            if (!pool.activeSelf)
            {
                // obj�� Ǯ�� �Ҵ��ϰ� Ȱ��ȭ.
                obj = pool;
                obj.SetActive(true);
                break;
            }
        }

        // Ȱ��ȭ�� ���ִٸ� obj = null.
        if (!obj)
        {
            // obj�� �������� ���� �����Ͽ� �Ҵ�.
            obj = Instantiate(prefabs[index], trans);
            // �ش� �������� �������� Ǯ�� �߰�.
            pools[index].Add(obj);
        }

        // obj ��ȯ.
        return obj;
    }
}