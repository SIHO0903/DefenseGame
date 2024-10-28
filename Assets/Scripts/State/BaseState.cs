public abstract class BaseState<T> : IState<T>
{
    protected UnitData unitData;
    public BaseState(UnitData unitData)
    {
        this.unitData = unitData;      
    }
    public abstract void EnterState();
    public abstract void UpdateState(T owner);

}
