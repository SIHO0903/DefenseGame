using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Castle : MonoBehaviour, IUnitHeatlh
{
    public CastleData castleData;
    public float Health { get; set; }
    Slider healthBar;
    TMP_Text healthTxt;

    public Action<Castle> CastleDestroy; 
    public void Awake()
    {
        healthBar = GetComponentInChildren<Slider>();
        healthTxt = GetComponentInChildren<TMP_Text>();
    }
    private void OnEnable()
    {
        Init();

    }
    public void Init()
    {
        Health = castleData.Health;
        UpdateHealthBar();
    }
    public void GetHit(float damage)
    {
        Health -= damage;
        Health = Mathf.Clamp(Health,0,castleData.Health);
        UpdateHealthBar();
        if (IsDead())
            Die();
    }
    public bool IsMaxHealth()
    {
        return Health == castleData.Health;
    }
    public void GetHeal(float amount)
    {
        Health += amount;
        Health = Mathf.Min(Health, castleData.Health);
        UpdateHealthBar();

        Debug.Log(amount + "의 힐을 받음");
    }
    public bool IsDead()
    {
        return Health <= 0;
    }
    private void UpdateHealthBar()
    {
        healthBar.value = Health / castleData.Health;
        healthTxt.text = Health + " / " + castleData.Health;
    }
    public void Die()
    {
        CastleDestroy?.Invoke(this);
        Debug.Log(castleData.UnitName + " 가 파괴되었습니다");
        CastleDestroy = null;
    }



}
