using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Microsoft.MixedReality.Toolkit.UI;

public class InstantiateButton : MonoBehaviour
{
    public void SetLabel(string objectName)
    {
        gameObject.transform.Find("Label").GetComponent<TextMeshProUGUI>().text = objectName;
    }

    public void SetInstantiateButtonEvent(string objectName, Transform parent)
    {
        gameObject.GetComponent<Interactable>().OnClick.AddListener(()=>
        {
            InstantiateManager.InstatiateObject(objectName, parent);
        });
    }
}
