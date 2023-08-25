using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System;

[Serializable]
public class Unit_06_AttackState : FSMState
{
    [NonSerialized]
    public Unit_06_Control parent;
    private float timeAttack;
    public override void OnEnter()
    {
        timeAttack = 1;
        parent.databiding.Attack = true;
        parent.weapon.gameObject.SetActive(true);
        parent.weapon.Fire();
    }
    
    public override void FixedUpdate()
    {
        timeAttack -= Time.deltaTime;
        if (timeAttack <= 0)
        {
            parent.GotoState(parent.idleState);
        }
    }
    public override void OnExit()
    {
        parent.databiding.Attack = false;
        parent.weapon.gameObject.SetActive(false);
    }
}
