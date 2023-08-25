using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class Enemy_08_DeadState : FSMState
{
    [NonSerialized]
    public Enemy_08_Control parent;

    public override void OnEnter()
    {
        parent.databiding.Dead = true;
        parent.OnDead();
    }
}

