using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssaultBullet : BulletUnit
{
    public float lifeTime = 0.2f;
    private bool isActive = false;
    public LayerMask mask;
    private WeaponUnitBehaviour weapon;
    IEnumerator WaitDestroy()
    {
        yield return new WaitForSeconds(lifeTime);
       PoolManager.dicPool[weapon.projecties.name].OnDespawned(transform);
      
    }
    public override void Setup(WeaponUnitBehaviour weapon)
    {
        this.weapon = weapon;
        //gameObject.GetComponent<Rigidbody2D>().velocity = transform.right * 5f;
        base.Setup(weapon);
    }
    private void Update()
    {
        gameObject.transform.Translate (transform.right * Time.deltaTime * 7f);
        RaycastHit2D hitinfo= Physics2D.Raycast(transform.position, transform.right, 0.5f, mask);
        if(hitinfo.collider!=null)
        {
            Transform impactObject = PoolManager.dicPool[weapon.impact.name].OnSpawned();
            impactObject.SetParent(null);
            impactObject.position = hitinfo.point;
            impactObject.right = hitinfo.normal;
            impactObject.GetComponent<AssaultImpact>().Setup(weapon.impact.name);
            PoolManager.dicPool[weapon.projecties.name].OnDespawned(transform);
            hitinfo.collider.GetComponent<EnemyControl>().OnDamage(weapon.weaponData.damage);
            StopCoroutine("WaitDestroy");
        }
    }
    public void OnSpawned()
    {
        isActive = true;
        StartCoroutine("WaitDestroy");
    }
    public void OnDespawned()
    {
        isActive = false;
    }
}
