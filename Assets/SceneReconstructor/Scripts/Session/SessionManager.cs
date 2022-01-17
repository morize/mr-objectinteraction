using System;
using System.Collections.Generic;
using UnityEngine;

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
    const string SAVEFILE = "SavedCrimeScenes.json";

    void Start()
    {
        LoadObjectDataFromJson();
    }

    public void SaveObjectDataInJson()
    {
        string jsonObjectData = JsonUtility.ToJson(GetCrimeSceneObjectData());

        FileManager.StoreJsonData(SAVEFILE, jsonObjectData);
    }

    public void LoadObjectDataFromJson()
    {
        List<SavableObject> objectsToLoad = FileManager.ReadJsonData<SavableObjects>(SAVEFILE).savableObjects;

        InstantiateCrimeSceneObjects(objectsToLoad);
    }

    private SavableObjects GetCrimeSceneObjectData()
    {
        SavableObjects savableObjects = new SavableObjects();

        foreach (Transform item in transform)
        {
            if (item.name == "Floor")
            {
                continue;
            }

            SavableObject savableObject = new SavableObject();

            savableObject.name = item.name;
            savableObject.px = item.position.x;
            savableObject.py = item.position.y;
            savableObject.pz = item.position.z;
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

            InstantiateManager.InstatiateObjects(obj.name, gameObject.transform, objPosition, objScale, objRotation);
        }
    }
}
