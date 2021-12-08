using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectMenu : MonoBehaviour
{
    GameObject buttonCollection;
    GameObject activeObject;

    void Start()
    {
        buttonCollection = gameObject.transform.Find("ButtonCollection Hidden").gameObject;
    }

    public void ToggleButtonsVisibility()
    {
        if (!buttonCollection.activeInHierarchy)
        {
            buttonCollection.SetActive(true);
        }
        else
        {
            activeObject.GetComponent<ObjectTrigger>().DisableObjectProperties();
            buttonCollection.SetActive(false);
        }
    }

    public void OnEditButtonPress(string button)
    {
        activeObject.GetComponent<ObjectTrigger>().SetEditMode(button);
    }

    public void SetEditableObject(GameObject editableObject)
    {
        activeObject = editableObject;
    }

    public GameObject GetEditableObject()
    {
        return activeObject;
    }
}
