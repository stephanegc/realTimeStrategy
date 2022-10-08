using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UnitActions : MonoBehaviour
{
    [SerializeField] Button buttonPrefab;
    public TMP_Text buttonPrefabText;
    public GameObject gridLayout;
    // Start is called before the first frame update
    void Start()
    {
        Button button = (Button)Instantiate(buttonPrefab, gridLayout.transform);
        buttonPrefabText = button.GetComponentInChildren<TMP_Text>(true);
        buttonPrefabText.text = "TRIAL";
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
