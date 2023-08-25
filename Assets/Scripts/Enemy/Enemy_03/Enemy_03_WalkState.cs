using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
[Serializable]

public class Enemy_03_WalkState : FSMState
{
    [NonSerialized]
    public Enemy_03_Control parent;
    private float timeWait = 0;
    public override void OnEnter()
    {
        parent.dataBiding.SpeedMove = 1;
        timeWait = UnityEngine.Random.Range(2F, 4F);
    }
    public override void OnExit()
    {
        parent.dataBiding.SpeedMove = -1;
    }
    public override void FixedUpdate()
    {
        timeWait -= Time.deltaTime;
        if(timeWait <= 0)
        {

        }
        parent.trans.Translate(Vector2.left * Time.deltaTime * parent.configLevel.speed);
    }

}
