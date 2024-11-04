using System;
using UnityEngine;

public interface IProjectile
{
    public float Damage { get; set; }
    public void Initialize(float unitDamage,string TargetString, Action<float> targetHealth);
    public void Shoot(Vector3 targetPosition);
}
