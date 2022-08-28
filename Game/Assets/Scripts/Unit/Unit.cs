using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Unit : MonoBehaviour
{
    private int maxHealth;
    public int health;
    public bool isSelected = false;
    public bool canMove = false;
    public bool canCreateUnits = false;
    public bool canAttack = false;

    void Start()
    {
        //add this unit to the list
        UnitSelections.Instance.unitList.Add(this.gameObject);
    }

    void OnDestroy()
    {
        //remove it from the list when destroyerd
        UnitSelections.Instance.unitList.Remove(this.gameObject);
    }

    
}



