﻿using System;
using UnityEngine;

public abstract class Factory
{
    public abstract IProjectile GetProduct(float damage,Vector3 position, Action<float> targetHealth, Vector3 targetPos);

}