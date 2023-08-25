using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class AssaultGun : WeaponUnitBehaviour
{

    public override void Setup(object data)
    {
        base.Setup(data);

        weaponUnitHandle = new IAssaultHandle();

        BYPool pool = new BYPool(projecties.name, clipSize, projecties);
        PoolManager.AddNewPool(pool);
        BYPool poolImpact = new BYPool(impact.name, clipSize, impact);
        PoolManager.AddNewPool(poolImpact);
    }
}

public class IAssaultHandle : IWeaponUnitHandle
{
    private AssaultGun assaultGun;
    public void FireHandle(object data)
    {
        assaultGun = (AssaultGun)data;
       

        float timeCount = 0;
        DOTween.To(() => timeCount, x => timeCount = x, 1, 0.1f).SetLoops(assaultGun.numberBulletPerShot).OnStepComplete(() =>
         {
             Transform bullet=  assaultGun.CreateBullet().transform;

             bullet.SetParent(null);
             bullet.position = assaultGun.transform.position;
             bullet.right = assaultGun.transform.right;

         });

        DOTween.To(() => timeCount, x => timeCount = x, 1, 0.1f).SetLoops(assaultGun.numberBulletPerShot).OnStepComplete(() =>
        {
            Transform bullet = assaultGun.CreateBullet().transform;

            bullet.SetParent(null);
            bullet.position = assaultGun.transform.position;
            bullet.right = assaultGun.transform.right;
        }).SetDelay(0.4f);
    }
}