using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Unit", menuName = "Scriptable Object/UnitData")]
public class UnitData : ScriptableObject
{
    public enum UnitClass { Normal, Rare, Epic, Legendary }

    public UnitClass unitClass;
    public int unitId;
    public string unitName;
    [TextArea]
    public string unitDesc;

    public float damage;
    public float atkSpeed;
    public float atkRange;
}