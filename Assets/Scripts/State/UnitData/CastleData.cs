using UnityEngine;

[CreateAssetMenu(menuName = "Unit/CastleData", fileName = "UnitData")]
public class CastleData : ScriptableObject, IUpgradeableUnit
{
    [field: SerializeField] public string UnitKorName { get; set; }
    [field: SerializeField] public string UnitName { get; set; }
    [field: SerializeField] public int Level { get; set; }
    [field: SerializeField] public float Health { get; set; }
    [field: SerializeField] public UnitUpgradeDataSO unitUpgradeData { get; set; }

    public void LevelUP(float growth1, float growth2)
    {
        Level++;
        Health += growth1;

    }
    public float MoneyCost(int index) => unitUpgradeData.unitUpgrade[index].moneyCost;
    public float GemCost(int index) => unitUpgradeData.unitUpgrade[index].gemCost;
    public float CurGrowth1() => Health;
    public float CurGrowth2() => 0;
    public float GetGrowth1(int index) => unitUpgradeData.unitUpgrade[index].growth1;
    public float GetGrowth2(int index) => unitUpgradeData.unitUpgrade[index].growth2;

}
