using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class Enemy_09_DeadState : FSMState
{
    [NonSerialized]
    public Enemy_09_Control parent;

    public override void OnEnter()
    {
        parent.databiding.Dead = true;
        parent.OnDead();
    }
}
