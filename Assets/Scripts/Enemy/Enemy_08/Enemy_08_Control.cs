
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_08_Control : EnemyControl
{
    public Enemy_08_Databiding databiding;
    public Enemy_08_IdleState idleState;
    public Enemy_08_StartState startState;
    public Enemy_08_WalkState walkState;
    public Enemy_08_AttackState attackState;
    public Enemy_08_DeadState deadState;

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

    public override void OnDamage(int damage)
    {
        base.OnDamage(damage);
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
