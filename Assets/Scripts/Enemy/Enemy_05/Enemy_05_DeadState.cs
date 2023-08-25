using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class Enemy_05_DeadState : FSMState
{
    [NonSerialized]
    public Enemy_05_Control parent;

    public override void OnEnter()
    {
        parent.databinding.Dead = true;
        parent.OnDead();
    }
}
