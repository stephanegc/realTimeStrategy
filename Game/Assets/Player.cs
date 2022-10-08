using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    //private CameraController cameraController;
    public List<UnitGroup> unitGroupList = new List<UnitGroup>();
    public UnitGroup unitGroupPrefab;
    List<KeyCode> groupKeyCodeList = new List<KeyCode> { KeyCode.Alpha0, KeyCode.Alpha1, KeyCode.Alpha2, KeyCode.Alpha3, KeyCode.Alpha4, KeyCode.Alpha5, KeyCode.Alpha6, KeyCode.Alpha7, KeyCode.Alpha8, KeyCode.Alpha9 };

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
        if (Input.GetKey(KeyCode.LeftControl) && groupKeyPressed != KeyCode.None)
        {
            SetNewUnitGroup(UnitSelection.Instance.unitsSelected, groupKeyPressed);
        } else if (groupKeyPressed != KeyCode.None)
        {
            SelectUnitGroup(groupKeyPressed);
        }
    }

    public void SetNewUnitGroup(List<Unit> unitList, KeyCode keycode)
    {
        //this.gameObject.AddComponent
        UnitGroup unitGroupNew = (UnitGroup)Instantiate(unitGroupPrefab);
        unitGroupNew.keyCode = keycode;
        unitGroupNew.unitList = unitList;
        unitGroupList.Add(unitGroupNew);
        Debug.Log("unitGroupNew" + unitGroupNew.unitList.Count);
    }

    public void SelectUnitGroup(KeyCode keyCode)
    {
        UnitGroup unitGroupToSelect = unitGroupList.Find(i => i.keyCode == keyCode);
        Debug.Log("unitGroupToSelect" + unitGroupToSelect.unitList);
        Debug.Log("unitGroupToSelect" + unitGroupToSelect.unitList.Count);
        UnitSelection.Instance.SelectUnits(unitGroupToSelect.unitList);
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
}