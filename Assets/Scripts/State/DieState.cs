using UnityEngine;

public class DieState<T> : BaseState<T> where T : UnitState<T>
{
    public DieState(UnitData unitData) : base(unitData)
    {
    }

    public override void EnterState()
    {
        SoundManager.Instance.PlaySFX(SoundType.Die);
        Debug.Log(unitData.UnitName + "사망");
    }

    public override void UpdateState(T owner)
    {
        Debug.Log("사망 Update");
        owner.Die(); 
    }
}
