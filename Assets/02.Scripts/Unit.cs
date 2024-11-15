using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Unit : MonoBehaviour
{
    public UnitData data;

    float damage;
    float atkSpeed;
    float atkTime;
    float curAtkTime;
    float rotSpeed = 10f;
    float atkRange;

    public Collider[] cols;
    int enemyLayer;
    public GameObject target;
    float targetDist;

    Animator anim;

    void Awake()
    {
        anim = GetComponent<Animator>();
    }

    void Start()
    {
        enemyLayer = 1 << LayerMask.NameToLayer("Enemy");
        Init();
    }

    void Init()
    {
        damage = data.damage;

        SetAtkSpeed(0f);
        
        if (data.unitType == UnitData.UnitType.Melee)
            atkRange = 5f;
        else
            atkRange = 10f;
    }

    void Update()
    {
        cols = Physics.OverlapSphere(transform.position, atkRange, enemyLayer);

        if (cols.Length != 0)
            target = cols[0].gameObject;
        else
            target = null;

        if (target != null)
        {
            StartCoroutine("Attack");
            Quaternion rot = Quaternion.LookRotation(target.transform.position);
            transform.rotation = Quaternion.Slerp(transform.rotation, rot, Time.deltaTime * rotSpeed);
        }
        else
        {
            StopCoroutine("Attack");
        }
    }

    public void SetAtkSpeed(float speed)
    {
        atkSpeed = data.atkSpeed + speed;
        atkTime = 1f / atkSpeed;
        curAtkTime = atkTime;

        if (atkSpeed > 1)
            anim.SetFloat("AttackSpeed", atkSpeed);
        else
            anim.SetFloat("AttackSpeed", 1f);
    }

    IEnumerator Attack()
    {
        while (true)
        {
            if (curAtkTime >= atkTime)
            {
                curAtkTime = 0f;
                anim.SetTrigger("Attack");
            }

            curAtkTime += Time.deltaTime;
            yield return null;
        }
    }

    void OnDisable()
    {
        StopCoroutine("Attack");
    }
}