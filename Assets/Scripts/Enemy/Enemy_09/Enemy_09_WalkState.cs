using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class Enemy_09_WalkState : FSMState
{
    [NonSerialized]
    public Enemy_09_Control parent;
    private float timeWait = 0;
    public override void OnEnter()
    {
        parent.databiding.SpeedMove = 1;

        timeWait = UnityEngine.Random.Range(3f, 5f);
    }
    public override void OnExit()
    {
        parent.databiding.SpeedMove = -1;
    }

    public override void FixedUpdate()
    {
        timeWait -= Time.deltaTime;
        if (timeWait <= 0)
        {
            parent.GotoState(parent.wifiState);

        }

        parent.trans.Translate(Vector2.left * Time.deltaTime * parent.configLevel.speed);
    }
}
