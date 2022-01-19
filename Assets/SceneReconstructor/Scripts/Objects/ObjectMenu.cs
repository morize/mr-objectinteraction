using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Microsoft.MixedReality.Toolkit.UI;

public class ObjectMenu : MonoBehaviour
{
    [SerializeField]
    private TracesMenu tracesMenu;

    private InteractableToggleCollection objectMenu;
    private ObjectFeatures selectedObject;
    private GameObject hiddenButtons;

    private bool isEditModeEnabled = true;

    void Start()
    {
        objectMenu = gameObject.GetComponent<InteractableToggleCollection>();
        hiddenButtons = gameObject.transform.Find("Object Hidden Buttons").gameObject;
    }

    public void OnObjectTriggered(ObjectFeatures incomingObject)
    {
        if (isEditModeEnabled)
        {
            if (!gameObject.activeInHierarchy) gameObject.SetActive(true);
            if (incomingObject == selectedObject) return;

            if (selectedObject)
            {
                tracesMenu.CloseTracesWindow();
                selectedObject.OnObjectFocusOff();
                objectMenu.CurrentIndex = 0;
            }
        }

        else
        {
            OnTracesInfoButtonPressed();
        }

        selectedObject = incomingObject;
    }

    public void OnEditButtonPressed(string buttonMode)
    {
        selectedObject.OnEditButtonPressed(buttonMode);
    }

    public void OnMoreButtonPressed()
    {
        if (!hiddenButtons.activeInHierarchy)
        {
            hiddenButtons.SetActive(true);
        }

        else
        {
            hiddenButtons.SetActive(false);
        }

        objectMenu.CurrentIndex = 0;
    }

    public void OnDeleteButtonPressed()
    {
        if (tracesMenu.isActiveAndEnabled) tracesMenu.CloseTracesWindow();
        
        if(objectMenu) objectMenu.CurrentIndex = 0;
        selectedObject = default;
        gameObject.SetActive(false);
    }

    public void OnTracesInfoButtonPressed()
    {
        tracesMenu.LoadTraceWindow(isEditModeEnabled);
        tracesMenu.LoadTraceInfo(selectedObject.GetTraceInfo());

    }

    public void AlignMenuWithObject(Bounds objectBounds)
    {
        gameObject.transform.position = new Vector3(objectBounds.max.x, objectBounds.min.y + 0.2f, objectBounds.min.z + -0.14f);
    }

    public void ToggleEditMode()
    {
        if (!isEditModeEnabled)
        {
            isEditModeEnabled = true;

            if (selectedObject)
            {
                tracesMenu.LoadTraceWindow(isEditModeEnabled);
                tracesMenu.LoadTraceInfo(selectedObject.GetTraceInfo());
            }
        }

        else
        {
            isEditModeEnabled = false;

            if (selectedObject)
            {
                tracesMenu.LoadTraceWindow(isEditModeEnabled);
                tracesMenu.LoadTraceInfo(selectedObject.GetTraceInfo());
                selectedObject.OnObjectFocusOff();
                OnDeleteButtonPressed();
            }
        }
    }

    // Should not be in this script but in TracesManager. Stay for now...
    public void SetTraceInfo(string trace)
    {
        Trace newTrace = tracesMenu.SetTraceInfo(trace);
        selectedObject.SetTraceInfo(newTrace);
    }

    public bool GetIsEditModeEnabled()
    {
        return isEditModeEnabled;
    }
}
