using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class Unit_01_DeadState : FSMState
{
    [NonSerialized]
    public Unit_01_Control parent;
    private float time;
    public override void OnEnter()
    {
        parent.dataBinding.Dead = true;
        time = parent.waitDead;
    }
    public override void Update()
    {
        base.Update();
        if (time > 0)
        {
            time -= Time.deltaTime;
            if (time <= 0)
                parent.OnDead();
        }
    }
}
