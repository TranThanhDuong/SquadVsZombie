using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class Enemy_04_DeadState : FSMState
{
    [NonSerialized]
    public Enemy_04_Control parent;
    public float time;
    public override void OnEnter()
    {
        parent.databiding.Dead = true;
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

