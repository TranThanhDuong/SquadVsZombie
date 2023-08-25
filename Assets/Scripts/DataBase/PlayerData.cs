using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class PlayerData 
{
    [SerializeField]
    public PlayerInfo playerInfo= new PlayerInfo();
    [SerializeField]
    public PlayerInventory playerInventory = new PlayerInventory();
    [SerializeField]
    public List<int> decks = new List<int>();
    [SerializeField]
    public Dictionary<string, SkillData> skills = new Dictionary<string, SkillData>();
    [SerializeField]
    public Dictionary<string, UnitData> units = new Dictionary<string, UnitData>();
    [SerializeField]
    public Dictionary<string, MissionData> missions = new Dictionary<string, MissionData>();
}
[Serializable]
public class PlayerInfo
{
    public string username;
    public int exp;
    public int level;
    public int curMission;
}
[Serializable]
public class PlayerInventory
{
    public int star;
    public int gem;
    public int energy;
}
[Serializable]
public class UnitData
{
    public int id;
    public int level;
}
[Serializable]
public class SkillData
{
    public int id;
    public int level;
}
[Serializable]
public class MissionData
{
    public int id;
    public List<int> goals;
}

public static class DataUtilities
{
    public static string ToKey(this object data)
    {
        return "K_" + data.ToString();
    }
}