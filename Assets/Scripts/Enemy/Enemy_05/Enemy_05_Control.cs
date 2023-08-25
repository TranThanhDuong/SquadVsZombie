using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_05_Control : EnemyControl
{
    public Enemy_05_StartState startState;
    public Enemy_05_IdleState idleState;
    public Enemy_05_WalkState walkState;
    public Enemy_05_AttackState attackState;
    public Enemy_05_DeadState deadState;
    public Enemy_05_Databinding databinding;
    public EnemyDataAttack dataAttack;

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
        AddState(deadState);

        deadState.parent = this;
        AddState(deadState);
    }

    public override void SystemUpdate()
    {
        timeAttack += Time.deltaTime;
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
    public override void SystemFixedUpdate()
    {
        RaycastHit2D hit = Physics2D.Raycast(trans.position, Vector2.left, cfEnemy.range, mask);

        if (hit.collider != null)
        {
            currenttarget = hit.collider.GetComponent<UnitControl>();
            if(currenttarget.isAlive)
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
}
