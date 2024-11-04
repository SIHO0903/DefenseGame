using System;
using UnityEngine;

public class ConcreteArrowFactory : Factory
{
    public override IProjectile GetProduct(float damage,Vector3 position,string targetString, Action<float> targetHealth, Vector3 targetPos)
    {
        GameObject instance = PoolManager.Instance.Get(PoolEnum.Projectile, "Arrow01", position, Quaternion.identity);
        Arrow arrow = instance.GetComponent<Arrow>();
        arrow.Initialize(damage, targetString, targetHealth);
        arrow.Shoot(targetPos);

        return arrow;
    }
}

