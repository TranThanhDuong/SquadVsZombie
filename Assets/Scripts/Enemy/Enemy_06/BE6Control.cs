using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class BE6Control : MonoBehaviour
{
    public Transform transParent;
    public LayerMask layerMask;
    private int damage = 0;
    public void InitBullet(Transform target, int damage=0)
    {
        transform.SetParent(null);
        Animator animator = gameObject.GetComponent<Animator>();
        this.damage = damage;
        transParent.DOJump(target.position, 2, 1, 1,false).OnComplete(() =>
        {
            animator.SetTrigger("Boom");
            transform.right = Vector2.right;
            Attack_03();
        }).SetEase(Ease.Linear);

    }

    private void Attack_01()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 3, layerMask);

        foreach(Collider2D e in colliders)
        {
            UnitControl unitControl = e.GetComponent<UnitControl>();

            if(unitControl!=null)
            {
                unitControl.OnDamage(damage);
            }
        }
    }
    private void Attack_02()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 3, layerMask);

        foreach (Collider2D e in colliders)
        {

            if (e.transform.position.y < transform.position.y)
                continue;
            UnitControl unitControl = e.GetComponent<UnitControl>();

            if (unitControl != null)
            {

                unitControl.OnDamage(damage);
            }
        }
    }
    private void Attack_03()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 3, layerMask);

        foreach (Collider2D e in colliders)
        {
            Vector3 dir = e.transform.position - transform.position;
            dir.Normalize();
            float dot = Vector3.Dot(transform.up, dir);

            if (dot > -0.5f || dot > 0.5f)
                continue;
            UnitControl unitControl = e.GetComponent<UnitControl>();

            if (unitControl != null)
            {
                if(unitControl.isAlive)
                {
                    unitControl.OnDamage(damage);
                }
            }
        }
    }
    private void Update()
    {
        Vector3 dir = transform.position -transParent.position;
        dir.Normalize();

        transform.right = Vector3.MoveTowards(transform.right,dir,Time.deltaTime*2);
        transform.position = transParent.position;
    }
    public void OnEndBoom()
    {
        Destroy(transParent.gameObject);
        Destroy(gameObject);
    }
}
