using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class ConfigMissionRecord
{
    public int id;
    [SerializeField]
    private string waves;
    public List<int> lsWave
    {
        get
        {
            List<int> ls = new List<int>();
            string[] s = waves.Split(';');
            foreach(string e in s)
            {
                ls.Add(int.Parse(e));
            }
            return ls;
        }
    }
    [SerializeField]
    private string wavestime;
    public List<int> lsWaveTime
    {
        get
        {
            List<int> ls = new List<int>();
            string[] s = wavestime.Split(';');
            foreach (string e in s)
            {
                ls.Add(int.Parse(e));
            }
            return ls;
        }
    }
    [SerializeField]
    private string reward_type;
    public List<RewardType> lsReward_Type
    {
        get
        {
            List<RewardType> ls = new List<RewardType>();
            string[] s = reward_type.Split(';');
            foreach (string e in s)
            {
                ls.Add((RewardType)int.Parse(e));
            }
            return ls;
        }
    }

    [SerializeField]
    private string reward_num;
    public List<int> lsReward_Num
    {
        get
        {
            List<int> ls = new List<int>();
            string[] s = reward_num.Split(';');
            foreach (string e in s)
            {
                ls.Add(int.Parse(e));
            }
            return ls;
        }
    }

    [SerializeField]
    private string mission_type;
    public List<MissionType> lsMissionType
    {
        get
        {
            List<MissionType> ls = new List<MissionType>();
            string[] s = mission_type.Split(';');
            foreach (string e in s)
            {
                ls.Add((MissionType)int.Parse(e));
            }
            return ls;
        }
    }

    [SerializeField]
    private string mission_need;
    public List<int> lsMissionNeed
    {
        get
        {
            List<int> ls = new List<int>();
            string[] s = mission_need.Split(';');
            foreach (string e in s)
            {
                ls.Add(int.Parse(e));
            }
            return ls;
        }
    }
    [SerializeField]
    private int max_money;
    public int Max_Money => max_money;
    [SerializeField]
    private int max_time;
    public int Max_Time => max_time;
    [SerializeField]
    private int bgscene;
    public int bgScene => bgscene;
}
public class ConfigMission : BYDataTable<ConfigMissionRecord>
{
    public override void SetCompareObject()
    {
        recordCompare = new ConfigCompareKey<ConfigMissionRecord>("id");
    }

    public List<string> GetMissionTypeName(List<MissionType> types)
    {
        List<string> ls = new List<string>();
        for(int i = 0; i < types.Count; i++)
        {
            switch (types[i])
            {
                case MissionType.EnemyAlive:
                    {
                        ls.Add("Enemy Alive:");
                        break;
                    }
                case MissionType.TimeDone:
                    {
                        ls.Add("Time Done:");
                        break;
                    }
                case MissionType.UseMoney:
                    {
                        ls.Add("Used Gold:");
                        break;
                    }
                default:
                    {
                        ls.Add("");
                        break;
                    }
            }
        }
        return ls;
    }

    public List<string> GetMissionReward(List<RewardType> types, List<int> nums)
    {
        List<string> ls = new List<string>();
        for (int i = 0; i < types.Count; i++)
        {
            switch (types[i])
            {
                case RewardType.Gold:
                    {
                        ls.Add(nums[i] + " Gold");
                        break;
                    }
                case RewardType.Exp:
                    {
                        ls.Add(nums[i] + " Exp");
                        break;
                    }
                case RewardType.Energy:
                    {
                        ls.Add(nums[i] + " Energy");
                        break;
                    }
                case RewardType.Gem:
                    {
                        ls.Add(nums[i] + " Gem");
                        break;
                    }
                case RewardType.Card:
                    {
                        ls.Add("1 " + ConfigManager.instance.configUnit.GetRecordByKeySearch(nums[i]).name);
                        break;
                    }
                default:
                    {
                        ls.Add("");
                        break;
                    }
            }
        }
        return ls;
    }
}
