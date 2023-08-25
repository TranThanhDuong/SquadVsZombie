﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
[Serializable]
public class Unit_03_IdleState : FSMState
{
    [NonSerialized]
    public Unit_03_Control parent;
    public override void OnEnter()
    {
        parent.weapon.OnEndFire();
    }
    public override void OnEnter(object data)
    {
        parent.weapon.OnEndFire();
    }
}
