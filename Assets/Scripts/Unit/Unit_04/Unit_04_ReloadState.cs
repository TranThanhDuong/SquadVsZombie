using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class Unit_04_ReloadState : FSMState
{
    [NonSerialized]
    public Unit_04_Control parent;

    public override void OnEnter()
    {
        base.OnEnter();
    }
    public override void OnEnter(object data)
    {
        base.OnEnter(data);
    }

}