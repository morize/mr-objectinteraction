using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Microsoft.MixedReality.Toolkit.UI;
using Microsoft.MixedReality.Toolkit.UI.BoundsControl;
using Microsoft.MixedReality.Toolkit.Input;

public class ObjectTrigger : MonoBehaviour
{
    BoxCollider boxCollider;
    ConstraintManager constraintManager;
    Interactable interactable;

    BoundsControl boundsControl;
    ObjectManipulator objectManipulator;
    NearInteractionGrabbable nearInteractionGrabbable;

    GameObject objectMenu;

    void Start()
    {
        AddObjectInteractionComponents();
        objectMenu = gameObject.transform.parent.Find("Object Edit Menu").gameObject;
    }

    // Add scripts that enables object selection.
    // Add and temporary disable scripts that enables movement and boundingboxes.
    private void AddObjectInteractionComponents()
    {
        boxCollider = gameObject.AddComponent<BoxCollider>();
        constraintManager = gameObject.AddComponent<ConstraintManager>();

        interactable = gameObject.AddComponent<Interactable>();
        interactable.OnClick.AddListener(OnObjectTriggered);

        boundsControl = gameObject.AddComponent<BoundsControl>();
        boundsControl.BoxPadding = new Vector3(0.01f, 0.01f, 0.01f);
        boundsControl.RotationHandlesConfig.ShowHandleForX = false;
        boundsControl.RotationHandlesConfig.ShowHandleForY = false;
        boundsControl.RotationHandlesConfig.ShowHandleForZ = false;
        boundsControl.ScaleHandlesConfig.ShowScaleHandles = false;
        boundsControl.enabled = false;

        objectManipulator = gameObject.AddComponent<ObjectManipulator>();
        objectManipulator.enabled = false;

        nearInteractionGrabbable = gameObject.AddComponent<NearInteractionGrabbable>();
        nearInteractionGrabbable.enabled = false;
    }

    // Enables boundingboxes around the object as visual feedback when the object is selected.
    // Shows the editable object menu option in the user's POV.
    private void OnObjectTriggered()
    {
        if (!boundsControl.enabled)
        {
            boxCollider.enabled = false;
            boundsControl.enabled = true;
            
            objectMenu.SetActive(true);
            objectMenu.GetComponent<ObjectMenu>().SetEditableObject(gameObject);
        }
    }

    // Fires when a new object is selected.
    // Disables the previous selected object's boundingboxes to indicate deselection and it's boxcollider is reenabled for future selection.
    public void OnObjectFocusOff()
    {
        objectManipulator.enabled = false;
        boundsControl.enabled = false;
        boxCollider.enabled = true;
    }

    // Fires when an edit mode button is pressed.
    // Enables the right boundingbox edit functionality with the incoming edit mode string.
    public void SetEditMode(string mode)
    {
        DisableEditProperties();

        switch (mode)
        {
            case "move":
                objectManipulator.enabled = true;
                nearInteractionGrabbable.enabled = true;
                break;

            case "scale":
                boundsControl.ScaleHandlesConfig.ShowScaleHandles = true;
                break;

            case "rotate":
                boundsControl.RotationHandlesConfig.ShowHandleForX = true;
                boundsControl.RotationHandlesConfig.ShowHandleForY = true;
                break;

            default:
                break;
        }
    }

    // Fires everytime a new edit mode button is pressed or the edit object menu is hidden.
    // Disables all boundingbox functionality except for the visual cue.
    public void DisableEditProperties()
    {
        objectManipulator.enabled = false;
        nearInteractionGrabbable.enabled = false;
        boundsControl.RotationHandlesConfig.ShowHandleForX = false;
        boundsControl.RotationHandlesConfig.ShowHandleForY = false;
        boundsControl.RotationHandlesConfig.ShowHandleForZ = false;
        boundsControl.ScaleHandlesConfig.ShowScaleHandles = false;
    }
}
