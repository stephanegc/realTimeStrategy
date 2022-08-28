using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Unit : MonoBehaviour
{
    public float maxHealth;
    public float health;
    public bool isSelected = false;
    public bool canMove = false;
    public bool canCreateUnits = false;
    public bool canAttack = false;
    public bool canGatherResources = false;

    public Unit targetUnit;
    public float distanceToTargetUnit;
    public float attackSpeed = 2f;
    public float attackRange = 2f;
    public float attackCountDown = 0f;
    public float attackPower;

    protected virtual void Start()
    {
        //add this unit to the list
        UnitSelections.Instance.unitList.Add(this);
        health = maxHealth;
    }

    protected virtual void OnDestroy()
    {
        //remove it from the list when destroyerd
        UnitSelections.Instance.unitList.Remove(this);
    }

    protected virtual void Update()
    {
        //Debug.Log("teeeeeest");
        if (targetUnit != null)
        {
            distanceToTargetUnit = Vector3.Distance(transform.position, targetUnit.transform.position);
        }
        if (targetUnit != null && distanceToTargetUnit <= attackRange && attackCountDown <= 0)
        {
            Debug.Log("TEEEEEEEEST");
            Attack(targetUnit);
        }
        if (attackCountDown <= 0)
        {
            attackCountDown = attackSpeed;
        }
        attackCountDown -= Time.deltaTime;
    }

    void Attack(Unit targetUnit)
    {
        Debug.Log("Attacking: " + targetUnit);
        targetUnit.health -= attackPower;
    }
}



