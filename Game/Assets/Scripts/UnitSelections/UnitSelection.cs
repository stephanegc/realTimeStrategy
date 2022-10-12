using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class UnitSelection : MonoBehaviour
{
    public List<Unit> unitList = new List<Unit>();
    public List<Unit> unitsSelected = new List<Unit>();
    public List<Vector3> unitsSelectedPositions = new List<Vector3>();
    public float distanceBetweenUnits = 2f;

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

    public void DoubleClickSelect(Unit unitToAdd)
    { 
        //TODO
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
        //TODO
    }

    public void SelectUnits(List<Unit> unitList)
    {
        DeselectAll();
        foreach (Unit unit in unitList)
        {
            Debug.Log("Selecting :" + unit.ToString());
            unitsSelected.Add(unit);
            unit.transform.Find("SelectionHighlight").gameObject.SetActive(true);
            unit.isSelected = true;
        }
    }

    public void SetUnitsSelectedPositions(Vector3 hitPosition)
    {
        // Aim is to keep center relative to mouse both when odd (middle unit at center) and even (one unit at half distance of center on each side)
        // Example ODD (1 unit : 0); (3 units: -1 0 +1); (5 units: -2 -1 0 +1 +2) %%% EVEN (2 units : -0.5 +0.5); (4 units : -1.5 -0.5 +0.5 +1.5)
        List<Vector3> positionList = new List<Vector3>();
        int positionCount = unitsSelected.Count;
        var isEven = (positionCount % 2) == 0;
        int startIndex;
        float modifier;
        Vector3 centerPosition;

        if (isEven)
        {
            startIndex = positionCount / 2 * -1;  // cannot make it float ! ...
            int middleIndexBefore = (int)Mathf.Floor(positionCount / 2) - 1;
            int middleIndexAfter = (int)Mathf.Floor(positionCount / 2);
            centerPosition = unitsSelected[middleIndexAfter].transform.position + (unitsSelected[middleIndexBefore].transform.position - unitsSelected[middleIndexAfter].transform.position) / 2;
        }
        else
        {
            startIndex = (positionCount - 1) / 2 * -1;
            centerPosition = unitsSelected[positionCount / 2].transform.position;
        }
        var toUnitPosition = centerPosition - hitPosition; // this is the vector FROM the hitPosition TO the centerPosition of the units
        var crossVector = Vector3.Cross(toUnitPosition, Vector3.up).normalized; // this yields the vector perpendicular to both the vector going from the hitPosition to the centerPosition AND the vector going up, normalized back to 1 (else it is as long as toUnitPosition !)
        //Debug.Log("toUnitPosition : " + toUnitPosition);
        Debug.Log("crossVector : " + crossVector);
        //var check = crossVector.x < 0 ;
        if (crossVector.x < 0 || crossVector.z < 0)
        {
            crossVector = crossVector * -1;
            Debug.Log("crossVector MODIF : " + crossVector);
        }

        IEnumerable<int> positionIndexes = Enumerable.Range(startIndex, positionCount);
        for (int i = 0; i < positionCount; i++)
        {
            modifier = (float)positionIndexes.ElementAt(i); // ... so we make it float after the fact !
            if (isEven)
            {
                modifier += 0.5f;
            }
            var pos = hitPosition + crossVector * distanceBetweenUnits * modifier;
            positionList.Add(pos);
        }
        unitsSelectedPositions = positionList;
    }

    public void ApplyUnitsSelectedPosition()
    {
        int targetPositionListIndex = 0;

        foreach (var unit in unitsSelected)
        {
            if (unit.GetComponent<Mover>() != null)
            {
                Mover mover = unit.GetComponent<Mover>();
                mover.targetPosition = unitsSelectedPositions[targetPositionListIndex];
                mover.aimingForTargetUnit = false;
                targetPositionListIndex += 1;
            }
        }
    }

    public void SetUnitsSelectedTarget(Unit hitUnit)
    {
        foreach (Unit unit in unitsSelected)
        {
            unit.SetTargetUnit(hitUnit); 
            if (unit.GetComponent<Mover>() != null)
            {
                Mover mover = unit.GetComponent<Mover>();
                mover.targetPosition = hitUnit.transform.position;
                mover.aimingForTargetUnit = true;
            }
        }
    }

    // Necessary to be able to create UnitGroups that are PERSISTENT and not constantly changed to what the unitsSelected is atm
    public List<Unit> CloneUnitsSelected()
    {
        return new List<Unit>(unitsSelected);
    }
}
