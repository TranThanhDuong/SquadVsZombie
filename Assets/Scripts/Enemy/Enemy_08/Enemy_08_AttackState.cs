using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class Enemy_08_AttackState : FSMState
{
    [NonSerialized]
    public Enemy_08_Control parent;
    private float timeAttack;
    public override void OnEnter()
    {
        timeAttack = 1;
        parent.databiding.Attack = true;
        //parent.currenttarget.OnDamage(parent.damage, (obj) =>
        // {

        // });
        MissionControl.instance.EnemyAttack(new EnemyDataAttack { damage = 2 });

        parent.currenttarget.OnDamage(parent.configLevel.damage);
        parent.currenttarget.OnDamage(parent.configLevel.damage, (obj) => {

            UnitControl unit = (UnitControl)obj;
            Debug.LogError(unit.name);

        });
    }
    public override void Update()
    {
        timeAttack -= Time.deltaTime;
        if (timeAttack <= 0)
        {
            parent.GotoState(parent.idleState, parent.configLevel.rof);
        }
    }

}