using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Unit_05_Control : UnitControl
{
    public Unit_05_Databinding dataBinding;
    public Unit_05_AttackState attackState;
    public Unit_05_DeadState deadState;
    public Unit_05_IdleState idleState;
    public override void Setup(UnitDataDeploy data)
    {
        base.Setup(data);

        GameObject goWeapon = Instantiate(Resources.Load("Units/Weapon/GatlingGun", typeof(GameObject))) as GameObject;
        goWeapon.transform.SetParent(trans);
        goWeapon.transform.localScale = Vector3.one;
        goWeapon.transform.localRotation = Quaternion.identity;
        goWeapon.transform.localPosition = anchorGun.localPosition;

        weapon = goWeapon.GetComponent<GatlingGun>();
        weapon.Setup(new WeaponData { damage = cflevel.damage, range = cflevel.range });

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
            isAlive = false;
            GotoState(deadState);
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
            isAlive = false;
            GotoState(deadState);
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

        if (hit.collider != null)
        {
            if (timeAttack >= cflevel.rof)
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
}
