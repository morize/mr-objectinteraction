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

    private bool isEditable = true;

    void Start()
    {
        objectMenu = gameObject.GetComponent<InteractableToggleCollection>();
        hiddenButtons = gameObject.transform.Find("Object Hidden Buttons").gameObject;
    }

    public void OnObjectTriggered(ObjectFeatures incomingObject)
    {
        if (!gameObject.activeInHierarchy) 
        {
            gameObject.SetActive(true);
        }

        if (selectedObject)
        {
            tracesMenu.CloseTracesWindow();
            selectedObject.OnObjectFocusOff();
            objectMenu.CurrentIndex = 0;
        }

        selectedObject = incomingObject;

        UpdateMenuPosition(incomingObject.GetBounds());
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
        
        objectMenu.CurrentIndex = 0;
        selectedObject = default;
        gameObject.SetActive(false);
    }

    public void OnTracesInfoButtonPressed()
    {
        tracesMenu.LoadTraceWindow(isEditable);
        tracesMenu.LoadTraceInfo(selectedObject.GetTraceInfo());
    }

    public void UpdateMenuPosition(Bounds objectBounds)
    {
        gameObject.transform.position = new Vector3(objectBounds.max.x, objectBounds.min.y + 0.2f, objectBounds.min.z + -0.14f);
    }

    public void ToggleEditMode()
    {
        if (!isEditable)
        {
            isEditable = true;

            if (tracesMenu.isActiveAndEnabled)
            {
                tracesMenu.LoadTraceWindow(isEditable);
                tracesMenu.LoadTraceInfo(selectedObject.GetTraceInfo());
            }
        }
        else
        {
            isEditable = false;

            if (!tracesMenu.isActiveAndEnabled)
            {
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
}
