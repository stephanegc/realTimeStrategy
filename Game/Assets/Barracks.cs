using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barracks : MonoBehaviour
{
    public GameObject unit;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.B)) 
        {
            Debug.Log("Creating unit at spawnPoint!");
            GameObject unitNew = (GameObject)Instantiate(unit, transform.Find("SpawnPoint").gameObject.transform.position, transform.Find("SpawnPoint").gameObject.transform.rotation);
        }
    }
}
