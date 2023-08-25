using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_01_Control : EnemyControl
{
    public Enemy_01_Databiding databiding;
    public Enemy_01_IdleState idleState;
    public Enemy_01_StartState startState;
    public Enemy_01_WalkState walkState;
    public Enemy_01_AttackState attackState;
    public Enemy_01_DeadState deadState;

    public override void Setup(EnemyCreateData data)
    {
        base.Setup(data);

        startState.parent = this;
        AddState(startState);

        idleState.parent = this;
        AddState(idleState);

        walkState.parent = this;
        AddState(walkState);


        attackState.parent = this;
        AddState(attackState);


        deadState.parent = this;
        AddState(deadState);

        GotoState(idleState, data.timeDelay);
    }
    public override void SystemUpdate()
    {
        timeAttack += Time.deltaTime;
    }
    public override void SystemFixedUpdate()
    {
        if (!isAlive)
            return;
        RaycastHit2D hit = Physics2D.Raycast(trans.position, Vector2.left, cfEnemy.range, mask);

        if(hit.collider!=null)
        {
            currenttarget = hit.collider.GetComponent<UnitControl>();

            if (currenttarget.isAlive)
            {
                if (timeAttack >= configLevel.rof)
                {
                    if (currentState != attackState)
                    {
                        GotoState(attackState);
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
