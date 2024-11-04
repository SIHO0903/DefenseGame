using TMPro;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class UpgradeUI
{
    public GameObject UI;
    public TMP_Text nameNLevel;
    public TMP_Text healthGrowthTxt;
    public TMP_Text damageGrowthTxt;
    public TMP_Text moneyCostTxt;
    public TMP_Text gemCostTxt;
    public Button btn;
}
[System.Serializable]
public class UpgradeLevels
{
    public TMP_Text nameTxt;
    public Transform levels;
}
public class ResearchMenu : BaseMenu
{
    [Header("View")]
    [SerializeField] UpgradeUI upgradeUI;
    [SerializeField] Transform content;
    [SerializeField] GameObject researchStageUIPrefab;
    [SerializeField] GameObject LvlUIPrefab;

    [Header("Model")]
    [SerializeField] DataContainer dataContainer;
    [SerializeField] MoneyGem moneyGem;

    Color highLevelBtnColor = new Color(255 / 255, 112/ 255, 98/ 255, 1);
    UpgradeLevels[] researchs;
    public override void Awake()
    {
        base.Awake();
        moneyGem.LoadResource();
        CreateButton();
        ButtonDesc(0,dataContainer.castleData, "Ã¼·Â");
        ButtonDesc(1,dataContainer.supplyResources, "¼Óµµ","ÃÑ·®");
        int index = 2;
        foreach (UnitData unitData in dataContainer.UnitDatas)
        {
            ButtonDesc(index, unitData, "Ã¼·Â","°ø°Ý·Â");
            index++;
        }
        SoundManager.Instance.PlayBGM(BGMType.Prepare);
    } 
    void CreateButton()
    {
        researchs = new UpgradeLevels[dataContainer.UnitDatas.Length + 2];

        CreateButtonData(0, dataContainer.castleData.UnitName,      dataContainer.castleData.unitUpgradeData.unitUpgrade.Length,      dataContainer.castleData.UnitKorName);
        CreateButtonData(1, dataContainer.supplyResources.UnitName, dataContainer.supplyResources.unitUpgradeData.unitUpgrade.Length, dataContainer.supplyResources.UnitKorName);

        for (int i = 2; i < researchs.Length; i++)
        {
            CreateButtonData(i, dataContainer.UnitDatas[i - 2]);
        }


    }
    void CreateButtonData(int index, UnitData unitData)
    {
        researchs[index] = new UpgradeLevels();
        GameObject building = Instantiate(researchStageUIPrefab, content);
        building.name = unitData.UnitName;
        researchs[index].nameTxt = building.GetComponentInChildren<TMP_Text>();
        researchs[index].levels = building.transform.GetChild(1);

        int lvlLength = unitData.unitUpgradeData.unitUpgrade.Length;
        for (int i = 0; i < lvlLength; i++)
            Instantiate(LvlUIPrefab, researchs[index].levels);

        Destroy(researchs[index].levels.GetComponentsInChildren<Image>()[lvlLength * 2 - 2].gameObject);

        researchs[index].nameTxt.text = unitData.UnitKorName;

    }
    void CreateButtonData(int index, string unitName, int upgradeLength, string unitKorName)
    {
        researchs[index] = new UpgradeLevels();
        GameObject castle = Instantiate(researchStageUIPrefab, content);
        castle.name = unitName;
        researchs[index].nameTxt = castle.GetComponentInChildren<TMP_Text>();
        researchs[index].levels = castle.transform.GetChild(1);

        for (int i = 0; i < upgradeLength; i++)
            Instantiate(LvlUIPrefab, researchs[index].levels);

        Destroy(researchs[index].levels.GetComponentsInChildren<Image>()[upgradeLength * 2 - 2].gameObject);

        researchs[index].nameTxt.text = unitKorName;
    }

