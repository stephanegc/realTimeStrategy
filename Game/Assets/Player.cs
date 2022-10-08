using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private CameraController cameraController;
    private UnitSelection unitSelection;
    public List<UnitGroup> unitGroupList = new List<UnitGroup>();
    private 
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        KeyCode keyCode = Input.GetKey
        if (Input.GetKey(KeyCode.Alpha0))
        {
            SetNewUnitGroup(unitSelection, );
        }
    }

    public void SetNewUnitGroup(UnitSelection unitSelection, KeyCode keycode)
    {
        UnitGroup unitGroup = new UnitGroup(keycode, unitSelection.unitsSelected);
        unitGroupList.Add(unitGroup);
        
    }

    public void SelectUnitGroup()
    {

    }
}
