using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitDataDeploy
{
    public ConfigUnitRecord cfUnit;
    public ConfigUnitLevelRecord cfUnitLevel;
    public UnitSlotDeployControl control;
}
public class UnitControl : FSMSystem 
{
    public string name;
    protected int curentHP;
    public bool isAlive;
    protected ConfigUnitRecord cfUnit;
    protected ConfigUnitLevelRecord cflevel;
    protected UnitSlotDeployControl unitSlot;
    protected Transform trans;
    public float waitDead = 2f;

    public EnemyControl currentTarget;
    public LayerMask mask;
    public float timeAttack = 0;

    [HideInInspector]
    public WeaponUnitBehaviour weapon;
    public Transform anchorGun;

    public virtual void Setup(UnitDataDeploy data)
    {
        name = data.cfUnit.name;
        isAlive = true;
        cfUnit = data.cfUnit;
        cflevel = data.cfUnitLevel;
        curentHP = cflevel.hp;
        unitSlot = data.control;
        trans = this.transform;
        MissionControl.instance.OnEnemyAttack += OnEnemyAttack;
    }
    public virtual void OnDamage(int damage)
    {

    }
    public virtual void OnDamage(int damage, System.Action<object> callback)
    {

    }
    public virtual void OnEnemyAttack(EnemyDataAttack data)
    {

    }
    public virtual void OnDead()
    {
        MissionControl.instance.OnEnemyAttack -= OnEnemyAttack;
        Destroy(this.gameObject);
        unitSlot.SetActive(false, false);
        MissionControl.instance.UnitDead();
    }
}