    void ButtonDesc(int researchIndex,IUpgradeableUnit unit, string growthTxt1, string growthTxt2 = null)
    {
        researchs[researchIndex].nameTxt.text = unit.UnitName;
        researchs[researchIndex].nameTxt.text = unit.UnitKorName;

        Button[] buttons = researchs[researchIndex].levels.GetComponentsInChildren<Button>();
        Image[] lines = researchs[researchIndex].levels.GetComponentsInChildren<Image>();

        for (int j = 0; j < buttons.Length; j++)
        {
            int level = unit.unitUpgradeData.unitUpgrade[j].level;
            float healthGrowth = unit.unitUpgradeData.unitUpgrade[j].growth1;
            float damageGrowth = unit.unitUpgradeData.unitUpgrade[j].growth2;
            string secondTxt = damageGrowth==0 ?"" : MyUtil.StringBuilderTxt($"  {growthTxt2} {damageGrowth}");

            buttons[j].GetComponentInChildren<TMP_Text>().text = MyUtil.StringBuilderTxt($"  Lv {level} \n{growthTxt1} {healthGrowth}{secondTxt}");
            int index = j;
            buttons[index].onClick.AddListener(() =>
            {
                SoundManager.Instance.PlaySFX(SoundType.ClickBtn);

                Button nextButton;
                Image line;
                if (index == buttons.Length - 1)
                {
                    nextButton = null;
                    line = null;
                }
                else
                {
                    nextButton = buttons[index + 1];
                    line = lines[index * 2];
                }
                UpgradeUI(unit, index, buttons[index], nextButton, line, growthTxt1, growthTxt2);
            });
            buttons[j].interactable = false;

        }

        int curLength = unit.Level - 1;

        if (curLength >= 1)
        {
            for (int i = 0; i < curLength; i++)
                UpgradedBtn(buttons[i], lines[i * 2]);

            buttons[curLength].interactable = true;

            for (int i = curLength + 1; i < buttons.Length; i++)
                HighLvlBtn(buttons[i]);
        }
        else
            buttons[0].interactable = true;

    }
    void HighLvlBtn(Button button)
    {
        ColorBlock cb = button.colors;
        cb.disabledColor = highLevelBtnColor;
        button.colors = cb;
        button.interactable = false;
    }
    void UpgradedBtn(Button button,Image line)
    {
        if(line !=null) line.color = Color.green;
        ColorBlock cb = button.colors;
        cb.disabledColor = Color.green;
        button.colors = cb;
        button.interactable = false;
    }
    void UpgradeUI(IUpgradeableUnit unitData, int index, Button button, Button nextbutton, Image line,string growthTxt1,string growthTxt2=null)
    {
        upgradeUI.UI.SetActive(true);

        string korName = unitData.UnitKorName;
        int level = unitData.Level+1;
        float moneyCost = unitData.MoneyCost(index);
        float gemCost = unitData.GemCost(index);
        float curGrowth1 = unitData.CurGrowth1();
        float curGrowth2 = unitData.CurGrowth2();
        float growth1 = unitData.GetGrowth1(index);
        float growth2 = unitData.GetGrowth2(index);
        if(index == unitData.Level)
        {
            nextbutton = null;
            line = null;
        }

        upgradeUI.nameNLevel.text = MyUtil.StringBuilderTxt(korName + " Lv " + level);
        upgradeUI.healthGrowthTxt.text = MyUtil.StringBuilderTxt(growthTxt1 + "\n" + curGrowth1 + " + " + growth1);
        upgradeUI.damageGrowthTxt.text = MyUtil.StringBuilderTxt(curGrowth2 == 0 ? "" : growthTxt2 + "\n" + curGrowth2 + " + " + growth2);
        upgradeUI.moneyCostTxt.text = MyUtil.StringBuilderTxt("°ñµå : " + moneyCost);
        upgradeUI.gemCostTxt.text = MyUtil.StringBuilderTxt("Áª : " + gemCost);

        upgradeUI.btn.interactable = moneyGem.Money >= moneyCost && moneyGem.Gem >= gemCost ? true : false;


        upgradeUI.btn.onClick.RemoveAllListeners();

        upgradeUI.btn.onClick.AddListener(() =>
        {
            SoundManager.Instance.PlaySFX(SoundType.Upgrade);

            moneyGem.UseMoney(moneyCost);
            moneyGem.UseGem(gemCost);
            moneyGem.SaveResource();
            unitData.LevelUP(growth1, growth2);

            upgradeUI.nameNLevel.text = MyUtil.StringBuilderTxt(korName + " Lv " + level);
            upgradeUI.healthGrowthTxt.text = MyUtil.StringBuilderTxt(growthTxt1 + "\n" + (curGrowth1 + growth1));
            upgradeUI.damageGrowthTxt.text = MyUtil.StringBuilderTxt(growthTxt2 + "\n" + (curGrowth2 + growth2));
            upgradeUI.btn.interactable = false;
            
            if(nextbutton != null)  nextbutton.interactable = true;

            UpgradedBtn(button,line);
            moneyGem.SaveResource();
        });
    }
}
