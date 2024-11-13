using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public enum SelectTicketType { Normal, Rare, Epic, Legendary }
    public SelectTicketType ticketType;

    [Header("# Personal Info")]
    public int drawNums;
    public Dictionary<SelectTicketType, int> ticketNums;
    public int gold;
    public int kill;
    public int enemyCount;

    [Header("# Stage Info")]
    public Transform[] unitZones;
    Dictionary<Transform, List<int>> zoneUnits;

    [Header("# Game Info")]
    public TextMeshProUGUI levelTxt;
    public TextMeshProUGUI timeTxt;
    public TextMeshProUGUI enemyNum;
    public int level;
    public float remainTime = 40f;

    public PoolManager pool;

    void Awake()
    {
        instance = this;
        ticketNums = new Dictionary<SelectTicketType, int>();
        zoneUnits = new Dictionary<Transform, List<int>>();

        foreach (SelectTicketType type in ticketNums.Keys)
        {
            ticketNums[type] = 0;
        }

        for (int i = 0; i < unitZones.Length; i++)
        {
            zoneUnits.Add(unitZones[i], new List<int>());
        }
    }

    void Update()
    {
        remainTime -= Time.deltaTime;
        int min = Mathf.FloorToInt(remainTime / 60);
        int sec = Mathf.FloorToInt(remainTime % 60);
        timeTxt.text = string.Format("{0:D2} : {1:D2}", min, sec);
    }

    public void DrawUnit()
    {
        drawNums--;

        int ran = Random.Range(0, 4);

        for (int i = 0; i < unitZones.Length; i++)
        {
            if (unitZones[i].childCount != 0)
            {
                if (ActiveCheck(unitZones[i]))
                {
                    int id = GetUnitID(GetActiveUnit(unitZones[i]));

                    if (ran == id)
                    {
                        Unit unit = GetActiveUnit(unitZones[i]).GetComponent<Unit>();
                        if (!unit.FullUnit())
                        {
                            unit.UnitActive();
                            break;
                        }
                    }
                }
            }
            else
            {
                pool.GetPool(ran, unitZones[i]);
                break;
            }
        }
    }

    bool ActiveCheck(Transform obj)
    {
        bool active = false;
        GameObject[] units = GetChildrenObjs(obj);

        for (int i = 0; i < units.Length; i++)
        {
            if (units[i].activeSelf)
            {
                active = true;
                break;
            }
        }

        return active;
    }

    GameObject GetActiveUnit(Transform obj)
    {
        GameObject unit = null;
        GameObject[] units = GetChildrenObjs(obj);

        for (int i = 0; i < units.Length; i++)
        {
            if (units[i].activeSelf)
            {
                unit = units[i];
                break;
            }
        }

        return unit;
    }

    GameObject[] GetChildrenObjs(Transform obj)
    {
        GameObject[] units = new GameObject[obj.childCount];

        for (int i = 0; i < units.Length; i++)
        {
            units[i] = obj.GetChild(i).gameObject;
        }

        return units;
    }

    int GetUnitID(GameObject unit)
    {
        int id = unit.GetComponent<Unit>().data.unitId;

        return id;
    }

    public void DrawGold()
    {
        drawNums--;
    }

    public void SelectUnit()
    {
        switch (ticketType)
        {
            case SelectTicketType.Normal:
                ticketNums[SelectTicketType.Normal]--;
                break;
            case SelectTicketType.Rare:
                ticketNums[SelectTicketType.Rare]--;
                break;
            case SelectTicketType.Epic:
                ticketNums[SelectTicketType.Epic]--;
                break;
            case SelectTicketType.Legendary:
                ticketNums[SelectTicketType.Legendary]--;
                break;
        }
    }
}