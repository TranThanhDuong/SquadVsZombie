using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class Enemy_05_WalkState : FSMState
{
    [NonSerialized]
    public Enemy_05_Control parent;
    private float timeWait = 0;

    public override void OnEnter()
    {
        parent.databinding.SpeedMove = 1;
        timeWait = UnityEngine.Random.Range(2f, 5f);
    }

    public override void OnExit()
    {
        parent.databinding.SpeedMove =  -1;
    }

    public override void FixedUpdate()
    {
        timeWait -= Time.deltaTime;

        if(timeWait <= 0)
        {
            parent.GotoState(parent.idleState, UnityEngine.Random.Range(1f, 3f));
        }

        parent.trans.Translate(Vector2.left * Time.deltaTime *parent.configLevel.speed);
    }
}
