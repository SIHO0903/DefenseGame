using System.Collections.Generic;
using UnityEngine;

public class UnitManager : MonoBehaviour
{
    public UnitList friendlyUnit = new UnitList();
    public UnitList EnemyUnit = new UnitList();

}
public class UnitList
{
    public List<GameObject> unitL = new List<GameObject>();

    public void AddUnit(GameObject unit)
    {
        unitL.Add(unit);
    }
    public void SubUnit(GameObject unit)
    {
        unitL.Remove(unit);
    }
}