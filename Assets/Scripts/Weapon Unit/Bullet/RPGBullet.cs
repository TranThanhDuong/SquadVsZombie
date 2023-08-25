using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class RPGBullet : BulletUnit
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
        transform.SetParent(null);
        base.Setup(weapon);
    }

    private void Attack_03()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 2, mask);

        foreach (Collider2D e in colliders)
        {
            //Vector3 dir = e.transform.position - transform.position;
            //Debug.LogError("test dir: " + dir + "pos1: " + e.transform.position + " pos2: " + transform.position);
            //dir.Normalize();
            //float dot = Vector3.Dot(transform.up, dir);
            //Debug.LogError("test dir: " + dir + " _dot: " + dot);
            //if (dot < -0.3f)
            //    continue;
            EnemyControl enemy = e.GetComponent<EnemyControl>();

            if (enemy != null)
            {
                enemy.OnDamage(weapon.weaponData.damage);
            }
        }
    }
    private void Update()
    {
        gameObject.transform.Translate(transform.right * Time.deltaTime * 7f);
        RaycastHit2D hitinfo = Physics2D.Raycast(transform.position, transform.right, 0.2f, mask);
        if (hitinfo.collider != null)
        {
            Animator animator = gameObject.GetComponent<Animator>();
            Attack_03();
            Transform impactObject = PoolManager.dicPool[weapon.impact.name].OnSpawned();
            impactObject.SetParent(null);
            impactObject.position = hitinfo.point;
            impactObject.right = hitinfo.normal;
            impactObject.GetComponent<RPGImpact>().Setup(weapon.impact.name);
            PoolManager.dicPool[weapon.projecties.name].OnDespawned(transform);
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
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, 1.5f);
    }
}
