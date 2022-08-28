using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Worker : Mover
{
    public float resourceTotal = 0f;
    public float resourceCap = 50f;
    public Building resourceBuilding;
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        canGatherResources = true;
        maxHealth = 100f;
        health = maxHealth;
    }

    // Update is called once per frame

    protected override void Attack(Unit targetUnit)
    {
        base.Attack(targetUnit);
        if (resourceTotal < resourceCap)
        {
            resourceTotal += attackPower;
        } 
        else
        {
            Debug.Log("Worker targetting " + resourceBuilding);
            targetUnit = resourceBuilding;
        }
        
    }

}
