using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour
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

            //Vector3 dir = unitNew.transform.position - transform.Find("BannerPoint").gameObject.transform.position;
            //float distanceThisFrame = 70f * Time.deltaTime;
            //transform.Translate(dir.normalized * distanceThisFrame, Space.World);

            //unitNew.GetComponent<NavMeshAgent>().SetDestination(hit.point); 
        }
    }
}
