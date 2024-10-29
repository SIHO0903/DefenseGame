using System;
using UnityEngine;

public class MeleeAttack : AttackStrategy
{
    public override void AttackLogic(Animator animator, UnitData unitData, Action<float> targetHealth, Vector3 position, Vector3 targetPos)
    {
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Attack01") &&
            animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.5f && canAttack)
        {
            SoundManager.Instance.PlaySFX(SoundType.MeleeAttack);
            targetHealth?.Invoke(unitData.Damage);


            canAttack = false;
        }
    }
}
