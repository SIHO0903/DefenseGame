using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UnitSpawn : MonoBehaviour
{
    [Header("View")]
    [SerializeField] Button[] buttons;
    [SerializeField] TMP_Text supplyTxt;
    [SerializeField] Button supplyUpBtn;

    SelectedUnit unitNames;
    UnitData[] selectedUnits;

    [Header("Model")]
    [SerializeField] SupplyResources supplyResourcesSO;
    float curResources;
    float curSpeed;
    float curCapacity;
    int curCapacityLvl = 1;


    private void Awake()
    {
        curResources = 1000f; //TMP
        JsonLoad();
        Init();
        ResourcesInit();
    }
    private void Update()
    {
        ResourceSupply();
        for (int i = 0; i < buttons.Length; i++)
        {
            buttons[i].interactable = curResources >= selectedUnits[i].Cost;
        }
    }
    private void ResourcesInit()
    {
        curSpeed = supplyResourcesSO.SupplySpeed;
        curCapacity = supplyResourcesSO.SupplyCapacity;

        supplyUpBtn.onClick.AddListener(() =>
        {
            curCapacityLvl++;
            curResources -= curCapacity / 2;
            curCapacity *= 1.2f;

            curCapacity = Mathf.Round(curCapacity);
        });
    }
    private void ResourceSupply()
    {
        if (curResources <= curCapacity)
        {
            curResources += Time.deltaTime * curSpeed;

            supplyTxt.text = string.Format($"{curResources:F0} / {curCapacity}");
        }

        supplyUpBtn.interactable = curCapacity / 2 <= curResources && curCapacityLvl <= 5 ? true : false;
    }

    private void JsonLoad()
    {

        unitNames = new SelectedUnit();
        unitNames = MyUtil.JsonLoad<SelectedUnit>(MyUtil.JsonFileName.SelectedUnitName);
        selectedUnits = new UnitData[6];
        for (int i = 0; i < unitNames.selectedUnitName.Length; i++)
        {
            GameObject unit = PoolManager.Instance.Get(PoolEnum.Friendly, unitNames.selectedUnitName[i], Vector3.zero, Quaternion.identity);
            unit.SetActive(false);
            selectedUnits[i] = unit.GetComponent<IUnitData>().UnitData;
            //Debug.Log("\n name : " + selectedUnits[i].unitName + "\n cost : " + selectedUnits[i].cost);
        }
    }

    private void Init()
    {
        for (int i = 0; i < buttons.Length; i++)
        {

            buttons[i].GetComponentsInChildren<Image>(true)[1].sprite = selectedUnits[i].Image;
            buttons[i].GetComponentInChildren<TMP_Text>(true).text = selectedUnits[i].Cost.ToString();
            int index = i;
            buttons[index].onClick.AddListener(() =>
            {
                SoundManager.Instance.PlaySFX(SoundType.UnitInstansiate);
                Debug.Log("유닛생산버튼클릭");
                CreateUnit(index);
            });
        }
    }

    void CreateUnit(int i)
    {
        //UnitManager.Instance.friendlyUnit.AddUnit(unit);
        if (DecreaseResources(selectedUnits[i].Cost))
        {
            GameObject unit = PoolManager.Instance.Get(PoolEnum.Friendly, unitNames.selectedUnitName[i], MyUtil.SpawnRandomPos(-13f, -14f, -4f, -6f), Quaternion.identity);
            unit.SetActive(true);
        }
    }
    public bool DecreaseResources(float amount)
    {
        if (curResources < amount)
        {
            return false;
        }
        else
        {
            curResources -= amount;
            return true;
        }
    }
}
