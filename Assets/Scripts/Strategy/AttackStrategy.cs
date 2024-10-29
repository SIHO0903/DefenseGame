using System;
using UnityEngine;

public abstract class AttackStrategy
{
    float attackTimer;
    protected bool canAttack;

    bool CanAttack(float attackSpeed)
    {
        if (attackTimer >= attackSpeed)
        {
            attackTimer = 0f;
            return true;
        }
        else
        {
            attackTimer += Time.deltaTime;
            return false;
        }
    }

    public void Attack(Animator animator,UnitData unitData, Action<float> targetHealth,Vector3 position,Vector3 targetPos)
    {
        if (CanAttack(unitData.AttackSpeed))
        {
            animator.SetTrigger("Attack");
            canAttack = true;
        }
        AttackLogic(animator,unitData, targetHealth, position,targetPos);
    }
    public abstract void AttackLogic(Animator animator, UnitData unitData, Action<float> targetHealth,Vector3 position, Vector3 targetPos);

}
