
public class HealUnit : UnitState<HealUnit>
{
    public override void Awake()
    {
        base.Awake();
        states.Add(EUnit.Idle, new IdleState<HealUnit>(UnitData));
        states.Add(EUnit.Move, new MoveState<HealUnit>(UnitData));
        states.Add(EUnit.Chasing, new HealChasingState<HealUnit>(UnitData));
        states.Add(EUnit.Attack, new HealState<HealUnit>(UnitData, new Heal()));
        states.Add(EUnit.GetHit, new GetHitState<HealUnit>(UnitData));
        states.Add(EUnit.Die, new DieState<HealUnit>(UnitData));

    }
    public override void OnEnable()
    {
        base.OnEnable();
        TransitionToState(EUnit.Move);
    }
    private void Update()
    {
        currentState.UpdateState(this);
    }
}
