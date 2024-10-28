
public class RangeUnit : UnitState<RangeUnit>
{
    public override void Awake()
    {
        base.Awake();
        states.Add(EUnit.Idle, new IdleState<RangeUnit>(UnitData));
        states.Add(EUnit.Move, new MoveState<RangeUnit>(UnitData));
        states.Add(EUnit.Chasing, new ChasingState<RangeUnit>(UnitData));
        states.Add(EUnit.Attack, new AttackState<RangeUnit>(UnitData, new RangeAttack()));
        states.Add(EUnit.GetHit, new GetHitState<RangeUnit>(UnitData));
        states.Add(EUnit.Die, new DieState<RangeUnit>(UnitData));

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

