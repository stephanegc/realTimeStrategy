using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Soldier : Mover
{
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        maxHealth = 500f;
        health = maxHealth;
        attackSpeed = 2.5f;
}

    // Update is called once per frame
    protected override void Attack(Unit targetUnit)
    {
        base.Attack(targetUnit);
        if (!animator.GetCurrentAnimatorStateInfo(0).IsName("Attack"))
        {
            Debug.Log("Setting to ATTACK");
            animator.SetTrigger("Attack");
        }
    }
}
