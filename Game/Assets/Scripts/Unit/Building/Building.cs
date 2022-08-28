using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Building : Unit
{
    public Mover unit;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            CreateUnit(unit);
        }
    }

    void CreateUnit(Mover unit)
    {
        Debug.Log("Creating unit at spawnPoint!");
        Mover unitNew = (Mover)Instantiate(unit, transform.Find("SpawnPoint").transform.position, transform.Find("SpawnPoint").transform.rotation);
        NavMeshAgent myAgent = unitNew.GetComponent<NavMeshAgent>();
        unitNew.targetPosition = transform.Find("BannerPoint").transform.position;
        unitNew.Move();
    }
}
