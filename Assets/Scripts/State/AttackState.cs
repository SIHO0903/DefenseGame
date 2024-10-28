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

        // Attack 상태 진입 시 수행할 작업
        Debug.Log("Entering Attack State");

    }

    public override void UpdateState(T owner)
    {
        //Debug.Log("UpdateState Attack");
        if(target =="")
        {
            Debug.Log("AttackState UpdateState : 타겟체크");
            target = owner.searhTarget.TargetTag();
            targetPos = owner.searhTarget.TargetTransform().position;
            Debug.Log(owner.searhTarget.TargetTransform().name);
        }

        //owner.Temp("Attack" , owner.searhTarget.TargetTransform().name);
        if (owner.searhTarget.DetectComPareTag(target, out Action<float> targetHealth))
        {

            attackStrategy.Attack(owner.animator,unitData, targetHealth, owner.transform.position, targetPos);
        }
        else
        {
            owner.TransitionToState(EUnit.Move);
        }
    }

}
