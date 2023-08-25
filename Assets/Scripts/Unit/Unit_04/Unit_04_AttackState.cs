using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class Unit_04_AttackState : FSMState
{
    [NonSerialized]
    public Unit_04_Control parent;
    public float timeWait;
    public override void OnEnter()
    {
        timeWait = 1.5f;
        parent.databiding.Attack = true;
        parent.weapon.Fire();
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();
        timeWait -= Time.deltaTime;
        if(timeWait <= 0)
        {
            parent.GotoState(parent.idleState);
        }    
    }
}