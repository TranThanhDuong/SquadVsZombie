using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Analytics;
public class ConfigManager : Singleton<ConfigManager>
{
    private ConfigEnemy configEnemy_;
    public ConfigEnemy configEnemy
    {
        get
        {
            return configEnemy_;
        }
        private set
        {
            configEnemy_ = value;
        }
    }
    private ConfigEnemyLevel configEnemylevel_;
    public ConfigEnemyLevel configEnemylevel
    {
        get
        {
            return configEnemylevel_;
        }
        private set
        {
            configEnemylevel_ = value;
        }
    }
    private ConfigMission configMission_;
    public ConfigMission configMission
    {
        get
        {
            return configMission_;
        }
        private set
        {
            configMission_ = value;
        }
    }
    private ConfigWave configWave_;
    public ConfigWave configWave
    {
        get
        {
            return configWave_;
        }
        private set
        {
            configWave_ = value;
        }
    }
    private ConfigUnit configUnit_;
    public ConfigUnit configUnit
    {
        get
        {
            return configUnit_;
        }
        private set
        {
            configUnit_ = value;
        }
    }
    private ConfigUnitLevel configUnitLevel_;
    public ConfigUnitLevel configUnitLevel
    {
        get
        {
            return configUnitLevel_;
        }
        private set
        {
            configUnitLevel_ = value;
        }
    }
    private ConfigShop configShop_;
    public ConfigShop configShop
    {
        get
        {
            return configShop_;
        }
        private set
        {
            configShop_ = value;
        }
    }
    //private void Start()
    //{//Test 
    //    InitStart(null);
    //    MissionControl.instance.SetUp(1);
    //}
    // Start is called before the first frame update
    public void InitStart(Action callback)
    {
        StartCoroutine(Init(callback));
    }
    IEnumerator Init(Action callback)
    {
        configEnemy = Resources.Load("DataTable/ConfigEnemy", typeof(ScriptableObject)) as ConfigEnemy;
        yield return new WaitUntil(() => configEnemy != null);
        configEnemylevel = Resources.Load("DataTable/ConfigEnemyLevel", typeof(ScriptableObject)) as ConfigEnemyLevel;
        yield return new WaitUntil(() => configEnemylevel != null);
        configMission = Resources.Load("DataTable/ConfigMission", typeof(ScriptableObject)) as ConfigMission;
        yield return new WaitUntil(() => configMission != null);
        configWave = Resources.Load("DataTable/ConfigWave", typeof(ScriptableObject)) as ConfigWave;
        yield return new WaitUntil(() => configWave != null);
        configUnit = Resources.Load("DataTable/ConfigUnit", typeof(ScriptableObject)) as ConfigUnit;
        yield return new WaitUntil(() => configUnit != null);
        configUnitLevel = Resources.Load("DataTable/ConfigUnitLevel", typeof(ScriptableObject)) as ConfigUnitLevel;
        yield return new WaitUntil(() => configUnitLevel != null);
        configShop = Resources.Load("DataTable/ConfigShop", typeof(ScriptableObject)) as ConfigShop;
        yield return new WaitUntil(() => configShop != null);
        callback?.Invoke();
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
