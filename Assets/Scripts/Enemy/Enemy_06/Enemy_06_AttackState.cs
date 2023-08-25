using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class Enemy_06_AttackState : FSMState
{
    [NonSerialized]
    public Enemy_06_Control parent;
    private float timeAttack;
    public override void OnEnter()
    {
        timeAttack = 1;
        parent.databiding.Attack = true;
    }
    public override void Update()
    {
        timeAttack -= Time.deltaTime;
        if (timeAttack <= 0)
        {
            parent.GotoState(parent.idleState);
        }
    }
}

