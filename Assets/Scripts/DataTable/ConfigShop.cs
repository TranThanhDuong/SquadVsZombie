using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public enum ItemType
{
    Star=1,
    gem=2,
    energy=3
}
[Serializable]
public class ConfigShopRecord
{
    public int id;
    public string name;
    public ItemType itemType;
    public string description;
    public float cost;
    public int value;
    public string imageIcon;
    public int discount;
}
public class ConfigShop : BYDataTable<ConfigShopRecord>
{
    public override void SetCompareObject()
    {
        recordCompare = new ConfigCompareKey<ConfigShopRecord>("id");
    }
   
}
