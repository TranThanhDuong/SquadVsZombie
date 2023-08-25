using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class Enemy_09_WifiState : FSMState
{
    [NonSerialized]
    public Enemy_09_Control parent;
    public float timeAttack;
    public LayerMask mask;
    public UnitControl currentTargetWifi;
    public override void OnEnter()
    {
        parent.databiding.Wifi = true;
        timeAttack = 1;
        base.OnEnter();

        //MissionControl.instance.EnemyStun(new EnemyDataStun { timeStun = 2 });
    }
    public override void Update()
    {
        Attack_01();
        timeAttack -= Time.deltaTime;
        if (timeAttack <= 0)
        {
            parent.GotoState(parent.idleState);
        }
        

    }
    private void Attack_01()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(parent.transform.position, 100, mask);

        foreach (Collider2D e in colliders)
        {
            currentTargetWifi = e.GetComponent<UnitControl>();

            if (currentTargetWifi != null)
            {

                currentTargetWifi.OnDamage(parent.configLevel.damage, (obj) => {

                    UnitControl unit = (UnitControl)obj;
                    if (!unit.isAlive)
                    {
                        parent.currenttarget = null;
                    }
                });

            }
        }
    }

}
