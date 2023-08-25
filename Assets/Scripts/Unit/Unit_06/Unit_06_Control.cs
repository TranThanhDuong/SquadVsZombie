using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Unit_06_Control : UnitControl
{
    public Unit_06_DataBiding databiding;
    public Unit_06_IdleState idleState;
    public Unit_06_AttackState attackState;
    public Unit_06_DeadState deadState;
    public Unit_06_ReloadState reloadState;
    public override void Setup(UnitDataDeploy data)
    {
        base.Setup(data);

        GameObject goWeapon = Instantiate(Resources.Load("Units/Weapon/FlameGun", typeof(GameObject))) as GameObject;
        goWeapon.transform.SetParent(trans);
        goWeapon.transform.localScale = Vector3.one;
        goWeapon.transform.localRotation = Quaternion.identity;
        goWeapon.transform.localPosition = anchorGun.localPosition;

        weapon = goWeapon.GetComponent<FlameGun>();
        weapon.Setup(new WeaponData { damage = cflevel.damage });

        idleState.parent = this;
        AddState(idleState);

        attackState.parent = this;
        AddState(attackState);

        deadState.parent = this;
        AddState(deadState);

        reloadState.parent = this;
        AddState(reloadState);
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
        if(curentHP<=0)
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


        base.SystemFixedUpdate();
        RaycastHit2D hit = Physics2D.Raycast(trans.position + Vector3.up*0.2f, trans.right, cflevel.range, mask);

        if (hit.collider != null)
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
