using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System;

[Serializable]
public class Unit_06_ReloadState : FSMState
{
    [NonSerialized]
    public Unit_06_Control parent;
    float timeReload;

    public override void OnEnter()
    {
        base.OnEnter();
        timeReload = 2f;
    }
    public override void FixedUpdate()
    {
        base.FixedUpdate();
        timeReload -= Time.deltaTime;

        if(timeReload <= 0)
        {
            parent.GotoState(parent.idleState);
        }
    }

}
