using UnityEngine;

public class HealChasingState<T> : BaseState<T> where T : UnitState<T>
{
    Transform enemyTransform;
    public HealChasingState(UnitData unitData) : base(unitData)
    {
    }
    public override void EnterState()
    {
        enemyTransform = null;
        Debug.Log("ChasingState<T>");
    }
    public override void UpdateState(T owner)
    {
        Debug.Log("HealChasingState Update");
        Vector3 dir = Vector3.right;
        enemyTransform = owner.searhTarget.HealTargetTransform(unitData.DetectRange);
        owner.animator.SetBool("IsMove", true);

        if (enemyTransform != null)
        {
            float distance = Vector3.Distance(enemyTransform.position, owner.transform.position);
            if (distance <= unitData.AttackRange)
            {
                owner.rigid.velocity = Vector3.zero;
                owner.animator.SetBool("IsMove", false);
                owner.TransitionToState(EUnit.Attack);
            }
        }

        owner.rigid.velocity = dir * MyUtil.MoveSpeed(unitData.MoveSpeed);
    }
}