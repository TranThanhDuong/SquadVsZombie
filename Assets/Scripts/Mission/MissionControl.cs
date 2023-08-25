using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EnemyDataAttack
{
    public int damage;
    public int line;
    public bool ispriority;
}
public class MissionControl : Singleton<MissionControl>
{
    public List<GameObject> bgScene;
    public event Action<EnemyDataAttack> OnEnemyAttack;
    public event Action<int> OnCostChange;
    public event Action<int> OnGoldChange;
    public event Action<int> OnStepChange;
    public event Action<int> OnAliveChange;
    public ConfigScence configScence;
    private List<ConfigWaveRecord> waves = new List<ConfigWaveRecord>();
    private ConfigMissionRecord cfMission;
    private List<int> wavestime = new List<int>();
    private int indexWave = -1;
    [SerializeField]
    private int totalEnemyWave;
    private int totalEnemyDead;
    private int totalUnitDead;
    private int totalGoldUse;
    private int gold;
    private int time;
    private int totalPass;
    private int nextWaveSpawnTime = 0;

    private int max_money;
    public int max_Money => max_money;
    private int max_time;
    public int max_Time => max_time;
    private int mission_id;
    public int mission_ID => mission_id;

    private int max_enemy_alive = 5;
    public int Max_Enemy_Alive => max_enemy_alive;

    public int totalWave => waves.Count;
    // Start is called before the first frame update
    public void SetUp(int missionID)
    {
        Time.timeScale = 1;
        mission_id = missionID;
        cfMission = ConfigManager.instance.configMission.GetRecordByKeySearch(missionID);

        if(cfMission == null)
            return;

        for (int i = 0; i < bgScene.Count; i++)
        {
            if (cfMission.bgScene == i)
                bgScene[i].SetActive(true);
            else
                bgScene[i].SetActive(false);
        }

        wavestime = cfMission.lsWaveTime;
        max_time = cfMission.Max_Time;
        max_money = cfMission.Max_Money;
        foreach(int e in cfMission.lsWave)
        {
            ConfigWaveRecord cf = ConfigManager.instance.configWave.GetRecordByKeySearch(e);
            waves.Add(cf);
        }
        StartCoroutine(LoopTime());
        StartCoroutine(LoopGold());
        StartCoroutine(StartWave());
    }

    IEnumerator StartWave()
    {
        yield return new WaitForSeconds(1);
        CreateNewWave();
        StopCoroutine(StartWave());
    }

    void CreateNewWave()
    {
        indexWave++;
        totalEnemyDead = 0;
        if (indexWave<waves.Count)
        {
            OnStepChange?.Invoke(indexWave);
            if ((indexWave + 1) < waves.Count)
                nextWaveSpawnTime = wavestime[indexWave + 1];

            totalEnemyWave = waves[indexWave].lsEnemy.Count;
            //totalEnemyWave += waves[indexWave].lsEnemy.Count;

            ConfigWaveRecord configWave = waves[indexWave];
            for(int i=0;i<configWave.lsDelayTime.Count;i++)
            {
                EnemyCreateData enemyCreateData = new EnemyCreateData();
                enemyCreateData.enemyID = configWave.lsEnemy[i];
                enemyCreateData.enemyLevel = configWave.lsEnemyLevel[i];
                enemyCreateData.timeDelay = configWave.lsDelayTime[i];
                CreateNewEnemy(enemyCreateData, configWave.lsLine[i]);
            }
        }
        else
        {
            // victory 
            OnFinish();
        }
    }

    private void CreateNewEnemy(EnemyCreateData enemyCreateData, int pos)
    {
        // 1. tao enemy game object
        ConfigEnemyRecord cf = ConfigManager.instance.configEnemy.GetRecordByKeySearch(enemyCreateData.enemyID);
        GameObject go = Instantiate(Resources.Load("Enemies/" + cf.prefab, typeof(GameObject))) as GameObject;

        // 2. set vi tri 
        go.transform.SetParent(null);
        go.transform.position = configScence.posEnemyCreates[pos-1].position;

        // 3. setup 

        go.GetComponent<EnemyControl>().Setup(enemyCreateData);
    }
    public void EnemyDead(EnemyControl e)
    {
        totalEnemyDead++;
        if(totalEnemyDead>=totalEnemyWave)
        //totalEnemyWave--;
        //if(totalEnemyWave <= 0)
        {
            totalEnemyWave = 0;
            CreateNewWave();
            //if(indexWave >= waves.Count)
            //{
            //    OnFinish();
            //}
                
        }
    }
    public void EnemyPass()
    {
        totalPass++;
        OnAliveChange(totalPass);
        totalEnemyWave--;

        if (totalEnemyWave <= 0)
            totalEnemyWave = 0;

        if (totalPass >= max_enemy_alive)
        {
            OnLoseGame();
        }    
    }

