using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Building : Unit
{
    public Mover mover;

    // Start is called before the first frame update
    void Start()
    {
        canCreateUnits = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.B))
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
