using UnityEngine;

public class IdleState<T> : BaseState<T> where T : UnitState<T>
{
    public IdleState(UnitData unitData) : base(unitData)
    {
    }

    public override void EnterState()
    {
        //Debug.Log(unitData.health);
        //Debug.Log("Entering Idle State");

    }

    public override void UpdateState(T owner)
    {
        // 지시가 내려졌을때 move로 이동
        // 또는 주변에 몹이 잇을 체이싱으로
        //Debug.Log("IdleState Update");

        if (Input.GetKeyDown(KeyCode.A))
            owner.animator.SetTrigger("Attack");
        if (Input.GetKeyDown(KeyCode.S))
            owner.animator.SetTrigger("TestAttack2");
        if (Input.GetKeyDown(KeyCode.D))
            owner.TransitionToState(EUnit.Move);
    }
}

