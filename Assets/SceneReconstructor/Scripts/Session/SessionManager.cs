using System;
using System.Collections.Generic;
using UnityEngine;
using Microsoft.MixedReality.Toolkit.UI.BoundsControl;
using Microsoft.MixedReality.Toolkit.UI;

[Serializable]
public class Trace
{
    public string name;
    public string description;
    public string type;
    public string condition;
    public string dateCollected;
    public string fromCase;
    public int imageId;
}

[Serializable]
public class SavableObject
{
    public string name;
    public float px, py, pz;
    public float sx, sy, sz;
    public float rx, ry, rz, rw;
    public Trace trace;
}

[Serializable]
public class SavableObjects
{
    public List<SavableObject> savableObjects = new List<SavableObject>();
}

public class SessionManager : MonoBehaviour
{
    public static Renderer floorRenderer;

    private string saveFile = "SavedCrimeScenes.json";
    private BoundsControl boundsControl;
    private ObjectManipulator objectManipulator;

    void Start()
    {
        floorRenderer = transform.Find("Floor").GetComponent<Renderer>();

        AssignMrtkComponents();
        LoadObjectDataFromJson();
    }

    private void AssignMrtkComponents()
    {
        gameObject.AddComponent<ConstraintManager>();
        boundsControl = gameObject.AddComponent<BoundsControl>();
        boundsControl.RotationHandlesConfig.ShowHandleForX = false;
        boundsControl.RotationHandlesConfig.ShowHandleForY = false;
        boundsControl.RotationHandlesConfig.ShowHandleForZ = false;
        boundsControl.ScaleHandlesConfig.ShowScaleHandles = false;
        boundsControl.enabled = false;

        objectManipulator = gameObject.AddComponent<ObjectManipulator>();
        objectManipulator.enabled = false;
    }

    public void ToggleSceneMovement()
    {
        if (!boundsControl.isActiveAndEnabled)
        {
            boundsControl.ScaleHandlesConfig.ShowScaleHandles = true;
            boundsControl.enabled = true;

            objectManipulator.enabled = true;
        }
        else
        {
            boundsControl.ScaleHandlesConfig.ShowScaleHandles = false;
            boundsControl.enabled = false;

            objectManipulator.enabled = false;
        }
    }

    public void SaveObjectDataInJson()
    {
        string jsonObjectData = JsonUtility.ToJson(GetCrimeSceneObjectData());

        FileManager.StoreJsonData(saveFile, jsonObjectData);
    }

    public void LoadObjectDataFromJson()
    {
        SavableObjects objectsToLoad = FileManager.ReadJsonData<SavableObjects>(saveFile);

        if (objectsToLoad != null)
        {
            List<SavableObject> objects = objectsToLoad.savableObjects;
            InstantiateCrimeSceneObjects(objects);
        }
    }

    private SavableObjects GetCrimeSceneObjectData()
    {
        SavableObjects savableObjects = new SavableObjects();

        foreach (Transform item in transform)
        {
            if (item.name == "Floor" | item.name == "rigRoot")
            {
                continue;
            }

            SavableObject savableObject = new SavableObject();

            savableObject.name = item.name;
            savableObject.px = item.localPosition.x;
            savableObject.py = item.localPosition.y;
            savableObject.pz = item.localPosition.z;
            savableObject.sx = item.localScale.x;
            savableObject.sy = item.localScale.y;
            savableObject.sz = item.localScale.z;
            savableObject.rx = item.localRotation.x;
            savableObject.ry = item.localRotation.y;
            savableObject.rz = item.localRotation.z;
            savableObject.rw = item.localRotation.w;

            Trace itemTrace = item.gameObject.GetComponent<ObjectFeatures>().GetTraceInfo();

            if (itemTrace != null)
            {
                savableObject.trace = itemTrace;
            }

            savableObjects.savableObjects.Add(savableObject);
        }

        return savableObjects;
    }

    private void InstantiateCrimeSceneObjects(List<SavableObject> savableObjects)
    {
        foreach (SavableObject obj in savableObjects)
        {
            if (obj.name == "Floor")
            {
                continue;
            }

            Vector3 objPosition = new Vector3(obj.px, obj.py, obj.pz);
            Vector3 objScale = new Vector3(obj.sx, obj.sy, obj.sz);
            Quaternion objRotation = new Quaternion(obj.rx, obj.ry, obj.rz, obj.rw);

            InstantiateManager.InstatiateObject(obj.name, gameObject.transform, objPosition, objScale, objRotation, obj.trace);
        }
    }
}
