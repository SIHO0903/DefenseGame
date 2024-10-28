using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnitSelectUI : BaseFacility
{
    [Header("View")]
    [SerializeField] GameObject grid;
    [SerializeField] GameObject selected;
    [SerializeField] Button exitBtn;

    [Header("Model")]
    [SerializeField] DataContainer unitDatasContainer;   
    [SerializeField] GameObject buttonPrefab;

    Image[] selectedImage;
    HashSet<string> selectedUnitNamesSet;
    public override void Awake()
    {
        base.Awake();
        SelectedUI();
        selectedUnitNamesSet = new HashSet<string>();

        UnitsUI();
        exitBtn.onClick.AddListener(() =>
        {
            SoundManager.Instance.PlaySFX(SoundType.ClickBtn);
            HideUI();
        });

    }
    // ���õ� ������ ǥ���ϴ� UI�� �ʱ�ȭ
    private void SelectedUI()
    {

        for (int i = 0; i < unitDatasContainer.selectedUnitName.Length; i++)
        {
            GameObject character = Instantiate(buttonPrefab, selected.transform);
            character.transform.GetChild(0).GetComponent<RectTransform>().sizeDelta = new Vector2(130, 130);
            character.transform.GetChild(1).GetComponent<RectTransform>().sizeDelta = new Vector2(120, 120);
            Button button = character.GetComponent<Button>();
            int index = i;
            button.onClick.AddListener(() =>
            {
                SoundManager.Instance.PlaySFX(SoundType.ClickBtn);
                DeselectUnit(button, index);
            });
        }
        selectedImage = new Image[unitDatasContainer.selectedUnitName.Length];

        for (int i = 0; i < unitDatasContainer.selectedUnitName.Length; i++)
        {
            selectedImage[i] = selected.transform.GetChild(i).GetChild(1).GetComponent<Image>();
        }
    }
    // ���� ������ ���� ����Ʈ�� ǥ���ϴ� UI�� �ʱ�ȭ
    private void UnitsUI()
    {
        for (int i = 0; i < unitDatasContainer.UnitDatas.Length; i++)
        {
            GameObject character = Instantiate(buttonPrefab, grid.transform);
            character.name = unitDatasContainer.UnitDatas[i].UnitName;
            character.GetComponentsInChildren<Image>()[2].sprite = unitDatasContainer.UnitDatas[i].Image;
            Button button = character.GetComponent<Button>();
            int index = i;
            button.onClick.AddListener(() =>
            {
                SoundManager.Instance.PlaySFX(SoundType.ClickBtn);
                SortSelected(unitDatasContainer.UnitDatas[index]);
            });
        }
    }
    // ���� ����, �� ���Կ� ��ġ
    void SortSelected(UnitData unitData)
    {
        if (selectedUnitNamesSet.Contains(unitData.UnitName))
        {
            return;
        }

        // ���� ���� �� �� �ڸ��� ã�� ������ ��ġ
        for (int i = 0; i < selectedImage.Length; i++)
        {
            if (selectedImage[i].sprite == null)
            {
                unitDatasContainer.selectedUnitName[i] = unitData.UnitName;
                selectedImage[i].sprite = unitData.Image;
                selectedUnitNamesSet.Add(unitData.UnitName); // HashSet�� ���� �̸� �߰�
                break;
            }
        }

    }
    // ���� ���� ����, ���Կ��� ����
    void DeselectUnit(Button button,int index)
    {
        string unitName = unitDatasContainer.selectedUnitName[index];
        if (unitName != null)
        {
            selectedUnitNamesSet.Remove(unitName); // HashSet���� ���� �̸� ����
            unitDatasContainer.selectedUnitName[index] = null;
        }

        button.transform.GetChild(1).GetComponent<Image>().sprite = null;

    }
    public override void HideUI()
    {
        base.HideUI();
        MyUtil.JsonSave(unitDatasContainer.GetSaveData(),MyUtil.JsonFileName.SelectedUnitName);
        Debug.Log("â����");
    }
}
