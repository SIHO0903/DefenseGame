public interface IState<T>
{
    abstract void EnterState();
    abstract void UpdateState(T owner);
}
