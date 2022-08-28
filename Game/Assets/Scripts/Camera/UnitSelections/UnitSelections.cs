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
        Debug.Log("Adding unit to the list");
        unitsSelected.Add(unitToAdd);
        unitToAdd.transform.Find("SelectionHighlight").gameObject.SetActive(true);
    }

    public void ShiftClickSelect(Unit unitToAdd)
    {
        if (!unitsSelected.Contains(unitToAdd))
        {
            unitsSelected.Add(unitToAdd);
            unitToAdd.transform.Find("SelectionHighlight").gameObject.SetActive(true);
        }
        else
        {
            unitToAdd.transform.Find("SelectionHighlight").gameObject.SetActive(false);
            unitsSelected.Remove(unitToAdd);
        }
    }

    public void DragSelect(Unit unitToAdd)
    {
        if (!unitsSelected.Contains(unitToAdd))
        {
            Debug.Log("Adding selected unit via drag to unitList and setting graphics to active");
            unitsSelected.Add(unitToAdd);
            unitToAdd.transform.Find("SelectionHighlight").gameObject.SetActive(true);
        }
    }

    public void DeselectAll()
    {
        Debug.Log("Removing all selected units from the list");
        
        foreach (var unit in unitsSelected)
        {
            unit.transform.Find("SelectionHighlight").gameObject.SetActive(false);
        }
        unitsSelected.Clear();
    }

    
    public void Deselect(Unit unitToDeselect)
    {

    }
}
