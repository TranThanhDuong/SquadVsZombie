using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class Enemy_04_AttackState : FSMState
{
    [NonSerialized]
    public Enemy_04_Control parent;

    private float timeAttack;
    public override void OnEnter()
    {
        timeAttack = 1;
        parent.databiding.Attack = true;

        //MissionControl.instance.EnemyAttack(null);

        //parent.currentTarget.OnDamage(parent.configEnemy.damage);

        parent.currenttarget.OnDamage(parent.configLevel.damage, (obj) => {

            UnitControl unit = (UnitControl)obj;
           if(!unit.isAlive)
            {
                parent.currentTarget = null;
            }

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

