using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_09_Control : EnemyControl
{
    public Enemy_09_Databiding databiding;
    public Enemy_09_StartState startState;
    public Enemy_09_IdleState idleState;
    public Enemy_09_WalkState walkState;
    public Enemy_09_WifiState wifiState;
    public Enemy_09_DeadState deadState;

    public override void Setup(EnemyCreateData data)
    {
        base.Setup(data);

        startState.parent = this;
        AddState(startState);

        idleState.parent = this;
        AddState(idleState);

        walkState.parent = this;
        AddState(walkState);


        wifiState.parent = this;
        AddState(wifiState);


        deadState.parent = this;
        AddState(deadState);

    }

    public override void SystemUpdate()
    {
        timeAttack += Time.deltaTime;
        
    }
    public override void SystemFixedUpdate()

    {
        RaycastHit2D hit = Physics2D.Raycast(trans.position, Vector2.left, cfEnemy.range, mask);

        if (hit.collider != null)
        {

            currenttarget = hit.collider.GetComponent<UnitControl>();
            if (currenttarget.isAlive)
            {
                if (timeAttack >= configLevel.rof)
                {
                    if (currentState != wifiState)
                    {
                        GotoState(wifiState);
                        timeAttack = 0;
                    }
                }
            }
        }
    }
    public override void OnDamage(int damage)
    {
        if (!isAlive)
            return;

        hp -= damage;
        if (hp <= 0)
        {
            OnDropCoin();
            GotoState(deadState);
            isAlive = false;
        }
        base.OnDamage(damage);
    }
}