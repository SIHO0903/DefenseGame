using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public enum EUnit
{
    Idle,
    Move,
    Chasing,
    Attack,
    GetHit,
    Die,
    Test
}

public interface IUnitData
{
    public UnitData UnitData { get; set; }
}

[RequireComponent(typeof(Rigidbody2D), typeof(CircleCollider2D))]
public class UnitState<T> : MonoBehaviour, IUnitHeatlh, IUnitData
{

    protected IState<T> currentState;
    protected Dictionary<EUnit, IState<T>> states = new Dictionary<EUnit, IState<T>>();
    Slider healthBar;
    TMP_Text healthTxt;

    public SearhTarget searhTarget;
    public float Health { get; set; }
    [field: SerializeField] public UnitData UnitData { get; set; }
    [SerializeField] protected LayerMask targetLayer;
    [HideInInspector] public Rigidbody2D rigid;
    [HideInInspector] public Animator animator;
    public virtual void Awake()
    {
        animator = GetComponentInChildren<Animator>();
        healthBar = GetComponentInChildren<Slider>();
        healthTxt = GetComponentInChildren<TMP_Text>();
        rigid = GetComponent<Rigidbody2D>();
        searhTarget = new SearhTarget(transform, UnitData, targetLayer);
    }
    public virtual void OnEnable()
    {
        Init();
    }
    public void Init()
    {
        Health = UnitData.Health;
        healthBar.value = Health / UnitData.Health;
        healthTxt.text = Health + " / " + UnitData.Health;
    }
    public void TransitionToState(EUnit estate)
    {
        currentState = states[estate];
        currentState.EnterState();
    }
    public void GetHit(float damage)
    {
        Health -= damage;
        healthBar.value = Health / UnitData.Health;
        healthTxt.text = Health + " / " + UnitData.Health;
        Debug.Log(damage + "의 피해를 입음");

        TransitionToState(EUnit.GetHit);
    }
    public bool IsMaxHealth()
    {
        return Health == UnitData.Health ? true : false;
    }
    public void GetHeal(float amount)
    {
        Health += amount;
        Health = Mathf.Min(Health, UnitData.Health);
        healthBar.value = Health / UnitData.Health;
        healthTxt.text = Health + " / " + UnitData.Health;
        Debug.Log(amount + "의 힐을 받음");
    }
    public bool IsDead()
    {
        return Health <= 0 ? true : false;
    }
    public void Die()
    {
        gameObject.SetActive(false);
        Debug.Log(UnitData.UnitName + " 가 파괴되었습니다");
        TransitionToState(EUnit.Move);
    }

}
