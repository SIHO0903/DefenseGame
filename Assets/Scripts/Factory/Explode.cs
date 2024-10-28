using System;
using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]
public class Explode : HitScan
{
    private void OnEnable()
    {
        StartCoroutine(SetActiveFalse(0.9f));
    }
    public override void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            collision.GetComponent<IUnitHeatlh>().GetHit(Damage);

            canAttack = false;
        }
    }
}
