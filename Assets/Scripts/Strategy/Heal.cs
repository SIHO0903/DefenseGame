using System;
using UnityEngine;

public class Heal : AttackStrategy
{

    public override void AttackLogic(Animator animator, UnitData unitData, Action<float> targetHealth, Vector3 position, Vector3 targetPos)
    {
        Debug.Log("힐");
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Attack01") &&
            animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.5f && canAttack)
        {
            SoundManager.Instance.PlaySFX(SoundType.Heal);
            PoolManager.Instance.Get(PoolEnum.HitScan, "HealEffect", targetPos, Quaternion.identity);
            targetHealth(unitData.Damage);
            canAttack = false;
        }
    }
}
