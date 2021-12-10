using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Microsoft.MixedReality.Toolkit.UI;

public class ObjectMenu : MonoBehaviour
{
    GameObject selectedObject;
    GameObject editButtonCollection;
   
    InteractableToggleCollection editButtonCollectionSettings;
    Interactable showButton;

    void Start()
    {
        editButtonCollection = gameObject.transform.Find("ButtonCollection Hidden").gameObject;
        editButtonCollectionSettings= editButtonCollection.GetComponent<InteractableToggleCollection>();
        showButton = gameObject.transform.GetChild(0).gameObject.GetComponent<Interactable>();
    }

    // Saves an instance of the object when it is selected to be edited.
    // If an object is already selected before a new object selection deselect it before replacement.
    // Also hide the edit mode buttons and reset their values.
    public void SetEditableObject(GameObject incomingObject)
    {
        if (selectedObject)
        {
            showButton.IsToggled = false;
            editButtonCollection.SetActive(false);
            selectedObject.GetComponent<ObjectTrigger>().OnObjectFocusOff();
        }
        
        selectedObject = incomingObject;
    }

    // Shows or hides the edit mode buttons and set its index back to 0 (first edit button).
    // Reset the edit mode properties of the object's bounding box.
    public void ToggleEditButtonsVisibility()
    {
        if (!editButtonCollection.activeInHierarchy)
        {
            editButtonCollection.SetActive(true);
            editButtonCollectionSettings.CurrentIndex = 0;
        }
        else
        {
            selectedObject.GetComponent<ObjectTrigger>().DisableEditProperties();
            editButtonCollection.SetActive(false);
        }
    }

    // Sets the edit mode (movement, scale, rotation) to the selected object.
    public void OnEditButtonPressed(string button)
    {
        selectedObject.GetComponent<ObjectTrigger>().SetEditMode(button);
    }
}
