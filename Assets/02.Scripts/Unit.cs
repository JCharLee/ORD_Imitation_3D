using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Unit : MonoBehaviour
{
    public UnitData data;

    float damage;
    float atkSpeed;
    float atkRange;

    public Collider[] cols;
    GameObject[] units;
    int enemyLayer;
    public GameObject target;
    float targetDist;

    void Awake()
    {
        damage = data.damage;
        atkSpeed = data.atkSpeed;
        atkRange = data.atkRange;

        enemyLayer = 1 << LayerMask.NameToLayer("Enemy");

        units = new GameObject[5];
        for (int i = 0; i < units.Length; i++)
        {
            units[i] = transform.GetChild(i).gameObject;
        }
    }

    void Update()
    {
        cols = Physics.OverlapSphere(transform.position, atkRange, enemyLayer);

        if (cols.Length != 0)
            target = cols[0].gameObject;
        else
            target = null;
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, data.atkRange);
    }

    public void UnitActive()
    {
        for (int i = 0; i < units.Length; i++)
        {
            if (!units[i].activeSelf)
            {
                units[i].SetActive(true);
                break;
            }
        }
    }

    public bool FullUnit()
    {
        for (int i = 0; i < units.Length; i++)
        {
            if (!units[i].activeSelf)
                return false;
        }
        return true;
    }

    void OnMouseDown()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            for (int i = units.Length - 1; i >= 0; i--)
            {
                if (units[i].activeSelf)
                {
                    if (i == 0)
                    {
                        gameObject.SetActive(false);
                        break;
                    }
                    else
                    {
                        units[i].SetActive(false);
                        break;
                    }
                }
            }
        }
    }
}