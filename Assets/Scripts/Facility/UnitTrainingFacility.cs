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

    // Ŭ���� ���� ��ư�� �̸��� ����Ͽ� �ش� ������ ������ UI�� ǥ��
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
        nameTxt.text = MyUtil.StringBuilderTxt($"{clickedUnit.UnitKorName} <color=yellow>{clickedUnit.Level}����</color>");
        healthTxt.text = MyUtil.StringBuilderTxt($"ü�� : {clickedUnit.Health}");
        damageTxt.text = MyUtil.StringBuilderTxt($"���ݷ� : {clickedUnit.Damage}");
        attackSpeedTxt.text = MyUtil.StringBuilderTxt($"���ݼӵ� : {clickedUnit.AttackSpeed}");
        attackRangeTxt.text = MyUtil.StringBuilderTxt($"��Ÿ� : {clickedUnit.AttackRange}");
        moveSpeedTxt.text = MyUtil.StringBuilderTxt($"�̵��ӵ� : {clickedUnit.MoveSpeed}");
        costTxt.text = MyUtil.StringBuilderTxt($"��� : {clickedUnit.Cost}");

        healthSlider.value = clickedUnit.Health / clickedUnit.MaxHealth;
        damageSlider.value = clickedUnit.Damage / clickedUnit.MaxDamage;

    }
}
