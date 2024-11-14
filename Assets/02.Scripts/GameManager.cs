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
    public int level = 1;
    public float levelTime = 41f;
    public float remainTime;
    public bool gameStart = false;

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

    void Start()
    {
        StartCoroutine(Ready());
    }

    void Update()
    {
        remainTime -= Time.deltaTime;
        int min = Mathf.FloorToInt(remainTime / 60);
        int sec = Mathf.FloorToInt(remainTime % 60);
        timeTxt.text = string.Format("{0:D2} : {1:D2}", min, sec);
        levelTxt.text = string.Format("·¹º§ {0:D2}", level);
        enemyNum.text = string.Format("{0:D2}", enemyCount);

        if (remainTime < 0.01f)
        {
            level++;
            remainTime = levelTime;
        }
    }

    IEnumerator Ready()
    {
        remainTime = 6f;
        level = 0;
        yield return new WaitForSeconds(remainTime);
        remainTime = levelTime;
        gameStart = true;
    }

    public void DrawUnit()
    {
        
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