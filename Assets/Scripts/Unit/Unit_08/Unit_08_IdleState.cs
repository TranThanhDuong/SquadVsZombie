using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class Unit_08_IdleState : FSMState
{
    [NonSerialized]
    public Unit_08_Control parent;
    float timeWait = 0;

    public override void OnEnter()
    {
        base.OnEnter();
        
        timeWait = 3f;
        
    }
    public override void OnEnter(object data)
    {
        base.OnEnter(data);
        timeWait = (float)data;
    }

    public override void FixedUpdate()
    {
        timeWait -= Time.deltaTime;
        if (timeWait <= 0)
        {
            parent.GotoState(parent.attackState);
        }
    }
}
