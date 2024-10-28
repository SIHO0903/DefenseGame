using UnityEngine;

[CreateAssetMenu(menuName = "Unit/SupplyResources", fileName = "UnitData")]
public class SupplyResources : ScriptableObject, IUpgradeableUnit
{
    [field: SerializeField] public string UnitKorName { get; set; }
    [field: SerializeField] public string UnitName { get; set; }
    [field: SerializeField] public int Level { get; set; }
    [field: SerializeField] public float SupplySpeed { get; set; }
    [field: SerializeField] public float SupplyCapacity { get; set; }

    [field: SerializeField] public UnitUpgradeDataSO unitUpgradeData { get; set; }

    public void LevelUP(float growth1, float growth2)
    {
        Level++;
        SupplySpeed += growth1;
        SupplyCapacity += growth2;
    }

    public float MoneyCost(int index) => unitUpgradeData.unitUpgrade[index].moneyCost;
    public float GemCost(int index) => unitUpgradeData.unitUpgrade[index].gemCost;
    public float CurGrowth1() => SupplySpeed;
    public float CurGrowth2() => SupplyCapacity;
    public float GetGrowth1(int index) => unitUpgradeData.unitUpgrade[index].growth1;
    public float GetGrowth2(int index) => unitUpgradeData.unitUpgrade[index].growth2;
}
