using UnityEngine;


public class ChasingState<T> : BaseState<T> where T : UnitState<T>
{
    Transform enemyTransform;
    Vector3 dir;
    public ChasingState(UnitData unitData) : base(unitData)
    {
    }
    public override void EnterState()
    {
        enemyTransform = null;
        dir = Vector3.right;
        Debug.Log("ChasingState<T>");      
    }
    public override void UpdateState(T owner)
    {
        enemyTransform = owner.searhTarget.TargetTransform(unitData.DetectRange);

        owner.rigid.velocity = dir * MyUtil.MoveSpeed(unitData.MoveSpeed);
        owner.animator.SetBool("IsMove", true);

        if (enemyTransform == null)
            owner.TransitionToState(EUnit.Move);
        else
        {
            dir = enemyTransform.position - owner.transform.position;
            dir.Normalize();
            InAttackRange(owner);
        }
    }

    private void InAttackRange(T owner)
    {
        float distance = Vector3.Distance(enemyTransform.position, owner.transform.position);
        if (distance <= unitData.AttackRange)
        {
            owner.rigid.velocity = Vector3.zero;
            owner.animator.SetBool("IsMove", false);
            owner.TransitionToState(EUnit.Attack);
        }
    }
}
