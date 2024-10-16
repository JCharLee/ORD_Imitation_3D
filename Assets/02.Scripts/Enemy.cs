using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public EnemyData data;
    public Transform[] point;

    int pointIdx;
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
        pointIdx = 0;
        transform.position = point[pointIdx].position;
    }

    void MoveNextPoint()
    {
        if (agent.isPathStale) return;

        if (pointIdx == 3)
            pointIdx = 0;
        else
            pointIdx++;

        agent.destination = point[pointIdx].position;
        agent.isStopped = false;
    }

    void Update()
    {
        if (!agent.isStopped)
        {
            Quaternion rot = Quaternion.LookRotation(agent.desiredVelocity);
            transform.rotation = Quaternion.Slerp(transform.rotation, rot, Time.deltaTime * 10f);
        }

        if (!agent.pathPending && agent.remainingDistance < 0.5f)
            MoveNextPoint();
    }
}