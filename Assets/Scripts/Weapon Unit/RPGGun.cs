using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RPGGun : WeaponUnitBehaviour
{

    public override void Setup(object data)
    {
        base.Setup(data);

        weaponUnitHandle = new IRPGGunHandle();

        BYPool pool = new BYPool(projecties.name, clipSize, projecties);
        PoolManager.AddNewPool(pool);
        BYPool poolimpact = new BYPool(impact.name, clipSize, impact);
        PoolManager.AddNewPool(poolimpact);
    }
}

public class IRPGGunHandle : IWeaponUnitHandle
{
    private RPGGun rpgGun;
    public void FireHandle(object data)
    {
        rpgGun = (RPGGun)data;
        Transform bulletTran = rpgGun.CreateBullet().transform;
        bulletTran.SetParent(null);
        bulletTran.position = rpgGun.transform.position;
        bulletTran.right = rpgGun.transform.right;
    }
}