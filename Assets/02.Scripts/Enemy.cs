using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public float hp;
    public float maxHp;
    public float def;
    public bool isDie;

    public Transform[] point;

    int pointIdx;

    // ÄÄÆ÷³ÍÆ®
    Animator anim;
    NavMeshAgent agent;

    void Awake()
    {
        anim = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
    }

    void OnEnable()
    {
        for (int i = 0; i < point.Length; i++)
        {
            point[i] = GameObject.Find("PatrolPoint").transform.GetChild(i).transform;
        }

        isDie = false;
        anim.SetBool("IsDie", false);
        hp = maxHp;
    }

    public void Init()
    {

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
        if (isDie) return;

        if (!agent.isStopped)
        {
            if (agent.desiredVelocity != Vector3.zero)
            {
                Quaternion rot = Quaternion.LookRotation(agent.desiredVelocity);
                transform.rotation = Quaternion.Slerp(transform.rotation, rot, Time.deltaTime * 10f);
            }
        }

        if (!agent.pathPending && agent.remainingDistance < 0.5f)
            MoveNextPoint();

        if (hp <= 0)
            Dead();
    }

    void Dead()
    {
        isDie = true;
        agent.isStopped = true;
        anim.SetBool("IsDie", true);
    }
}