using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Microsoft.MixedReality.Toolkit.UI;
using Microsoft.MixedReality.Toolkit.UI.BoundsControl;
using Microsoft.MixedReality.Toolkit.Utilities.Solvers;

public class ObjectTrigger : MonoBehaviour
{
    ConstraintManager constraintManager;
    Interactable interactable;

    BoundsControl boundsControl;
    SolverHandler solverHandler;
    TapToPlace tapToPlace;

    ObjectMenu objectMenu;

    void Start()
    {
        AddObjectInteractionComponents();
        objectMenu = gameObject.transform.parent.Find("Object Menu").GetComponent<ObjectMenu>();
    }

    // Add scripts that enables object selection.
    // Add and temporary disable scripts that enables movement and boundingboxes.
    private void AddObjectInteractionComponents()
    {
        constraintManager = gameObject.AddComponent<ConstraintManager>();

        interactable = gameObject.AddComponent<Interactable>();
        interactable.OnClick.AddListener(OnObjectTriggered);

        boundsControl = gameObject.AddComponent<BoundsControl>();
        boundsControl.BoxPadding = new Vector3(0.05f, 0.05f, 0.05f);
        boundsControl.RotationHandlesConfig.ShowHandleForX = false;
        boundsControl.RotationHandlesConfig.ShowHandleForY = false;
        boundsControl.RotationHandlesConfig.ShowHandleForZ = false;
        boundsControl.ScaleHandlesConfig.ShowScaleHandles = false;
        boundsControl.enabled = false;

        solverHandler = gameObject.AddComponent<SolverHandler>();
        solverHandler.enabled = false;

        tapToPlace = gameObject.AddComponent<TapToPlace>();
        tapToPlace.UseDefaultSurfaceNormalOffset = false;
        tapToPlace.SurfaceNormalOffset = 0;
        tapToPlace.KeepOrientationVertical = true;
        tapToPlace.MagneticSurfaces[0].value = 8;
        tapToPlace.enabled = false;
    }

    // Enables boundingboxes around the object as visual feedback when the object is selected.
    // Shows the editable object menu option in the user's POV.
    private void OnObjectTriggered()
    {
        if (!boundsControl.enabled)
        {
            boundsControl.enabled = true;
            objectMenu.SetEditableObject(gameObject);
        }
    }

    // Fires when a new object is selected.
    // Disables the previous selected object's boundingboxes to indicate deselection and it's boxcollider is reenabled for future selection.
    public void OnObjectFocusOff()
    {
        solverHandler.enabled = false;
        boundsControl.enabled = false;
        tapToPlace.AutoStart = false;
        tapToPlace.enabled = false;
    }

    // Fires when an edit mode button is pressed.
    // Enables the right boundingbox edit functionality with the incoming edit mode string.
    public void SetEditMode(string mode)
    {
        DisableEditProperties();

        switch (mode)
        {
            case "move":
                tapToPlace.enabled = true;
                solverHandler.enabled = true;
                break;

            case "scale":
                boundsControl.ScaleHandlesConfig.ShowScaleHandles = true;
                break;

            case "rotate":
                boundsControl.RotationHandlesConfig.ShowHandleForX = true;
                boundsControl.RotationHandlesConfig.ShowHandleForY = true;
                break;

            case "delete":
                // Confirmation popup?
                DisableEditProperties();
                objectMenu.OnObjectDeleted();
                Destroy(gameObject);
                break;

            default:
                break;
        }
    }

    // Fires everytime a new edit mode button is pressed or the edit object menu is hidden.
    // Disables all boundingbox functionality except for the visual cue.
    public void DisableEditProperties()
    {
        solverHandler.enabled = false;
        tapToPlace.AutoStart = false;
        tapToPlace.enabled = false;
        boundsControl.RotationHandlesConfig.ShowHandleForX = false;
        boundsControl.RotationHandlesConfig.ShowHandleForY = false;
        boundsControl.RotationHandlesConfig.ShowHandleForZ = false;
        boundsControl.ScaleHandlesConfig.ShowScaleHandles = false;
    }
}
