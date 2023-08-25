using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class FlameGun : WeaponUnitBehaviour
{
    public float rof = 0.1f;
    public Collider2D colliderGun;
    public Transform pointDamage;
    public LayerMask mask;
    public Animator animator;
    public override void Setup(object data)
    {
        base.Setup(data);
        weaponUnitHandle = new IFlameGun();

        //BYPool pool = new BYPool(projecties.name, clipSize, projecties);
        //PoolManager.AddNewPool(pool);
        //BYPool poolImpact = new BYPool(impact.name, clipSize, impact);
        //PoolManager.AddNewPool(poolImpact);
    }

    public void PlayAnimtion()
    {
        animator.SetTrigger("Fire");
    }
}
public class IFlameGun : IWeaponUnitHandle
{
    private FlameGun flameGun;
    public void FireHandle(object data)
    {
        flameGun = (FlameGun)data;

        flameGun.PlayAnimtion();
        Collider2D[] cols = Physics2D.OverlapBoxAll(flameGun.pointDamage.position,
        flameGun.colliderGun.bounds.size, 360, flameGun.mask);

        foreach(Collider2D e in cols)
        {
            e.GetComponent<EnemyControl>().OnDamage(flameGun.weaponData.damage);
        }
    }
}
