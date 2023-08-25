using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class ConfigUnitRecord
{
    public int id;
    public string name;
    public string prefab;
    public int cost;
    public int countDown;
    public int timeDeploy;
    public int buycost;
}

public class ConfigUnit : BYDataTable<ConfigUnitRecord>
{
    public override void SetCompareObject()
    {
        recordCompare = new ConfigCompareKey<ConfigUnitRecord>("id");
    }
}
