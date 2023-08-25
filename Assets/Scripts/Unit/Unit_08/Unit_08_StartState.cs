using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit_08_StartState : FSMState
{
    public Unit_08_Control parent;
    private float timeWait = 0;

    public override void OnEnter()
    {
        base.OnEnter();
        timeWait = 1f;
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();
        timeWait -= Time.deltaTime;
        if(timeWait <= 0)
        {
            parent.GotoState(parent.idleState);
        }
    }


}
