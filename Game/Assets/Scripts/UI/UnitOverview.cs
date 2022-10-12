using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UnitOverview : MonoBehaviour
{
    public Player player;
    public TextMeshProUGUI unitsSelectedNames;
    private string unitsSelectedNamesNew;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        unitsSelectedNamesNew = "";
        if (player.unitSelection.unitsSelected.Count > 0)
        {
            foreach (var unit in player.unitSelection.unitsSelected)
            {
                unitsSelectedNamesNew += unit.transform.name + " ";
            }
        }
        if (unitsSelectedNames.text == "" || (unitsSelectedNamesNew != unitsSelectedNames.text)) // check empty to ensure it is updated at start 
        {
            unitsSelectedNames.text = unitsSelectedNamesNew;
        }
    }
}
