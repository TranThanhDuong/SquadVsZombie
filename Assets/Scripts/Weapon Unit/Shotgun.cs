using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shotgun : WeaponUnitBehaviour
{
    public float angleBullet = 5f;
    public override void Setup(object data)
    {
        base.Setup(data);
        weaponUnitHandle = new IShotgunHandle();
        BYPool pool = new BYPool(projecties.name, clipSize* numberBulletPerShot, projecties);
        PoolManager.AddNewPool(pool);
        BYPool poolImpact = new BYPool(impact.name, clipSize* numberBulletPerShot, impact);
        PoolManager.AddNewPool(poolImpact);
    }
}
public class IShotgunHandle : IWeaponUnitHandle
{
    private Shotgun shotgun;
    public void FireHandle(object data)
    {
        shotgun = (Shotgun)data;


        for (int i= -shotgun.numberBulletPerShot / 2; i <=shotgun.numberBulletPerShot / 2;i++)
        {
            Transform bullet = shotgun.CreateBullet().transform;
            bullet.SetParent(null);
            bullet.position = shotgun.transform.position;
            Vector3 dir = shotgun.transform.right;
            Quaternion q = Quaternion.Euler(0, 0, i * shotgun.angleBullet);
            bullet.right = q * dir;
            bullet.rotation = q;
        }
    }
}