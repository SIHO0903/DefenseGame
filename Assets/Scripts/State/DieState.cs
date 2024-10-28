using UnityEngine;

public class DieState<T> : BaseState<T> where T : UnitState<T>
{
    public DieState(UnitData unitData) : base(unitData)
    {
    }

    public override void EnterState()
    {
        // Die 상태 진입 시 수행할 작업
        SoundManager.Instance.PlaySFX(SoundType.Die);
        Debug.Log(unitData.UnitName + " has died.");
    }

    public override void UpdateState(T owner)
    {
        // Die 상태 유지
        Debug.Log("사망 Update");
        owner.Die(); //사망애니메이션끝나고 사라지게
        //UnitManager.Instance.friendlyUnit.AddUnit(owner.gameObject);
    }
}
