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
    }

    // Update is called once per frame
  
}
