using System;
using UnityEngine;

public class AttackState<T> : BaseState<T> where T : UnitState<T>
{

    private AttackStrategy attackStrategy;
    string target;
    Vector3 targetPos;
    public AttackState(UnitData unitData, AttackStrategy attackStrategy) : base(unitData)
    {
        this.attackStrategy = attackStrategy;
    }

    public override void EnterState()
    {
        target = "";
        targetPos = Vector3.zero;
        Debug.Log("Entering Attack State");

    }

    public override void UpdateState(T owner)
    {
        if(target =="")
        {
            target = owner.searhTarget.TargetTag();
            targetPos = owner.searhTarget.TargetTransform(unitData.AttackRange).position;
        }
        if (owner.searhTarget.DetectComPareTag(target, unitData.AttackRange, out Action<float> targetHealth))
            attackStrategy.Attack(owner.animator,unitData, target, targetHealth, owner.transform.position, targetPos);
        else
            owner.TransitionToState(EUnit.Move);
    }

}
