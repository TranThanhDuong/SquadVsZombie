using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class Unit_07_IdleState : FSMState   
{
    [NonSerialized]
    public Unit_07_Control parent;
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

    }
}
