using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System;

[Serializable]
public class Unit_06_IdleState : FSMState
{
    [NonSerialized]
    public Unit_06_Control parent;

    public override void OnEnter()
    {
        parent.weapon.OnEndFire();
    }
    public override void OnEnter(object data)
    {
        parent.weapon.OnEndFire();
    }
    public override void FixedUpdate()
    {

    }
}
