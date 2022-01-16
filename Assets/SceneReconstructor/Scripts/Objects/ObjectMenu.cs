using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Microsoft.MixedReality.Toolkit.UI;

public class ObjectMenu : MonoBehaviour
{
    [SerializeField]
    TracesManager tracesWindow;

    InteractableToggleCollection objectMenuSettings;
    Interactable showButton;
    GameObject hiddenButtons;
    GameObject selectedObject;

    void Start()
    {
        objectMenuSettings = gameObject.GetComponent<InteractableToggleCollection>();
        showButton = gameObject.transform.Find("Object Button Show").gameObject.GetComponent<Interactable>();
        hiddenButtons = showButton.transform.parent.Find("Object Hidden Buttons").gameObject;
    }

    // Saves an instance of the object when it is selected to be edited.
    // If an object is already selected before a new object selection deselect it before replacement.
    // Also hide the edit mode buttons and reset their values.
    public void SetEditableObject(GameObject incomingObject)
    {
        if (selectedObject)
        {
            showButton.IsToggled = false;
            selectedObject.GetComponent<ObjectFeatures>().OnObjectFocusOff();
            objectMenuSettings.CurrentIndex = 0;
            selectedObject = incomingObject;
        }
        else
        {
            gameObject.SetActive(true);
            selectedObject = incomingObject;
        }

        UpdateMenuPosition(incomingObject.GetComponent<Renderer>().bounds);
    }

    // Shows or hides the edit mode buttons and set its index back to 0 (first edit button).
    // Reset the edit mode properties of the object's bounding box.
    public void ToggleMoreButtonsVisibility()
    {
        if (!hiddenButtons.activeInHierarchy)
        {
            hiddenButtons.SetActive(true);
            objectMenuSettings.CurrentIndex = 0;
        }
        else
        {
            selectedObject.GetComponent<ObjectFeatures>().DisableEditProperties();
            objectMenuSettings.CurrentIndex = 0;
            hiddenButtons.SetActive(false);
        }
    }

    // Sets the edit mode (movement, scale, rotation) to the selected object.
    public void OnEditButtonPressed(string button)
    {
        selectedObject.GetComponent<ObjectFeatures>().SetEditMode(button);
    }

    public void OnObjectDeleted()
    {
        hiddenButtons.SetActive(false);
        selectedObject = null;
        objectMenuSettings.CurrentIndex = 0;
        gameObject.SetActive(false);
    }

    public void UpdateMenuPosition(Bounds objectBounds)
    {
        gameObject.transform.position = new Vector3(objectBounds.max.x, objectBounds.min.y + 0.2f, objectBounds.min.z + -0.14f);
    }

    public void OpenTracesWindow()
    {
        tracesWindow.OpenTracesWindow();
    }

    public void SetTraceInfo(string trace)
    {
        Trace newTrace = tracesWindow.SetTraceInfo(trace);
        selectedObject.GetComponent<ObjectFeatures>().SetTraceInfo(newTrace);
    }
}
