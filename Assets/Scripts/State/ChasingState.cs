using UnityEngine;


public class ChasingState<T> : BaseState<T> where T : UnitState<T>
{
    Transform enemyTransform;
    public ChasingState(UnitData unitData) : base(unitData)
    {
    }

    public override void EnterState()
    {
        enemyTransform = null;
        Debug.Log("ChasingState<T>");
        
    }

    public override void UpdateState(T owner)
    {
        
        //Debug.Log("ChasingState Update");

        enemyTransform = owner.searhTarget.TargetTransform();

        Vector3 dir = enemyTransform.position - owner.transform.position;
        dir.Normalize();

        owner.rigid.velocity = dir * MyUtil.MoveSpeed(unitData.MoveSpeed);
        owner.animator.SetBool("IsMove",true);

        float distance = Vector3.Distance(enemyTransform.position, owner.transform.position);

        if (distance <= unitData.AttackRange)
        {
            owner.rigid.velocity = Vector3.zero;
            owner.animator.SetBool("IsMove", false);
            owner.TransitionToState(EUnit.Attack);
        }

        //따라갈 유닛이 사라지면 다시 Idle로
    }
}
