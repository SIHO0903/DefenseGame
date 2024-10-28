
public class MeleeUnit : UnitState<MeleeUnit>
{
    public override void Awake()
    {
        base.Awake();
        states.Add(EUnit.Idle, new IdleState<MeleeUnit>(UnitData));
        states.Add(EUnit.Move, new MoveState<MeleeUnit>(UnitData));
        states.Add(EUnit.Chasing, new ChasingState<MeleeUnit>(UnitData));
        states.Add(EUnit.Attack, new AttackState<MeleeUnit>(UnitData, new MeleeAttack()));
        states.Add(EUnit.GetHit, new GetHitState<MeleeUnit>(UnitData));
        states.Add(EUnit.Die, new DieState<MeleeUnit>(UnitData));

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
