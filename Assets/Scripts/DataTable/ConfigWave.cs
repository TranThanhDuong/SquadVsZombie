using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class ConfigWaveRecord
{
    public int id;
    [SerializeField]
    private string idEnemys;
    public List<int> lsEnemy
    {
        get
        {
            List<int> ls = new List<int>();
            string[] sArray = idEnemys.Split(';');
            foreach (string s in sArray)
            {
                ls.Add(int.Parse(s));
            }
            return ls;
        }
    }
    [SerializeField]
    private string enemyLevels;
    public List<int> lsEnemyLevel
    {
        get
        {
            List<int> ls = new List<int>();
            string[] sArray = enemyLevels.Split(';');
            foreach (string s in sArray)
            {
                ls.Add(int.Parse(s));
            }
            return ls;
        }
    }
    [SerializeField]
    private string delayTimes;
    public List<int> lsDelayTime
    {
        get
        {
            List<int> ls = new List<int>();
            string[] sArray = delayTimes.Split(';');
            foreach (string s in sArray)
            {
                ls.Add(int.Parse(s));
            }
            return ls;
        }
    }
    [SerializeField]
    private string lines;
    public List<int> lsLine
    {
        get
        {
            List<int> ls = new List<int>();
            string[] s = lines.Split(';');
            foreach (string e in s)
            {
                ls.Add(int.Parse(e));
            }
            return ls;
        }
    }
}
public class ConfigWave : BYDataTable<ConfigWaveRecord>
{
    public override void SetCompareObject()
    {
        recordCompare = new ConfigCompareKey<ConfigWaveRecord>("id");
    }
}
