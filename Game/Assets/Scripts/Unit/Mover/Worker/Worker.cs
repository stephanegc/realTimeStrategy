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
        myAgent.acceleration = 100f;
        myAgent.speed = 8f;
        myAgent.angularSpeed = 10000f;
    }

    // Update is called once per frame

    protected override void Attack(Unit targetUnit)
    {
        base.Attack(targetUnit);
        if (resourceTotal < resourceCap)
        {
            if (!animator.GetCurrentAnimatorStateInfo(0).IsName("Mine"))
            {
                animator.SetTrigger("Mine");
            }
            resourceTotal += attackPower;
        } 
        else
        {
            animator.SetTrigger("Idle");
            Debug.Log("Worker targetting " + resourceBuilding);
            targetUnit = resourceBuilding;
        }
        
    }

}
