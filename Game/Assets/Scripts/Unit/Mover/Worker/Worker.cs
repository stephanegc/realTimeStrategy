using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Worker : Mover
{
    // Start is called before the first frame update
    void Start()
    {
        canGatherResources = true;
        maxHealth = 100;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
