using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wave : MonoBehaviour
{
    public List<Mover> movers = new List<Mover>();
    public Transform spawnPoint;
    public Transform targetPoint;
    // Start is called before the first frame update
    void Start()
    {
        foreach (Mover mover in movers)
        {
            Mover moverNew = (Mover)Instantiate(mover, spawnPoint.position, spawnPoint.rotation);
            moverNew.targetPosition = targetPoint.position;
        }
        Destroy(gameObject);
    }
}
