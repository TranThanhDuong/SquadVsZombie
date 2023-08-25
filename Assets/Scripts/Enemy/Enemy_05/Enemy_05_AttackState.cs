using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class Enemy_05_AttackState : FSMState
{
    [NonSerialized]
    public Enemy_05_Control parent;
    private float timeAttack;

    public override void OnEnter()
    {
        timeAttack = 1;
        parent.databinding.Attack = true;
      

       // MissionControl.instance.EnemyAttack(new EnemyDataAttack { dammage = 2 });

        parent.currenttarget.OnDamage(parent.configLevel.damage,(obj) => {


            UnitControl unit = (UnitControl)obj;
            if (!unit.isAlive)
            {
                parent.currenttarget = null;
            }
        });
    }

    public override void FixedUpdate()
    {
        timeAttack -= Time.deltaTime;

        if(timeAttack <= 0)
        {
            
            parent.GotoState(parent.idleState, parent.configLevel.rof);
            
        }
    }
}
