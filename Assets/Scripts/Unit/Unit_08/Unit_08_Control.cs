using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit_08_Control : UnitControl
{
    public Unit_08_IdleState idleState;
    public Unit_08_ReloadState reloadState;
    public Unit_08_DeadState deadState;
    public Unit_08_AttackState attackState;
    public Unit_08_Databiding databiding;
    public override void Setup(UnitDataDeploy data)
    {
        base.Setup(data);

        GameObject goWeapon = Instantiate(Resources.Load("Units/Weapon/TeslaGun", typeof(GameObject))) as GameObject;
        goWeapon.transform.SetParent(anchorGun);
        goWeapon.transform.localScale = Vector3.one;
        goWeapon.transform.localRotation = Quaternion.identity;
        goWeapon.transform.localPosition = Vector3.zero;

        weapon = goWeapon.GetComponent<TeslaGun>();
        weapon.Setup(new WeaponData { damage = cflevel.damage, range = cflevel.range });
        weapon.gameObject.SetActive(false);

        idleState.parent = this;
        AddState(idleState);

        attackState.parent = this;
        AddState(attackState);

        reloadState.parent = this;
        AddState(reloadState);

        deadState.parent = this;
        AddState(deadState);
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
        base.OnDamage(damage, callback);

        if (!isAlive)
            return;

        curentHP -= damage;
        if (curentHP <= 0)
        {
            GotoState(deadState);
            isAlive = false;
        }
        callback(this);
    }


    public override void SystemUpdate()
    {
        base.SystemUpdate();

        timeAttack += Time.deltaTime;
    }

    public override void SystemFixedUpdate()
    {
        if (!isAlive)
            return;

        base.SystemFixedUpdate();

        RaycastHit2D hit = Physics2D.Raycast(trans.position + Vector3.up * 0.2f, trans.right, cflevel.range, mask);

        if (hit.collider != null)
        {
            currentTarget = hit.collider.GetComponent<EnemyControl>();
            if(currentTarget.isAlive)
            {
                if(timeAttack >= cflevel.rof)
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
}
