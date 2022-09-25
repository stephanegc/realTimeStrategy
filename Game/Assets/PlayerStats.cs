using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerStats : MonoBehaviour
{
    public float resourceTotal;
    [SerializeField] private float resourceTotalPre;
    private static PlayerStats _instance;
    public TextMeshProUGUI resourceText;
    public TextMeshProUGUI unitsSelectedNames;
    private string unitsSelectedNamesNew;
    public static PlayerStats Instance { get { return _instance; } }
    // Start is called before the first frame update
    void Start()
    {
        resourceTotal = 1000;
        resourceText.text = resourceTotal.ToString() + "$";
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

    // Update is called once per frame
    void Update()
    {
        // check if different first, else will always be the same !
        if (resourceTotal != resourceTotalPre)
        {
            resourceText.text = resourceTotal.ToString() + "$";
        }
        resourceTotalPre = resourceTotal;

        unitsSelectedNamesNew = "";
        if (UnitSelections.Instance.unitsSelected.Count > 0)
        {
            foreach (var unit in UnitSelections.Instance.unitsSelected)
            {
                unitsSelectedNamesNew += unit.transform.name + " ";
            }
        }
        if (unitsSelectedNames.text == "" || (unitsSelectedNamesNew != unitsSelectedNames.text))
        {
            unitsSelectedNames.text = unitsSelectedNamesNew;
        }
    }
}
