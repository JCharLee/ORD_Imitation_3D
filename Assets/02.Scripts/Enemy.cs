using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public EnemyData data;
    public Transform[] point;

    float hp;
    float def;
    NavMeshAgent agent;

    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    void Start()
    {
        hp = data.hp;
        def = data.def;
    }

    void OnEnable()
    {
        transform.position = point[0].position;
        
    }

    void Update()
    {
        
    }
}