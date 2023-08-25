using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Unit_03_Control : UnitControl
{
    public Unit_03_Binding dataBinding;
    public Unit_03_AttackState attackState;
    public Unit_03_DeadState deadState;
    public Unit_03_IdleState idleState;
    public override void Setup(UnitDataDeploy data)
    {
        base.Setup(data);

        GameObject goWeapon = Instantiate(Resources.Load("Units/Weapon/SniperGun", typeof(GameObject))) as GameObject;
        goWeapon.transform.SetParent(trans);
        goWeapon.transform.localScale = Vector3.one;
        goWeapon.transform.localRotation = Quaternion.identity;
        goWeapon.transform.localPosition = anchorGun.localPosition;

        weapon = goWeapon.GetComponent<SniperGun>();
        weapon.Setup(new WeaponData { damage = cflevel.damage });

        idleState.parent = this;
        AddState(idleState);

        attackState.parent = this;
        AddState(attackState);

        deadState.parent = this;
        AddState(deadState);
    }
    public override void OnDamage(int damage)
    {
        if (!isAlive)
            return;

        curentHP -= damage;
        if (curentHP <= 0)
        {
            GotoState(deadState);
            isAlive = false;
        }
        base.OnDamage(damage);
    }
    public override void OnDamage(int damage, Action<object> callback)
    {
        if (!isAlive)
            return;

        curentHP -= damage;
        if (curentHP <= 0)
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
            if (currentTarget.isAlive)
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
}
