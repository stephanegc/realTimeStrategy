using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    //private CameraController cameraController;
    public UnitSelection unitSelection;
    public List<UnitGroup> unitGroupList;
    public UnitGroup unitGroupPrefab;
    List<KeyCode> groupKeyCodeList = new List<KeyCode> { KeyCode.Alpha0, KeyCode.Alpha1, KeyCode.Alpha2, KeyCode.Alpha3, KeyCode.Alpha4, KeyCode.Alpha5, KeyCode.Alpha6, KeyCode.Alpha7, KeyCode.Alpha8, KeyCode.Alpha9 };
    public float resourceTotal;

    void Awake()
    {

    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        KeyCode groupKeyPressed = GetGroupKeyPressed();
        UnitGroup unitGroupOfGroupKey = GetUnitGroupOfGroupKey(groupKeyPressed);
        if (Input.GetKey(KeyCode.LeftControl) && groupKeyPressed != KeyCode.None)
        {
            SetNewUnitGroup(unitSelection.CloneUnitsSelected(), groupKeyPressed);
        } else if (groupKeyPressed != KeyCode.None && unitGroupOfGroupKey != null)
        {
            SelectUnitGroup(unitGroupOfGroupKey);
        }
    }

    public void SetNewUnitGroup(List<Unit> unitList, KeyCode keyCode)
    {
        unitGroupList.Add(Instantiate(unitGroupPrefab).Init(keyCode, unitList));
    }

    public void SelectUnitGroup(UnitGroup unitGroupOfGroupKey)
    {
        unitSelection.SelectUnits(unitGroupOfGroupKey.unitList);
    }

    public KeyCode GetGroupKeyPressed()
    {
        foreach (KeyCode keyCode in groupKeyCodeList)
        {
            if (Input.GetKeyDown(keyCode))
            {
                return keyCode;
            }
        }
        return KeyCode.None;
    }

    public UnitGroup GetUnitGroupOfGroupKey(KeyCode keyCode)
    {
        foreach (UnitGroup unitGroup in unitGroupList)
        {
            if (unitGroup.keyCode == keyCode)
            {
                return unitGroup;
            }
        }
        return null;
    }
}
