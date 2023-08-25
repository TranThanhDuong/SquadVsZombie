using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


[Serializable]
public class Enemy_05_StartState : FSMState
{
    [NonSerialized]
    public Enemy_05_Control parent;
    private float timeDelay = 0;

    public override void OnEnter()
    {
        base.OnEnter();
        timeDelay = parent.timeDelay;
    }

    public override void FixedUpdate()
    {
        timeDelay -= Time.deltaTime;

        if(timeDelay <= 0)
        {
            parent.GotoState(parent.idleState);
        }

    }
}
