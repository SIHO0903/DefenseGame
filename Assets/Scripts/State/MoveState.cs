using System;
using UnityEngine;

public class MoveState<T> : BaseState<T> where T : UnitState<T>
{
    public MoveState(UnitData unitData) : base(unitData)
    {
    }
    public override void EnterState()
    {
        Debug.Log("EnterState MoveState");
    }
    public override void UpdateState(T owner)
    {
        owner.rigid.velocity = Convert.ToInt32(unitData.MoveDir) * Vector2.right * MyUtil.MoveSpeed(unitData.MoveSpeed);
        owner.animator.SetBool("IsMove", true);

        if (owner.searhTarget.DetectComPareTag(owner.searhTarget.TargetTag(),unitData.DetectRange))
            owner.TransitionToState(EUnit.Chasing);

    }
}
