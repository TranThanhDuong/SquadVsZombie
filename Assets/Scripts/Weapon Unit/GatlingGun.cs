using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class GatlingGun : WeaponUnitBehaviour
{
    public float accuracy = 10;
    public float rof = 0.1f;
    public override void Setup(object data)
    {
        base.Setup(data);
        weaponUnitHandle = new IGatlingGun();

        BYPool pool = new BYPool(projecties.name, clipSize, projecties);
        PoolManager.AddNewPool(pool);
        BYPool poolImpact = new BYPool(impact.name, clipSize, impact);
        PoolManager.AddNewPool(poolImpact);
    }
}
public class IGatlingGun : IWeaponUnitHandle
{
    private GatlingGun gatlingGun;
    public void FireHandle(object data)
    {
        gatlingGun = (GatlingGun)data;


        float timeCount = 0;
        DOTween.To(() => timeCount, x => timeCount = x, 1, gatlingGun.rof).SetLoops(gatlingGun.clipSize).OnStepComplete(() =>
        {
            Transform bullet = gatlingGun.CreateBullet().transform;

            bullet.SetParent(null);
            bullet.position = gatlingGun.transform.position;
            Vector3 dir = gatlingGun.transform.right;
            Quaternion q = Quaternion.Euler(0, 0,UnityEngine.Random.Range(-gatlingGun.accuracy,gatlingGun.accuracy));
            bullet.right = q * dir;

        });

       
    }
}