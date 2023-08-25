using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


[Serializable]
public class Unit_01_AttackState : FSMState
{
    [NonSerialized]
    public Unit_01_Control parent;
    private float timeAttack;
    public override void OnEnter()
    {
        timeAttack = 1;
        parent.dataBinding.Attack = true;
        parent.weapon.Fire();
    }
    
    public override void FixedUpdate()
    {
        timeAttack -= Time.deltaTime;
        if (timeAttack <= 0)
            parent.GotoState(parent.idleState);
    }
}
