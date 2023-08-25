using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCreateData
{
    public int enemyID;
    public int enemyLevel;
    public float timeDelay;
}
public class EnemyControl : FSMSystem
{
    public int hp;
    public int damage;
    public ConfigEnemyLevelRecord configLevel;
    public UnitControl currenttarget;
    public ConfigEnemyRecord cfEnemy;
    public float timeDelay;
    public Transform trans;
    public float waitDead = 2f;
    public bool isAlive;
    public LayerMask mask;
    public float timeAttack = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }
    public virtual void Setup(EnemyCreateData data)
    {
        cfEnemy = ConfigManager.instance.configEnemy.GetRecordByKeySearch(data.enemyID);
        ConfigEnemylevelKey key = new ConfigEnemylevelKey { id_Enemy = data.enemyID, level = data.enemyLevel };
        configLevel = ConfigManager.instance.configEnemylevel.GetRecordByKeySearch(key);
        damage = configLevel.damage;
        hp = configLevel.hp;
        timeDelay = data.timeDelay;
        trans = transform;
        isAlive = true;
    }
    public virtual void OnDamage(int damage)
    {

    }
   
    public void OnDropCoin()
    {
        GameObject obj = Instantiate(Resources.Load("icon/coin_drop", typeof(GameObject)) as GameObject);
        obj.GetComponent<CoinsControl>()?.SetUp(this.transform.position, 10);
    }

    public void OnDead()
    {
       
        MissionControl.instance.EnemyDead(this);
        Destroy(this.gameObject);
    }
    public void OnPass()
    {
        MissionControl.instance.EnemyPass();
        Destroy(this.gameObject);
    }
}
