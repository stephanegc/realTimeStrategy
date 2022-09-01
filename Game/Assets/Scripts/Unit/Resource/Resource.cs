using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Resource : Unit
{

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        health = maxHealth;
        maxHealth = 5000f;
        health = maxHealth;
        isSelectable = false;
    }
    void Awake()
    {
        // Keep here to not trigger Unit.Start() and list it in Units
        
    }

    // Update is called once per frame

}
