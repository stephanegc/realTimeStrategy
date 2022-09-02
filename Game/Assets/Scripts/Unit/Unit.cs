using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Unit : MonoBehaviour
{
    public float maxHealth;
    public float health;
    public bool isSelectable = true;
    public bool isSelected = false;
    public bool canMove = false;
    public bool canCreateUnits = false;
    public bool canAttack = false;
    public bool canGatherResources = false;
    public bool canAttackNonResourcesUnits = false;

    public Unit targetUnit;
    public float distanceToTargetUnit;
    public float distanceToTargetUnitPosition;
    public float attackSpeed = 2f;
    public float attackRange = 2f;
    public float attackCountDown = 0f;
    public float attackPower;
    public Animator animator;

    public Vector3 targetPosition;

    protected virtual void Start()
    {
        //add this unit to the list
        UnitSelections.Instance.unitList.Add(this);
        health = maxHealth;
        isSelectable = true;
        animator = GetComponent<Animator>();
        distanceToTargetUnit = Mathf.Infinity;
        targetPosition = transform.position;
    }

    protected virtual void OnDestroy()
    {
        //remove it from the list when destroyerd
        UnitSelections.Instance.unitList.Remove(this);
    }

    protected virtual void Update()
    {
        if (health == 0)
        {
            Debug.Log("Setting to DIE");
            animator.SetTrigger("Die");
            Destroy(gameObject);
            return;
        }
        //Debug.Log("teeeeeest");
        if (targetUnit != null)
        {
            distanceToTargetUnit = Vector3.Distance(transform.position, targetUnit.transform.position);
            distanceToTargetUnitPosition = Vector3.Distance(targetPosition, targetUnit.transform.position);
        }
        if (targetUnit != null && distanceToTargetUnit <= attackRange && attackCountDown <= 0 && distanceToTargetUnitPosition <= 1.5 && CanAttackTargetUnit())
        {
            Attack(targetUnit);
        }
        if (attackCountDown <= 0)
        {
            attackCountDown = attackSpeed;
        }
        attackCountDown -= Time.deltaTime;
    }

    private bool CanAttackTargetUnit()
    {
        if (targetUnit == null)
        {
            return false;
        }
        var check = (targetUnit.GetComponent<Resource>() != null && canGatherResources) || (targetUnit.GetComponent<Unit>() != null && targetUnit.GetComponent<Resource>() == null && canAttackNonResourcesUnits);
        return check;
    }

    protected virtual void Attack(Unit targetUnit)
    {
        Debug.Log(this + " ATTACKING " + targetUnit);
        targetUnit.health -= attackPower;
    }
}



