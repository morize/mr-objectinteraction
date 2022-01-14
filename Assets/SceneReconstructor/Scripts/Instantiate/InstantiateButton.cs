using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Microsoft.MixedReality.Toolkit.UI;

public class InstantiateButton : MonoBehaviour
{
    Transform crimeScene;
    public void SetLabel(string objectName)
    {
        gameObject.transform.Find("Label").GetComponent<TextMeshProUGUI>().text = objectName;
    }

    public void SetButtonEvent(string objectName, Transform parent)
    {
        Interactable interactable = gameObject.GetComponent<Interactable>();
        interactable.OnClick.AddListener(delegate { InstantiateMenuObject(objectName, parent); });

    }


    public void InstantiateMenuObject(string objectName, Transform parent)
    {
        InstantiateManager.InstatiateObjects(objectName, parent);
    }
}
