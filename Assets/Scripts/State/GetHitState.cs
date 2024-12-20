﻿
public class GetHitState<T> : BaseState<T> where T : UnitState<T>
{
    public GetHitState(UnitData unitData) : base(unitData)
    {
    }

    public override void EnterState()
    {
        SoundManager.Instance.PlaySFX(SoundType.GetHit);

    }
    public override void UpdateState(T owner)
    {
        if (owner.IsDead())
        {
            owner.TransitionToState(EUnit.Die);
        }
        else
        {
            owner.animator.SetTrigger("GetHit");
            if (owner.animator.GetCurrentAnimatorStateInfo(0).IsName("Hurt") &&
                owner.animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.99f)
                owner.TransitionToState(EUnit.Move);
        }
    }
}