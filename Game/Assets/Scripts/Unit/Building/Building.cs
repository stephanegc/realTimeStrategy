using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Building : Unit
{
    public Mover mover;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        canCreateUnits = true;
        maxHealth = 1000f;
        health = maxHealth;
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        if (Input.GetKeyDown(KeyCode.B) && this.isSelected)
        {
            CreateMover(mover);
        }
    }

    void CreateMover(Mover mover)
    {
        Debug.Log("Creating unit at spawnPoint!");
        Mover moverNew = (Mover)Instantiate(mover, transform.Find("SpawnPoint").transform.position, transform.Find("SpawnPoint").transform.rotation);
        NavMeshAgent myAgent = moverNew.GetComponent<NavMeshAgent>();
        moverNew.targetPosition = transform.Find("BannerPoint").transform.position;
        moverNew.Move();
    }
}
