using System;
using UnityEngine;

public class MagicAttack : AttackStrategy
{
    Factory factory;
    public override void AttackLogic(Animator animator, UnitData unitData, Action<float> targetHealth, Vector3 position, Vector3 targetPos)
    {
        Debug.Log("MagicAttack");

        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Attack01") &&
            animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.5f && canAttack)
        {
            SoundManager.Instance.PlaySFX(SoundType.MagicAttack);
            factory = new ConcreteFireBallFactory();
            factory.GetProduct(unitData.Damage, position, targetHealth, targetPos);

            canAttack = false;
        }
    }
}