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
        boxCollider = gameObject.AddComponent<BoxCollider>();
        constraintManager = gameObject.AddComponent<ConstraintManager>();

        interactable = gameObject.AddComponent<Interactable>();


        boundsControl = gameObject.AddComponent<BoundsControl>();
        boundsControl.BoxPadding = new Vector3(0.02f, 0.02f, 0.02f);
        boundsControl.RotationHandlesConfig.ShowHandleForX = false;
        boundsControl.RotationHandlesConfig.ShowHandleForY = false;
        boundsControl.RotationHandlesConfig.ShowHandleForZ = false;
        boundsControl.ScaleHandlesConfig.ShowScaleHandles = false;
        boundsControl.enabled = false;

        nearInteractionGrabbable = gameObject.AddComponent<NearInteractionGrabbable>();
        nearInteractionGrabbable.enabled = false;

        objectManipulator = gameObject.AddComponent<ObjectManipulator>();
        objectManipulator.enabled = false;

        interactable.OnClick.AddListener(OnObjectTriggered);
        
        objectMenu = gameObject.transform.parent.Find("Object Edit Menu").gameObject;
    }


    public void OnObjectTriggered()
    {
        if (!boundsControl.enabled)
        {
            boundsControl.enabled = true;
            
            objectMenu.SetActive(true);
            objectMenu.GetComponent<ObjectMenu>().SetEditableObject(gameObject);
        }
    }

    public void OnObjectFocusOff()
    {
        boundsControl.enabled = false;
    }

    public void SetEditMode(string mode)
    {
        DisableObjectProperties();

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
   
    public void DisableObjectProperties()
    {
        objectManipulator.enabled = false;
        nearInteractionGrabbable.enabled = false;
        boundsControl.RotationHandlesConfig.ShowHandleForX = false;
        boundsControl.RotationHandlesConfig.ShowHandleForY = false;
        boundsControl.RotationHandlesConfig.ShowHandleForZ = false;
        boundsControl.ScaleHandlesConfig.ShowScaleHandles = false;
    }
}
