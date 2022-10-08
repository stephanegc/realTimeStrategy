using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private float timeBetweenWaves = 5f;
    private float countDown = 5f;
    //private Wave wave = new Wave;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (countDown <= 0f)
        {
            SpawnWave();
            countDown = timeBetweenWaves;
        }
        countDown -= Time.deltaTime;
        countDown = Mathf.Clamp(countDown, 0f, Mathf.Infinity);
    }

    public void SpawnWave()
    {
        //Wave wave = (Wave)Instantiate(mover, spawnPoint.position, spawnPoint.rotation);
    }
}
