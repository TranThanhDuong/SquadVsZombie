using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public enum EnemyType
{
    MELEE_RANGE=1,
    MID_RANGE=2,
    LONG_RANGE=3
}
[Serializable]
public class ConfigEnemyRecord
{

    public int id;
    public string name;
    public string prefab;
    public float range;
    public EnemyType type;
}
public class ConfigEnemy : BYDataTable<ConfigEnemyRecord>
{
    public override void SetCompareObject()
    {
        recordCompare = new ConfigCompareKey<ConfigEnemyRecord>("id");
    }
}
