using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit_02_Control : UnitControl
{
    public Unit_02_Databinding dataBinding;
    public Unit_02_AttackState attackState;
    public Unit_02_IdleState idleState;
    public Unit_02_DeadState deadState;

    public override void Setup(UnitDataDeploy data)
    {
        base.Setup(data);

        GameObject goWeapon = Instantiate(Resources.Load("Units/Weapon/Stick", typeof(GameObject))) as GameObject;
        goWeapon.transform.SetParent(trans);
        goWeapon.transform.localScale = Vector3.one;
        goWeapon.transform.localRotation = Quaternion.identity;
        goWeapon.transform.localPosition = anchorGun.localPosition;

        weapon = goWeapon.GetComponent<Stick>();
        weapon.Setup(new WeaponData { damage = cflevel.damage, range = cflevel.range });

        idleState.parent = this;
        AddState(idleState);
        attackState.parent = this;
        AddState(attackState);
        deadState.parent = this;
        AddState(deadState);

        GotoState(idleState);
    }

    public override void OnDamage(int damage)
    {
        base.OnDamage(damage);
        if (!isAlive)
            return;

        curentHP -= damage;
        if (curentHP <= 0)
        {
            GotoState(deadState);
            isAlive = false;
        }
    }

    public override void OnDamage(int damage, Action<object> callback)
    {
        if (!isAlive)
            return;

        curentHP -= damage;
        if(curentHP<=0)
        {
            GotoState(deadState);
            isAlive = false;         
        }
        callback(this);
        base.OnDamage(damage, callback);
    }

    public override void OnEnemyAttack(EnemyDataAttack data)
    {
        base.OnEnemyAttack(data);
        if (!isAlive)
            return;

        curentHP -= data.damage;
        if (curentHP <= 0)
        {
            GotoState(deadState);
            isAlive = false;
        }
    }
    
    public override void SystemUpdate()
    {
        timeAttack += Time.deltaTime;
    }
    
    public override void SystemFixedUpdate()
    {
        if (!isAlive)
            return;

        RaycastHit2D hit = Physics2D.Raycast(trans.position, trans.right, cflevel.range, mask);
        if(hit.collider != null)
        {
            currentTarget = hit.collider.GetComponent<EnemyControl>();
            if(currentTarget.isAlive)
            {
                if (timeAttack >= cflevel.rof)
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

    void OnAnimEvent()
    {
        weapon.Fire();
    }
}


