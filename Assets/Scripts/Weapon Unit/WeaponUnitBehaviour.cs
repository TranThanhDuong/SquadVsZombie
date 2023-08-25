using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponData
{
    public int damage;
    public float range = 3f;
    public UnitControl unitControl = null;
}
public class WeaponUnitBehaviour : MonoBehaviour
{
    public Transform projecties;
    public Transform impact;
    public int numberBulletPerShot = 1;
    public int clipSize = 6;
    protected IWeaponUnitHandle weaponUnitHandle;
    
    public WeaponData weaponData;
    public virtual void Setup(object data)
    {
        weaponData = (WeaponData)data;
    }
    public void Fire(bool isFrie=true)
    {
        if(isFrie)
            weaponUnitHandle.FireHandle(this);
        if(!isFrie)
        {
            OnEndFire();
        }
    }
    public virtual void OnEndFire()
    {

    }
    public BulletUnit CreateBullet()
    {
        Transform bullet = PoolManager.dicPool[projecties.name].OnSpawned();//Instantiate(projecties);
        bullet.GetComponent<BulletUnit>().Setup(this);
        return bullet.GetComponent<BulletUnit>();
     
    }

}
