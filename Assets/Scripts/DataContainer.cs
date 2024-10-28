using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SelectedUnit
{
    public string[] selectedUnitName;

}

public class DataContainer : MonoBehaviour
{
    [field: SerializeField] public UnitData[] UnitDatas { get; private set; }
    [field: SerializeField] public CastleData castleData { get; private set; }
    [field: SerializeField] public SupplyResources supplyResources { get; private set; }

    [HideInInspector] public string[] selectedUnitName;

    private void Awake()
    {
        selectedUnitName = new string[6];
    }
    public SelectedUnit GetSaveData()
    {
        SelectedUnit saveData = new SelectedUnit();
        saveData.selectedUnitName = selectedUnitName;
        return saveData;
    }

}
