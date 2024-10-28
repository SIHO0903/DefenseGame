using UnityEngine;


[CreateAssetMenu(menuName = "Unit/UnitData", fileName = "UnitData")]
public class UnitData : ScriptableObject, IMove, IDamage, IUpgradeableUnit
{
    [field: SerializeField] public Sprite Image { get; set; }
    [field: SerializeField] public string UnitKorName { get; set; }
    [field: SerializeField] public string UnitName { get; set; }
    [field: SerializeField] public int Level { get; set; }
    [field: SerializeField] public float Health { get; set; }
    [field: SerializeField] public float Damage { get; set; }
    [field: SerializeField] public float AttackSpeed { get; set; }
    [field: SerializeField] public float MoveSpeed { get; set; }
    [field: SerializeField] public float DetectRange { get; set; }
    [field: SerializeField] public float AttackRange { get; set; }
    [field: SerializeField] public int Cost { get; set; }
    [field: SerializeField] public MoveDir MoveDir { get; set; }
    [field: SerializeField] public UnitUpgradeDataSO unitUpgradeData { get; set; }

    [field: SerializeField] public float MaxHealth { get; set; }
    [field: SerializeField] public float MaxDamage { get; set; }

    public void MaxHealthNDamage()
    {
        MaxHealth = Health;
        MaxDamage = Damage;
        for (int i = Level-1; i < unitUpgradeData.unitUpgrade.Length; i++)
        {
            MaxHealth += unitUpgradeData.unitUpgrade[i].growth1;
            MaxDamage += unitUpgradeData.unitUpgrade[i].growth2;
        }
    }
    public void LevelUP(float health, float damage)
    {
        Level++;
        Health += health;
        Damage += damage;
    }
    public float MoneyCost(int index) => unitUpgradeData.unitUpgrade[index].moneyCost;
    public float GemCost(int index) => unitUpgradeData.unitUpgrade[index].gemCost;
    public float CurGrowth1() => Health;
    public float CurGrowth2() => Damage;
    public float GetGrowth1(int index) => unitUpgradeData.unitUpgrade[index].growth1;
    public float GetGrowth2(int index) => unitUpgradeData.unitUpgrade[index].growth2;
}
