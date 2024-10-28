using System;
using UnityEngine;

public class HealState<T> : BaseState<T> where T : UnitState<T>
{
    private AttackStrategy attackStrategy;
    string target;
    Vector3 targetPos;
    public HealState(UnitData unitData, AttackStrategy attackStrategy) : base(unitData)
    {
        this.attackStrategy = attackStrategy;
    }

    public override void EnterState()
    {
        target = "";
        targetPos = Vector3.zero;

        Debug.Log("Entering Heal State");
    }

    public override void UpdateState(T owner)
    {
        owner.rigid.velocity = Vector3.zero;
        target = owner.searhTarget.TargetTag();
        Transform targetTransform = owner.searhTarget.HealTargetTransform();

        // 아군을 감지하여 치유
        if (owner.searhTarget.DetectHealComPareTag(target, out Action<float> targetHeal))
        {
            attackStrategy.Attack(owner.animator,unitData, targetHeal, owner.transform.position, targetTransform.position);
        }
        else if(targetTransform == null)
        {
            owner.TransitionToState(EUnit.Move); // 아군이 없으면 이동 상태로
        }
    }
}
