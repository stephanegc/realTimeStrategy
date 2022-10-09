using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private float timeBetweenWaves = 60f;
    public float countDown = 5f;
    public Mover mover;
    private int numberOfMovers = 1;
    public int waveCount = 1;
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
            numberOfMovers = numberOfMovers * waveCount;
            SpawnWave();
            countDown = timeBetweenWaves;
        }
        countDown -= Time.deltaTime;
        countDown = Mathf.Clamp(countDown, 0f, Mathf.Infinity);
    }

    public void SpawnWave()
    {
        for (int i = 0; i < numberOfMovers; i++)
        {
            Mover moverNew = (Mover)Instantiate(mover, transform.Find("SpawnPoint").transform.position, transform.Find("SpawnPoint").transform.rotation);
            moverNew.targetPosition = transform.Find("TargetPoint").transform.position;
        }
        waveCount += 1;
    }
}
