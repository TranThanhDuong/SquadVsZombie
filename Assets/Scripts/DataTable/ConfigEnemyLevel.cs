using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class ConfigEnemyLevelRecord
{
    //id,id_Enemy,level,damage,hp
    public int id;
    public int id_Enemy;
    public int level;
    public int damage;
    public int hp;
    public float rof;
    public float speed;
}
public class ConfigEnemylevelKey
{
    public int id_Enemy;
    public int level;
}
public class ConfigEnemyLevelComparison : RecordCompare<ConfigEnemyLevelRecord>
{
    public override int OnRecordCompare(ConfigEnemyLevelRecord x, ConfigEnemyLevelRecord y)
    {
        if(x.id_Enemy>y.id_Enemy)
        {
            return 1;
        }
        else if(x.id_Enemy<y.id_Enemy)
        {
            return -1;
        }
        else
        {
            if(x.level>y.level)
            {
                return 1;
            }
            else if(x.level<y.level)
            {
                return -1;
            }
             else
            {
                return 0;
            }
        }
    }
    public override ConfigEnemyLevelRecord GetKeySearch(object key)
    {
        ConfigEnemylevelKey newKey = (ConfigEnemylevelKey)key;
        ConfigEnemyLevelRecord data = new ConfigEnemyLevelRecord();
        data.id_Enemy = newKey.id_Enemy;
        data.level = newKey.level;
        return data;
    }
}
public class ConfigEnemyLevel : BYDataTable<ConfigEnemyLevelRecord>
{
    public override void SetCompareObject()
    {
        recordCompare = new ConfigEnemyLevelComparison();
    }
}
