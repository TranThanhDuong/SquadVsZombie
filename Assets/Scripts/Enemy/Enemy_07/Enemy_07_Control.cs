using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_07_Control : EnemyControl
{
    public Enemy_07_Databiding databiding;
    public Enemy_07_IdleState idleState;
    public Enemy_07_StartState startState;
    public Enemy_07_WalkState walkState;
    public Enemy_07_AttackState attackState;
    public Enemy_07_DeadState deadState;
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

    public override void SystemUpdate()
    {
        timeAttack += Time.deltaTime;
    }
    public override void SystemFixedUpdate()
    {
        
        RaycastHit2D hit = Physics2D.Raycast(trans.position, Vector2.left, cfEnemy.range, mask);

        if(hit.collider!=null)
        {
            currenttarget = hit.collider.GetComponent<UnitControl>();
            if (currenttarget.isAlive)
            {
                if(timeAttack>= configLevel.rof)
                {
                    if(currentState!=attackState)
                    {
                        GotoState(attackState);
                        timeAttack = 0;
                    }
                }
            }
            
        }
    }
    public void OnAllUnit()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 3, mask);

        foreach (Collider2D e in colliders)
        {
            Vector3 dir = e.transform.position - transform.position;
            dir.Normalize();
            float dot = Vector3.Dot(transform.up, dir);

            if (dot > -0.5f || dot > 0.5f)
                continue;
            UnitControl unitControl = e.GetComponent<UnitControl>();

            if (unitControl != null)
            {
                if (unitControl.isAlive)
                {
                    unitControl.OnDamage(configLevel.damage);
                }
            }
        }
    }
}
