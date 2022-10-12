using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ResourceOverview : MonoBehaviour
{
    public Player player;
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
        if (player.resourceTotal != resourceTotalPre)
        {
            resourceText.text = player.resourceTotal.ToString() + "$";
        }
        resourceTotalPre = player.resourceTotal;
    }
}
