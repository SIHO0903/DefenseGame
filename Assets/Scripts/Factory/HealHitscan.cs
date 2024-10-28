using System;
using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]
public class HealHitscan : HitScan
{
    public override void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Friendly"))
        {
            StopCoroutine(SetActiveFalse());
            StartCoroutine(SetActiveFalse());
            canAttack = false;
        }
    }
}