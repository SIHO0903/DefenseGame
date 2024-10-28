using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UnitTrainingFacility : BaseFacility
{
    [Header("View")]
    [SerializeField] GameObject unitButtons;
    [SerializeField] TMP_Text nameTxt;
    [SerializeField] TMP_Text healthTxt;
    [SerializeField] TMP_Text damageTxt;
    [SerializeField] Slider healthSlider;
    [SerializeField] Slider damageSlider;
    [SerializeField] TMP_Text attackSpeedTxt;
    [SerializeField] TMP_Text attackRangeTxt;
    [SerializeField] TMP_Text moveSpeedTxt;
    [SerializeField] TMP_Text costTxt;
    Button[] buttons;

    [Header("Model")]
    [SerializeField] DataContainer unitDataContainer;
    void Start()
    {
        buttons = unitButtons.GetComponentsInChildren<Button>();

        foreach (var button in buttons)
        {
            button.onClick.AddListener(() =>
            {
                SoundManager.Instance.PlaySFX(SoundType.ClickBtn);
                Click(button.name);
            });
        }
        foreach (UnitData unitData in unitDataContainer.UnitDatas)
        {
            unitData.MaxHealthNDamage();
        }


    }

    // 클릭된 유닛 버튼의 이름을 사용하여 해당 유닛의 정보를 UI에 표시
    void Click(string buttonName)
    {
        for (int i = 0; i < unitDataContainer.UnitDatas.Length; i++)
        {
            if (unitDataContainer.UnitDatas[i].UnitName == buttonName)
            {
                ShowClickedInfo(unitDataContainer.UnitDatas[i]);
            }
        }    
    }
    void ShowClickedInfo(UnitData clickedUnit)
    {
        nameTxt.text = MyUtil.StringBuilderTxt($"{clickedUnit.UnitKorName} <color=yellow>{clickedUnit.Level}레벨</color>");
        healthTxt.text = MyUtil.StringBuilderTxt($"체력 : {clickedUnit.Health}");
        damageTxt.text = MyUtil.StringBuilderTxt($"공격력 : {clickedUnit.Damage}");
        attackSpeedTxt.text = MyUtil.StringBuilderTxt($"공격속도 : {clickedUnit.AttackSpeed}");
        attackRangeTxt.text = MyUtil.StringBuilderTxt($"사거리 : {clickedUnit.AttackRange}");
        moveSpeedTxt.text = MyUtil.StringBuilderTxt($"이동속도 : {clickedUnit.MoveSpeed}");
        costTxt.text = MyUtil.StringBuilderTxt($"비용 : {clickedUnit.Cost}");

        healthSlider.value = clickedUnit.Health / clickedUnit.MaxHealth;
        damageSlider.value = clickedUnit.Damage / clickedUnit.MaxDamage;

    }
}
