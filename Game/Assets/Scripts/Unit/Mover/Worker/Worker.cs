using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Worker : Mover
{
    public float resourceTotal = 0f;
    public float resourceCap = 50f;
    public Building resourceBuilding;
    public bool isGatheringResources;
    public float distanceToResourceBuilding;
    public Building building;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        canGatherResources = true;
        isGatheringResources = false;
        maxHealth = 100f;
        health = maxHealth;
        resourceCost = 50f;
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        distanceToResourceBuilding = Vector3.Distance(transform.position, resourceBuilding.transform.position);
        if (targetUnit != null)
        {
            isGatheringResources = true;
        }
        else
        {
            isGatheringResources = false;
        }
        if (distanceToResourceBuilding <= 1)
        {
            PlayerStats.Instance.resourceTotal += resourceTotal;
            resourceTotal = 0f;
        }
        if (isGatheringResources && resourceTotal == 0f && distanceToResourceBuilding <= 1) // return to resource target after having deposited the resources to the resourceBuilding
        {
            targetPosition = targetUnit.transform.position;
        }
        if (Input.GetKeyDown(KeyCode.B) && this.isSelected && PlayerStats.Instance.resourceTotal >= building.resourceCost)
        {
            Build(building);
        }
    }

    protected override void Attack(Unit targetUnit)
    {
        base.Attack(targetUnit);
        if (resourceTotal < resourceCap)
        {
            if (!animator.GetCurrentAnimatorStateInfo(0).IsName("Mine"))
            {
                Debug.Log("Setting to MINE");
                animator.SetTrigger("Mine");
            }
            resourceTotal += attackPower;
        } 
        else
        {
            aimingForTargetUnit = false;
            targetPosition = resourceBuilding.transform.position;
        }
        
    }

    private void Build(Building building)
    {
        Building buildingNew = (Building)Instantiate(building, transform.position, building.transform.rotation);
        PlayerStats.Instance.resourceTotal -= building.resourceCost;
    }
}
