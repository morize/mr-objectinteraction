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

    public bool isEditModeEnabled = true;

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
            tracesMenu.LoadTrace(isEditModeEnabled, incomingObject.GetTraceInfo());
        }

        selectedObject = incomingObject;
    }

    public void OnEditButtonPressed(string editMode)
    {
        selectedObject.OnEditButtonPressed(editMode);
    }

    public void OnMoreButtonPressed()
    {
        if (!hiddenButtons.activeInHierarchy) hiddenButtons.SetActive(true);
        else hiddenButtons.SetActive(false);

        objectMenu.CurrentIndex = 0;
    }

    public void OnTracesInfoButtonPressed()
    {
        tracesMenu.LoadTrace(isEditModeEnabled, selectedObject.GetTraceInfo());
    }

    public void OnDeleteButtonPressed()
    {
        if (tracesMenu.isActiveAndEnabled) tracesMenu.CloseTracesWindow();

        InstantiateManager.ReleaseGameObject(selectedObject.gameObject);
        selectedObject = null;
        
        objectMenu.CurrentIndex = 0;
        gameObject.SetActive(false);
    }

    public void AlignMenuWithObject(Bounds objectBounds)
    {
        gameObject.transform.position = new Vector3(objectBounds.max.x, objectBounds.min.y + 0.2f, objectBounds.min.z + -0.14f);
    }

    private void ResetEnvironment()
    {
        if (objectMenu)
        {
            if (objectMenu.isActiveAndEnabled) objectMenu.CurrentIndex = 0;
        }

        if (selectedObject)
        {
            selectedObject.OnObjectFocusOff();
            selectedObject = null;
        }

        if (tracesMenu.isActiveAndEnabled) tracesMenu.CloseTracesWindow();

        gameObject.SetActive(false);
    }

    public void ToggleEditMode()
    {
        ResetEnvironment();
        string mode = !isEditModeEnabled ? "Edit Mode" : "View Only Mode";
        MainMenu.SetModeLabel(mode);
        isEditModeEnabled = !isEditModeEnabled;
    }

    public void SetTraceToSelectedObject(string trace)
    {
        Trace newTrace = tracesMenu.GenerateTraceInfo(trace);
        selectedObject.SetTraceInfo(newTrace);
    }
}
