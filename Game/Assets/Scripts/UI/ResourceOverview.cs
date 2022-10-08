using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ResourceOverview : MonoBehaviour
{
    public TextMeshProUGUI resourceText;
    [SerializeField] private float resourceTotalPre;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // check if different first, else will always be the same !
        if (PlayerStats.Instance.resourceTotal != resourceTotalPre)
        {
            resourceText.text = PlayerStats.Instance.resourceTotal.ToString() + "$";
        }
        resourceTotalPre = PlayerStats.Instance.resourceTotal;
    }
}
