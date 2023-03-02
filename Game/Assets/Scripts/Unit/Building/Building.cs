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
        maxHealth = 100f;
        health = maxHealth;
        resourceCost = 200f;
    }

    // Update is called once per frame
    protected override void Update()
    {
        if (health <= 0)
        {
            Debug.Log("quitting app !");
            Application.Quit();
        }
        base.Update();
        
        if (Input.GetKeyDown(KeyCode.B) && this.isSelected)
        {
            StartCoroutine(CreateMover());
        }
    }

    public IEnumerator CreateMover()
    {
        yield return new WaitForSeconds(3f);
        Debug.Log("Creating unit at spawnPoint!");
        Mover moverNew = (Mover)Instantiate(mover, transform.Find("SpawnPoint").transform.position, transform.Find("SpawnPoint").transform.rotation);
        moverNew.targetPosition = transform.Find("BannerPoint").transform.position;
        moverNew.player = this.player;
        if (moverNew.GetComponent<Worker>() != null)
        {
            Worker worker = moverNew.GetComponent<Worker>();
            worker.resourceBuilding = this;
        }
        if (targetUnit != null)
        {
            moverNew.targetUnit = targetUnit;
            moverNew.aimingForTargetUnit = true;
        }
        this.player.resourceTotal -= moverNew.resourceCost;
    }
}
