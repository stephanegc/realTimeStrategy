using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Resource : Unit
{

    // Start is called before the first frame update
    void Awake()
    {
        // Keep here to not trigger Unit.Start() and list it in Units
        maxHealth = 5000f;
        health = maxHealth;
    }

    // Update is called once per frame

}
