using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Worker : Mover
{
    public float resourceTotal = 0f;
    public float resourceCap = 50f;
    public GameObject workerBuilding;
    public Building resourceBuilding;
    public bool isGatheringResources;
    public float distanceToResourceBuilding;
    public Building building;

    protected override void Awake()
    {
        base.Awake();
        resourceCost = 50f;
        canGatherResources = true;
        isGatheringResources = false;
        maxHealth = 100f;
        health = maxHealth;
    }

    // Start is called before the first frame update
    //protected override void Start()
    //{
    //    base.Start();
        
        
    //}

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
            this.player.resourceTotal += resourceTotal;
            resourceTotal = 0f;
        }
        if (isGatheringResources && resourceTotal == 0f && distanceToResourceBuilding <= 1) // return to resource target after having deposited the resources to the resourceBuilding
        {
            targetPosition = targetUnit.transform.position;
        }
        if (Input.GetKeyDown(KeyCode.B) && this.isSelected && this.player.resourceTotal >= building.resourceCost)
        {
            StartCoroutine(Build(building));
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

    IEnumerator Build(Building building)
    {
        Debug.Log("Setting to BUILD");
        GameObject workerBuildingNew = (GameObject)Instantiate(workerBuilding, transform.position, transform.rotation);
        animator.SetTrigger("Build");
        yield return new WaitForSeconds(5f);
        Destroy(workerBuildingNew);
        Building buildingNew = (Building)Instantiate(building, transform.position, building.transform.rotation);
        buildingNew.player = this.player;
        this.player.resourceTotal -= buildingNew.resourceCost;
        animator.SetTrigger("Idle");
    }
}
