using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public float resourceTotal;
    private static PlayerStats _instance;
    public static PlayerStats Instance { get { return _instance; } }
    // Start is called before the first frame update
    void Start()
    {
        // ensure that we destroy this instance if it already exists and isn't this one
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
