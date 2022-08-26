using UnityEngine;

public class Unit : MonoBehaviour
{
    private Animation animation;
    void Start()
    {
        //foreach (Transform child in transform.GetComponentsInChildren<Transform>())
        //{
        //    UnitSelections.Instance.unitList.Add(child.gameObject);
        //}
        UnitSelections.Instance.unitList.Add(this.gameObject);
        //animation = GetComponent<Animation>();
        //animation.Play("WK_heavy_infantry_05_combat_idle");
    }

    void OnDestroy()
    {
        UnitSelections.Instance.unitList.Remove(this.gameObject);
    }


}
