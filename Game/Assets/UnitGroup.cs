using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitGroup : MonoBehaviour
{
    public List<Unit> unitList;
    public KeyCode keyCode;
    public string formation;

    // CONSTRUCTOR : this allows to instantiate a UnitGroup with the expected inputs AND return "this" which enables method chaining ! 
    public UnitGroup Init(KeyCode keyCode, List<Unit> unitList)
    {
        this.keyCode = keyCode;
        this.unitList = unitList;
        return this;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
