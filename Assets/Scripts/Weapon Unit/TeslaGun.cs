using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class TeslaGun : WeaponUnitBehaviour
{
    public Animator animator;
    public LayerMask maskEnemy;
    public Tweener tween;
    public SpriteRenderer spriteRenderer;
    public override void Setup(object data)
    {
        base.Setup(data);
        impact.gameObject.SetActive(false);
        weaponUnitHandle = new ITeslaGunHandle();
    }
    public override void OnEndFire()
    {
        base.OnEndFire();
        OnFireAnimation(false);
    }
    public void OnFireAnimation(bool isFire)
    {
        if(!isFire)
            tween.Kill();
        animator.SetBool("Fire", isFire);
    }
    public void SetImpactPos(Vector3 pos)
    {
        float dis = Vector2.Distance(transform.position, pos);
        Vector2 s = new Vector2(dis, spriteRenderer.size.y);
        spriteRenderer.size = s;
        impact.transform.localPosition = new Vector3(dis, 0, 0);
    }
}
public class ITeslaGunHandle : IWeaponUnitHandle
{
    private TeslaGun teslaGun;
    [SerializeField]
    private float timeDamage = 0.5f;
    private float curTimeDamage = 0;
    public void FireHandle(object data)
    {
        teslaGun = (TeslaGun)data;
        teslaGun.OnFireAnimation(true);
        teslaGun.impact.gameObject.SetActive(true);
        float timeCount = 0;
        teslaGun.tween= DOTween.To(() => timeCount, x => timeCount = x, 1, 0.02f).SetLoops(-1).OnStepComplete(() =>
        {
            curTimeDamage += Time.deltaTime;
            RaycastHit2D hitInfo= Physics2D.Raycast(teslaGun.transform.position, teslaGun.transform.right, teslaGun.weaponData.range,teslaGun.maskEnemy);
            if(hitInfo.collider!=null)
            {
                teslaGun.SetImpactPos(hitInfo.point);
                if(curTimeDamage >= timeDamage)
                {
                    hitInfo.collider.GetComponent<EnemyControl>().OnDamage(teslaGun.weaponData.damage);
                    curTimeDamage = 0;
                }

            }
        });


    }
}