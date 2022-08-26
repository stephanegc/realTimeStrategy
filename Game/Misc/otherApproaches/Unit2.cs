using UnityEngine;

public class Unit2 : MonoBehaviour
{
    void Start()
    {
        foreach (Transform child in transform.GetComponentsInChildren<Transform>())
        {
            UnitSelections.Instance.unitList.Add(child.gameObject);
        }
    }

    void OnDestroy()
    {
        UnitSelections.Instance.unitList.Remove(this.gameObject);
    }


}
