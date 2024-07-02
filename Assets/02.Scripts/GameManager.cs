using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

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
    public UnitData[] unitDatas;

    public PoolManager pool;

    void Awake()
    {
        instance = this;
        ticketNums = new Dictionary<SelectTicketType, int>();

        foreach (SelectTicketType type in ticketNums.Keys)
        {
            ticketNums[type] = 0;
        }
    }

    public void DrawUnit()
    {
        for (int i = 0; i < unitZones.Length; i++)
        {
            int ran = Random.Range(0, 4);

            GameObject unit = null;
            for (int j = 0; j < unitZones[i].childCount; j++)
            {
                GameObject obj = unitZones[i].GetChild(j).gameObject;
                if (obj.activeSelf)
                {
                    unit = obj;
                    break;
                }
            }

            if (!unit)
            {
                pool.GetPool(ran, unitZones[i]);
                break;
            }
        }
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