using UnityEngine;

[CreateAssetMenu(menuName = "Unit/UnitUpgradeData", fileName = "UnitUpgradeData")]
public class UnitUpgradeDataSO : ScriptableObject
{
    public ResearchUpgrade[] unitUpgrade;
}


[System.Serializable]
public class ResearchUpgrade
{
    public int level;
    public float moneyCost;
    public float gemCost;
    public float growth1;
    public float growth2;
    public ResearchUpgrade(int level,float moneyCost,float gemCost, float growth1, float growth2)
    {
        this.level = level;
        this.growth1 = growth1;
        this.growth2 = growth2;
        this.moneyCost = moneyCost;
        this.gemCost = gemCost;
    }
}