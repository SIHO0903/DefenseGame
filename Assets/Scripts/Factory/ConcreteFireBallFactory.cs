﻿using System;
using UnityEngine;

public class ConcreteFireBallFactory : Factory
{
    public override IProjectile GetProduct(float damage, Vector3 position, Action<float> targetHealth, Vector3 targetPos)
    {
        GameObject instance = PoolManager.Instance.Get(PoolEnum.Projectile, "FireBall", position, Quaternion.identity);
        FireBall fireBall = instance.GetComponent<FireBall>();
        fireBall.Initialize(damage, targetHealth);
        fireBall.Shoot(targetPos);
        return fireBall;
    }
}