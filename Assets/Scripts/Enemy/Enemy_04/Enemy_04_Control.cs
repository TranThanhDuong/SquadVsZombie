using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_04_Control : EnemyControl
{
    public Enemy_04_Databiding databiding;
    public Enemy_04_IdleState idleState;
    public Enemy_04_StartState startState;
    public Enemy_04_WalkState walkState;
    public Enemy_04_AttackState attackState;
    public Enemy_04_DeadState deadState;
    public UnitControl currentTarget;

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
    }

    public override void SystemUpdate()
    {
        timeAttack += Time.deltaTime;
    }
    public override void SystemFixedUpdate()
    {
        if (!isAlive)
            return;

        if (cfEnemy == null)
            return;

        RaycastHit2D hit = Physics2D.Raycast(trans.position, Vector2.left, cfEnemy.range, mask);

        if (hit.collider != null)
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
