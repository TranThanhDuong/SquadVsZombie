using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class ConfigUnitLevelRecord
{
    public int id;
    public int id_Unit;
    public int level;
    public int damage;
    public int hp;
    public float rof;
    public float range;
    public int gemUpgrade;
}
public class ConfigUnitLevel : BYDataTable<ConfigUnitLevelRecord>
{
    public override void SetCompareObject()
    {
        recordCompare = new ConfigUnitLevelComparison();
    }
}
public class ConfigUnitlevelKey
{
    public int id_Unit;
    public int level;
}
public class ConfigUnitLevelComparison : RecordCompare<ConfigUnitLevelRecord>
{
    public override int OnRecordCompare(ConfigUnitLevelRecord x, ConfigUnitLevelRecord y)
    {
        if (x.id_Unit > y.id_Unit)
        {
            return 1;
        }
        else if (x.id_Unit < y.id_Unit)
        {
            return -1;
        }
        else
        {
            if (x.level > y.level)
            {
                return 1;
            }
            else if (x.level < y.level)
            {
                return -1;
            }
            else
            {
                return 0;
            }
        }
    }

    public override ConfigUnitLevelRecord GetKeySearch(object key)
    {
        ConfigUnitlevelKey newKey = (ConfigUnitlevelKey)key;
        ConfigUnitLevelRecord data = new ConfigUnitLevelRecord();
        data.id_Unit = newKey.id_Unit;
        data.level = newKey.level;
        return data;
    }
}