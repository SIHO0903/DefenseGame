public interface IUpgradeableUnit
{
    public string UnitName { get; }
    public string UnitKorName { get; }
    public int Level { get; }
    public UnitUpgradeDataSO unitUpgradeData { get; set; }
    public void LevelUP(float growth1, float growth2);
    public float MoneyCost(int index);
    public float GemCost(int index);
    public float CurGrowth1();
    public float CurGrowth2();
    public float GetGrowth1(int index);
    public float GetGrowth2(int index);
}
