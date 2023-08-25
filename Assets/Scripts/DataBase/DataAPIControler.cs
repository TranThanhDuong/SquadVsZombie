using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class DataAPIControler : Singleton<DataAPIControler>
{
    [SerializeField]
    private DataBaseLocal model;
    // Start is called before the first frame update




    public void OnInit(Action callback)
    {
        if(model.LoadData())
        {
            callback?.Invoke();
        }
      else
        {
            // create nea data
            PlayerData playerData = new PlayerData();
            playerData.playerInfo.level = 1;
            playerData.playerInfo.exp = 0;
            playerData.playerInfo.username = "Hero";
            playerData.playerInfo.curMission = 1;
            playerData.playerInventory = new PlayerInventory { energy=5, gem=100, star=10};
            model.CreateNewData(playerData, null);
            List<int> deck = new List<int>();
            for(int i=1;i<2;i++)
            {
                UnitData unit = new UnitData();
                unit.id = i;
                unit.level = 1;
                model.UpdateData<UnitData>(DataPath.PLAYER_UNIT, i, unit, null);
                deck.Add(i);
            }
            model.UpdateData(DataPath.PLAYER_DECK, deck, callback);
        }
    }
    // Update is called once per frame
   public PlayerInfo GetPlayerInfo()
    {
        return model.Read<PlayerInfo>(DataPath.PLAYER_INFO);
    }
    public PlayerInventory GetPlayerInventory()
    {
        return model.Read<PlayerInventory>(DataPath.PLAYER_INVENTORY);
    }
    public void AddGem(int id, Action callback)
    {
        int gem=  model.Read<int>(DataPath.PLAYER_GEM);
        ConfigShopRecord configShopRecord = ConfigManager.instance.configShop.GetRecordByKeySearch(id);
        gem += configShopRecord.value;
        model.UpdateData(DataPath.PLAYER_GEM, gem , callback);
    }
    public void AddStar(int id, Action callback)
    {
        int star = model.Read<int>(DataPath.PLAYER_STAR);
        ConfigShopRecord configShopRecord = ConfigManager.instance.configShop.GetRecordByKeySearch(id);
        star += configShopRecord.value;
        model.UpdateData(DataPath.PLAYER_STAR, star, callback);
    }
    public List<int> GetDeckInfo ()
    {
        return model.Read<List<int>>(DataPath.PLAYER_DECK);
    }
    public Dictionary<string, UnitData> GetUnits()
    {
        return model.Read<Dictionary<string, UnitData>>(DataPath.PLAYER_UNIT);
    }

    public List<UnitData> GetDecks()
    {
        List<UnitData> data = new List<UnitData>();
        List<int> decks = new List<int>();
        Dictionary<string, UnitData> units = new Dictionary<string, UnitData>();
        decks = GetDeckInfo();
        units = GetUnits();
        foreach(var item in decks)
        {
            if(units.ContainsKey(item.ToKey()))
            {
                data.Add(units[item.ToKey()]);
            }
        }
        return data;
    }

    public void UnLockUnit(int id, Action<bool> callback)
    {
        int gem = model.Read<int>(DataPath.PLAYER_GEM);
        ConfigUnitRecord cf = ConfigManager.instance.configUnit.GetRecordByKeySearch(id);
        if (gem >= cf.buycost)
        {

            UnitData unit = new UnitData();
            unit.id = id;
            unit.level = 1;
            model.UpdateData<UnitData>(DataPath.PLAYER_UNIT, id, unit, () => {

                gem -= cf.buycost;
                model.UpdateData(DataPath.PLAYER_GEM, gem, null);

            });
            callback?.Invoke(true);
        }
        else
        {
            callback?.Invoke(false);
        }
    }
    public void UpGradeUnit(UnitData unit, Action<UnitData> callback)
    {
        int gem = model.Read<int>(DataPath.PLAYER_GEM);
        ConfigUnitlevelKey key = new ConfigUnitlevelKey();
        key.id_Unit = unit.id;
        key.level = unit.level+1;
        ConfigUnitLevelRecord cfLevel = ConfigManager.instance.configUnitLevel.GetRecordByKeySearch(key);
        if(gem>=cfLevel.gemUpgrade)
        {
           
            unit.level++;
            model.UpdateData<UnitData>(DataPath.PLAYER_UNIT, unit.id, unit, ()=> {

                gem -= cfLevel.gemUpgrade;
                model.UpdateData(DataPath.PLAYER_GEM, gem, null);

            });
            callback?.Invoke(unit);
        }
        else
        {
            callback?.Invoke(null);
        }
    }
    public void OnEquipDeck(int index, int id, Action callback )
    {
        List<int> deck = model.Read<List<int>>(DataPath.PLAYER_DECK);
        if(index<deck.Count)
        {
            deck[index] = id;
        }
        else
        {
            deck.Add(id);
        }
        model.UpdateData(DataPath.PLAYER_DECK, deck, callback);
    }

    public Dictionary<string, MissionData> GetMissionData()
    {
        return model.Read<Dictionary<string, MissionData>>(DataPath.PLAYER_MISSION);
    }

    public MissionData GetMissionDataByID(int id)
    {
        MissionData item = model.Read<MissionData>(DataPath.PLAYER_MISSION, id);
        return item;
    }
    public int GetCurrentMission()
    {
        return model.Read<int>(DataPath.PLAYER_CURRENT_MISSION);
    }
    public void ChangeCurrentMission(int id, Action callBack)
    {
        model.UpdateData(DataPath.PLAYER_CURRENT_MISSION, id, callBack);
    }
    public void ChangeMissionData(int id, List<int> goals, Action<bool> callBack)
    {
        bool ishave = true;

        MissionData mission = GetMissionDataByID(id);
        ConfigMissionRecord rd = ConfigManager.instance.configMission.GetRecordByKeySearch(id);


        if (mission == null)
        {
            mission = new MissionData();
            mission.goals = goals;
            ishave = false;
            ReciveReward(rd.lsReward_Type[0], rd.lsReward_Num[0]);
        }

        mission.id = id;


        for(int i = 0; i < goals.Count; i++)
        {
            if(ishave)
            {
                if(mission.goals[i] > rd.lsMissionNeed[i])
                    if (goals[i] <= rd.lsMissionNeed[i])
                        ReciveReward(rd.lsReward_Type[i + 1], rd.lsReward_Num[i + 1]);
            }
            else
            {
                if (goals[i] <= rd.lsMissionNeed[i])
                    ReciveReward(rd.lsReward_Type[i + 1], rd.lsReward_Num[i + 1]);
            }
            if (goals[i] <= mission.goals[i])
                mission.goals[i] = goals[i];
        }    

        model.UpdateData<MissionData>(DataPath.PLAYER_MISSION, id, mission, () =>
        {
            callBack?.Invoke(true);
        });

        int currentMiss = GetCurrentMission();
        if (id >= currentMiss)
            ChangeCurrentMission(id+1, () => { });
    }
    

    private void ReciveReward(RewardType type, int num)
    {
        switch(type)
        {
            case RewardType.Card:
                {
                    Dictionary<string, UnitData> units = new Dictionary<string, UnitData>();
                    units = GetUnits();

                    if(!units.ContainsKey(num.ToKey()))
                        UnLockUnit(num, null);
                }
                break;
            case RewardType.Gem:
                {
                    model.UpdateData(DataPath.PLAYER_GEM, num, null);
                }
                break;
            case RewardType.Gold:
                {
                    model.UpdateData(DataPath.PLAYER_STAR, num, null);
                }
                break;
            case RewardType.Exp:
                {
                    model.UpdateData(DataPath.PLAYER_EXP, num, null);
                }
                break;
            case RewardType.Energy:
                {

                }
                break;
        }
    }
        
}