    public void UnitDead()
    {
        totalUnitDead++;
    }

    public void EnemyAttack(EnemyDataAttack dataAttack)
    {
        OnEnemyAttack?.Invoke(dataAttack);
    }
    //private void CreateNewUnit()
    //{
       
    //    GameObject go = Instantiate(Resources.Load("Unit/" , typeof(GameObject))) as GameObject;

       
    //    UnitControl unit=go.GetComponent<UnitControl>();
    //    unit.Setup();

    //}
    IEnumerator LoopTime()
    {
        while(true)
        {
            yield return new WaitForSeconds(0.7f);
            if (time < max_time)
                TimeChange(1);
        }
    }
    IEnumerator LoopGold()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.5f);
            if (gold < max_money)
                GoldChange(1);
        }
    }
    public void GoldChange(int numberGold)
    {
        if ((gold >= max_money && numberGold > 0) || (gold + numberGold < 0))
            return;

        gold += numberGold;
        if (gold >= max_money)
            gold = max_money;
        if (gold < 0)
            gold = 0;

        OnGoldChange?.Invoke(gold);
    }
    public void TimeChange(int timeChange)
    {
        if ((time >= max_time && timeChange > 0) || (time + timeChange < 0))
            return;

        time += timeChange;

        if (time >= max_time)
            time = max_time;
        if (time < 0)
            time = 0;

        //if (time == nextWaveSpawnTime)
        //    CreateNewWave();

        //if (time >= max_time)
        //{
        //    time = max_time;
        //    StopCoroutine(LoopTime());
        //}
        OnCostChange?.Invoke(time);
    }
    public void OnFinish()
    {
        if (time < max_time)
            StopCoroutine(LoopTime());
        StopCoroutine(LoopGold());
        Time.timeScale = 0;

        CheckMission(cfMission);
        MissionData data = DataAPIControler.instance.GetMissionDataByID(mission_ID);

        DialogResultMissionParam param = new DialogResultMissionParam { cf = cfMission, data = data, isVictory = true };
        DialogManager.instance.ShowDialog(DialogIndex.DialogResultMission, param);
    }
        
    public void OnLoseGame()
    {
        if (time < max_time)
            StopCoroutine(LoopTime());
        StopCoroutine(LoopGold());
        Time.timeScale = 0;

        MissionData data = DataAPIControler.instance.GetMissionDataByID(mission_ID);

        DialogResultMissionParam param = new DialogResultMissionParam { cf = cfMission, data = data, isVictory = false };
        DialogManager.instance.ShowDialog(DialogIndex.DialogResultMission, param);
    }

    private int CheckMission(ConfigMissionRecord cf)
    {
        int total = 1;
        List<int> goals = new List<int>();

        for(int i = 0; i < cf.lsMissionType.Count; i++)
        {
            int curMiss = GetPlayerMissVal(cf.lsMissionType[i]);
            goals.Add(curMiss);

            if (curMiss < cf.lsMissionNeed[i])
                total++;

        }
        DataAPIControler.instance.ChangeMissionData(cf.id, goals, (check) =>
        {
            Debug.LogError("bugg");
        });

        return total;
    }
    private int GetPlayerMissVal(MissionType missType)
    {
        switch(missType)
        {
            case MissionType.EnemyAlive:
                return totalPass;
            case MissionType.TimeDone:
                return time;
            case MissionType.UseMoney:
                return totalGoldUse;
            case MissionType.UnitDead:
                return totalUnitDead;
        }    
        return 0;
    }
        
}
