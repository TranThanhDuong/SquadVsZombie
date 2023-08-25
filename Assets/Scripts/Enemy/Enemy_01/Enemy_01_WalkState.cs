using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class Enemy_01_WalkState : FSMState
{
    [NonSerialized]
    public Enemy_01_Control parent;
    private float timeWait = 0;
    public override void OnEnter()
    {
        parent.databiding.SpeedMove = 1;

        timeWait = UnityEngine.Random.Range(2f, 4f);
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
            //parent.GotoState(parent.idleState, UnityEngine.Random.Range(1f, 3f));

        }

        parent.trans.Translate(Vector2.left * Time.deltaTime * parent.configLevel.speed);
    }
}
