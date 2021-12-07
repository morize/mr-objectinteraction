using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Microsoft.MixedReality.Toolkit.UI;
using Microsoft.MixedReality.Toolkit.Input;
public class InteractableController : MonoBehaviour
{
    Outline outline;
    
    ObjectManipulator objectManipulator;
    ConstraintManager constraintManager;
    NearInteractionGrabbable nearInteractionGrabbable;

    bool isInteractable;

    void Start()
    {
        outline = gameObject.AddComponent<Outline>();
        objectManipulator = gameObject.AddComponent<ObjectManipulator>();
        constraintManager = gameObject.AddComponent<ConstraintManager>();
        nearInteractionGrabbable = gameObject.AddComponent<NearInteractionGrabbable>();

        outline.enabled = false;
        objectManipulator.enabled = false;
        constraintManager.enabled = false;
        nearInteractionGrabbable.enabled = false;
        gameObject.GetComponent<BoxCollider>().enabled = false;
    }

    public void ToggleInteractivity()
    {
        if (!isInteractable)
        {
            gameObject.GetComponent<BoxCollider>().enabled = true;
            outline.enabled = true;
            objectManipulator.enabled = true;
            constraintManager.enabled = true;
            nearInteractionGrabbable.enabled = true;
        }

        else
        {
            gameObject.GetComponent<BoxCollider>().enabled = false;
            outline.enabled = false;
            objectManipulator.enabled = false;
            constraintManager.enabled = false;
            nearInteractionGrabbable.enabled = false;
        }

        isInteractable = !isInteractable;
    }
}
