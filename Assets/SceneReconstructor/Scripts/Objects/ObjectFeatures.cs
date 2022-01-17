using System.Collections;
using UnityEngine;
using Microsoft.MixedReality.Toolkit.UI;
using Microsoft.MixedReality.Toolkit.UI.BoundsControl;
using Microsoft.MixedReality.Toolkit.Utilities.Solvers;

public class ObjectFeatures : MonoBehaviour
{
    private Interactable interactable;
    private BoundsControl boundsControl;
    private SolverHandler solverHandler;
    private TapToPlace tapToPlace;
    private Renderer objectRenderer;

    private ObjectFeatures objectFeatures;
    private ObjectMenu objectMenu;
    private Trace traceInfo;

    void Start()
    {
        objectFeatures = gameObject.GetComponent<ObjectFeatures>();
        objectMenu = gameObject.transform.parent.transform.parent.Find("ObjectEdit Menu").GetComponent<ObjectMenu>();
        objectRenderer = gameObject.GetComponent<Renderer>();

        AssignObjectInteractionComponents();
    }

    private void AssignObjectInteractionComponents()
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
        boundsControl.RotateStopped.AddListener(AdjustObjectPosition);
        boundsControl.ScaleStopped.AddListener(AdjustObjectPosition);
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
        tapToPlace.OnPlacingStopped.AddListener(AdjustObjectPosition);
        tapToPlace.enabled = false;
    }

    private void OnObjectTriggered()
    {
        if (!boundsControl.enabled)
        {
            boundsControl.enabled = true;
            objectMenu.OnObjectTriggered(objectFeatures);
        }
    }

    private void ResetObjectInteractionProperties()
    {
        solverHandler.enabled = false;
        tapToPlace.AutoStart = false;
        tapToPlace.enabled = false;
        boundsControl.RotationHandlesConfig.ShowHandleForX = false;
        boundsControl.RotationHandlesConfig.ShowHandleForY = false;
        boundsControl.RotationHandlesConfig.ShowHandleForZ = false;
        boundsControl.ScaleHandlesConfig.ShowScaleHandles = false;
    }

    private void AdjustObjectPosition()
    {
       Bounds bounds = objectRenderer.bounds;

        if (gameObject.transform.localPosition.y - bounds.min.y > 0.001)
        {
            float fixedPositionY = gameObject.transform.localPosition.y + (-0.3875f - bounds.min.y/gameObject.transform.parent.localScale.y);
            gameObject.transform.localPosition = new Vector3(gameObject.transform.localPosition.x, fixedPositionY, gameObject.transform.localPosition.z);
        }

        objectMenu.UpdateMenuPosition(bounds);
    }

    public void OnEditButtonPressed(string mode)
    {
        ResetObjectInteractionProperties();

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
                boundsControl.RotationHandlesConfig.ShowHandleForZ = true;
                break;

            case "addTrace":
                objectMenu.OnTracesInfoButtonPressed();
                break;

            case "delete":
                objectMenu.OnDeleteButtonPressed();
                InstantiateManager.ReleaseGameObject(gameObject);
                break;

            default:
                break;
        }
    }

    public void OnObjectFocusOff()
    {
        solverHandler.enabled = false;
        boundsControl.enabled = false;
        tapToPlace.AutoStart = false;
        tapToPlace.enabled = false;
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
