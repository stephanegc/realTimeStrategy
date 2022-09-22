using System.Collections.Generic;
using UnityEngine;

public class UnitSelections : MonoBehaviour
{
    public List<Unit> unitList = new List<Unit>();
    public List<Unit> unitsSelected = new List<Unit>();

    private static UnitSelections _instance;
    public static UnitSelections Instance { get { return _instance; } }

    private void Awake()
    {
        // ensure that we destroy this instance if it already exists and isn't this one
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }


    public void ClickSelect(Unit unitToAdd)
    {
        DeselectAll();
        unitsSelected.Add(unitToAdd);
        unitToAdd.transform.Find("SelectionHighlight").gameObject.SetActive(true);
        unitToAdd.isSelected = true;
    }

    public void ShiftClickSelect(Unit unitToAdd)
    {
        if (!unitsSelected.Contains(unitToAdd))
        {
            unitsSelected.Add(unitToAdd);
            unitToAdd.transform.Find("SelectionHighlight").gameObject.SetActive(true);
            unitToAdd.isSelected = true;
        }
        else
        {
            unitToAdd.transform.Find("SelectionHighlight").gameObject.SetActive(false);
            unitsSelected.Remove(unitToAdd);
            unitToAdd.isSelected = false;
        }
    }

    public void DragSelect(Unit unitToAdd)
    {
        if (!unitsSelected.Contains(unitToAdd))
        {
            unitsSelected.Add(unitToAdd);
            unitToAdd.transform.Find("SelectionHighlight").gameObject.SetActive(true);
            unitToAdd.isSelected = true;
        }
    }

    public void DeselectAll()
    {        
        foreach (var unit in unitsSelected)
        {
            unit.transform.Find("SelectionHighlight").gameObject.SetActive(false);
            unit.isSelected = false;
        }
        unitsSelected.Clear();
    }

    
    public void Deselect(Unit unitToDeselect)
    {

    }
}
