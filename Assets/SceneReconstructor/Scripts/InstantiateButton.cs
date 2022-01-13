using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; 
public class InstantiateButton : MonoBehaviour
{
    TextMeshProUGUI labelField;

    void Start()
    {
        labelField = gameObject.transform.Find("Label").GetComponent<TextMeshProUGUI>();
    }

    public void SetLabel(string label)
    {
        labelField.text = label;
    }
}
