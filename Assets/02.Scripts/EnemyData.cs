using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Enemy", menuName = "Scriptable Object/EnemyData")]
public class EnemyData : ScriptableObject
{
    public enum EnemyType { Normal, Boss }

    public EnemyType enemyType;
    public int enemyId;
    public string enemyName;
    [TextArea]
    public string enemyDesc;

    public float hp;
    public float def;
}