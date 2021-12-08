using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Microsoft.MixedReality.Toolkit.UI;
using Microsoft.MixedReality.Toolkit.UI.BoundsControl;
using Microsoft.MixedReality.Toolkit.Input;

public class InteractableController : MonoBehaviour
{
    BoundsControl boundsControl;
    ObjectManipulator objectManipulator;
    ConstraintManager constraintManager;
    NearInteractionGrabbable nearInteractionGrabbable;

    bool isInteractable;

    void Start()
    {
        gameObject.AddComponent<ConstraintManager>();

        boundsControl = gameObject.AddComponent<BoundsControl>();

        objectManipulator = gameObject.AddComponent<ObjectManipulator>();
        constraintManager = gameObject.AddComponent<ConstraintManager>();
        nearInteractionGrabbable = gameObject.AddComponent<NearInteractionGrabbable>();

        boundsControl.enabled = false;
        boundsControl.BoxPadding = new Vector3(0.1f, 0.1f, 0.1f);
        boundsControl.RotationHandlesConfig.ShowHandleForX = false;
        GetComponent<BoxCollider>().enabled = false;

        objectManipulator.enabled = false;
        constraintManager.enabled = false;
        nearInteractionGrabbable.enabled = false;
    }

    public void ToggleInteractivity()
    {
        if (!isInteractable)
        {
            GetComponent<BoxCollider>().enabled = true;
            boundsControl.enabled = true;
            objectManipulator.enabled = true;
            constraintManager.enabled = true;
            nearInteractionGrabbable.enabled = true;
        }

        else
        {
            GetComponent<BoxCollider>().enabled = false;
            boundsControl.enabled = false;
            objectManipulator.enabled = false;
            constraintManager.enabled = false;
            nearInteractionGrabbable.enabled = false;
        }

        isInteractable = !isInteractable;
    }
}
