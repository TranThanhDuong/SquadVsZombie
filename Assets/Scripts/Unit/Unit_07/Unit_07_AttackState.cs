using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]

public class Unit_07_AttackState : FSMState
{
    [NonSerialized]
    public Unit_07_Control parent;
    private float timeAttack;

    public override void OnEnter()
    {
        timeAttack = 2;
        parent.databiding.Attack = true;
        Debug.LogError("test2");
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
   
    }
    public void OnShoot()
    {
        parent.weapon.Fire(true);
    }
}