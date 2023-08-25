using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class Enemy_08_StartState : FSMState
{

    [NonSerialized]
    public Enemy_08_Control parent;
    private float timeDelay = 0;
    public override void OnEnter()
    {
        timeDelay = parent.timeDelay;
        base.OnEnter();
    }
    public override void OnEnter(object data)
    {
        base.OnEnter(data);
    }
    
    public override void FixedUpdate()
    {
        timeDelay -= Time.deltaTime;

        if (timeDelay <= 0)
        {
            parent.GotoState(parent.idleState);
        }
    }
}
