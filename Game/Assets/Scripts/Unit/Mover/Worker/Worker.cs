using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Worker : Mover
{
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        canGatherResources = true;
        maxHealth = 100f;
    }

    // Update is called once per frame
    

}
