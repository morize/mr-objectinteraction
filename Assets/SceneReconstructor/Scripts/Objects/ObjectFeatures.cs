using System.Collections;
using UnityEngine;
using Microsoft.MixedReality.Toolkit.UI;
using Microsoft.MixedReality.Toolkit.UI.BoundsControl;
using Microsoft.MixedReality.Toolkit.Utilities.Solvers;

public class ObjectFeatures : MonoBehaviour
{
    private Renderer objectRenderer;
    private Interactable interactable;
    private BoundsControl boundsControl;
    private SolverHandler solverHandler;
    private TapToPlace tapToPlace;

    private ObjectFeatures objectFeatures;
    private ObjectMenu objectMenu;
    private Trace traceInfo;

    void Start()
    {
        objectFeatures = gameObject.GetComponent<ObjectFeatures>();
        objectMenu = gameObject.transform.parent.transform.parent.Find("ObjectEdit Menu").GetComponent<ObjectMenu>();
        objectRenderer = gameObject.GetComponent<Renderer>();

        AssignMrtkInteractionComponents();
    }
    private void AssignMrtkInteractionComponents()
    {
        gameObject.AddComponent<ConstraintManager>();

        interactable = gameObject.AddComponent<Interactable>();
        interactable.OnClick.AddListener(OnObjectTriggered);

        boundsControl = gameObject.AddComponent<BoundsControl>();
        boundsControl.BoxPadding = new Vector3(0.05f, 0.05f, 0.05f);
        boundsControl.RotationHandlesConfig.ShowHandleForX = false;
        boundsControl.RotationHandlesConfig.ShowHandleForY = false;
        boundsControl.RotationHandlesConfig.ShowHandleForZ = false;
        boundsControl.ScaleHandlesConfig.ShowScaleHandles = false;
        boundsControl.RotateStopped.AddListener(CheckIfObjectExceedesFloor);
        boundsControl.ScaleStopped.AddListener(CheckIfObjectExceedesFloor);
        boundsControl.enabled = false;
        
        solverHandler = gameObject.AddComponent<SolverHandler>();
        solverHandler.enabled = false;

        tapToPlace = gameObject.AddComponent<TapToPlace>();
        tapToPlace.UseDefaultSurfaceNormalOffset = false;
        tapToPlace.SurfaceNormalOffset = 0;
        tapToPlace.KeepOrientationVertical = true;
        tapToPlace.MagneticSurfaces[0].value = 8;
        tapToPlace.RotateAccordingToSurface = true;
        tapToPlace.DebugEnabled = false;
        tapToPlace.OnPlacingStopped.AddListener(CheckIfObjectExceedesFloor);
        tapToPlace.enabled = false;
    }

    private void OnObjectTriggered()
    {
        if (objectMenu.isEditModeEnabled && !boundsControl.enabled)
        {
            boundsControl.enabled = true;
            objectMenu.AlignMenuWithObject(objectRenderer.bounds);
        }

        objectMenu.OnObjectTriggered(objectFeatures);
    }

    private void DisableMrtkInteractionComponents()
    {
        solverHandler.enabled = false;
        tapToPlace.AutoStart = false;
        tapToPlace.enabled = false;
        boundsControl.RotationHandlesConfig.ShowHandleForX = false;
        boundsControl.RotationHandlesConfig.ShowHandleForY = false;
        boundsControl.RotationHandlesConfig.ShowHandleForZ = false;
        boundsControl.ScaleHandlesConfig.ShowScaleHandles = false;
    }

    private void CheckIfObjectExceedesFloor()
    {
        float minObjectPositionY = objectRenderer.bounds.min.y;
        float maxFloorPositionY = SessionManager.floorRenderer.bounds.max.y;

        if (gameObject.transform.localPosition.y - minObjectPositionY > 0.001)
        {
            float fixedPositionY = gameObject.transform.localPosition.y + (maxFloorPositionY - minObjectPositionY / gameObject.transform.parent.localScale.y);
            gameObject.transform.localPosition = new Vector3(gameObject.transform.localPosition.x, fixedPositionY, gameObject.transform.localPosition.z);
        }

        objectMenu.AlignMenuWithObject(objectRenderer.bounds);
    }

    public void OnEditButtonPressed(string editMode)
    {
        DisableMrtkInteractionComponents();

        switch (editMode)
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
                boundsControl.RotationHandlesConfig.ShowHandleForZ = true;
                break;

            default:
                break;
        }
    }

    public void OnObjectFocusOff()
    {
        DisableMrtkInteractionComponents();

        boundsControl.enabled = false;
    }

    public Trace GetTraceInfo()
    {
        return traceInfo;
    }

    public void SetTraceInfo(Trace newTrace)
    {
        traceInfo = newTrace;
    }

    public Bounds GetBounds()
    {
        return objectRenderer.bounds;
    }
}
