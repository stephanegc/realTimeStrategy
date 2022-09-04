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
        resourceCost = 200f;
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
        Debug.Log("UNIT CLASS : " + mover.GetType());
        Mover moverNew = (Mover)Instantiate(mover, transform.Find("SpawnPoint").transform.position, transform.Find("SpawnPoint").transform.rotation);
        moverNew.targetPosition = transform.Find("BannerPoint").transform.position;
        if (moverNew.GetComponent<Worker>() != null)
        {
            Worker worker = moverNew.GetComponent<Worker>();
            worker.resourceBuilding = this;
        }
        PlayerStats.Instance.resourceTotal -= mover.resourceCost;
    }
}
