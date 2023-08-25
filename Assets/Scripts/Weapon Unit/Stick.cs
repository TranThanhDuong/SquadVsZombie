using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stick : WeaponUnitBehaviour
{
    public Collider2D colliderGun;
    public Transform pointDamage;
    public LayerMask mask;
    public override void Setup(object data)
    {
        base.Setup(data);
        weaponUnitHandle = new ISticktHandle();
    }
}

public class ISticktHandle : IWeaponUnitHandle
{
    private Stick stick;
    public void FireHandle(object data)
    {
        stick = (Stick)data;
        Collider2D cols = Physics2D.OverlapBox(stick.pointDamage.position,
        stick.colliderGun.bounds.size, 360, stick.mask);
        if(cols!=null)
        {
            cols.GetComponent<EnemyControl>().OnDamage(stick.weaponData.damage);
        }
 
    }
}
