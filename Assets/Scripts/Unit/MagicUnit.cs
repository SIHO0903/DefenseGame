
public class MagicUnit : UnitState<MagicUnit>
{
    public override void Awake()
    {
        base.Awake();
        states.Add(EUnit.Idle, new IdleState<MagicUnit>(UnitData));
        states.Add(EUnit.Move, new MoveState<MagicUnit>(UnitData));
        states.Add(EUnit.Chasing, new ChasingState<MagicUnit>(UnitData));
        states.Add(EUnit.Attack, new AttackState<MagicUnit>(UnitData, new MagicAttack()));
        states.Add(EUnit.GetHit, new GetHitState<MagicUnit>(UnitData));
        states.Add(EUnit.Die, new DieState<MagicUnit>(UnitData));

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
