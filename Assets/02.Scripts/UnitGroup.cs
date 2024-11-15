using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitGroup : MonoBehaviour
{
    GameObject[] units;

    void Start()
    {
        units = new GameObject[5];
        for (int i = 0; i < units.Length; i++)
        {
            units[i] = transform.GetChild(i).gameObject;
        }
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