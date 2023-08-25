﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class Enemy_08_IdleState : FSMState
{
    [NonSerialized]
    public Enemy_08_Control parent;
    float timeWait = 0;
    private int enternumber = 0;
    public override void OnEnter()
    {
        timeWait = 2f;
    }
    public override void OnEnter(object data)
    {
        timeWait = (float)data;
    }
    public override void FixedUpdate()
    {
        timeWait -= Time.deltaTime;
        if (timeWait <= 0)
        {
            parent.GotoState(parent.walkState);
        }
    }
}
