using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Trace
{
    public string name;
    public int imageId;
    public string description;
    public string type;
    public string condition;
    public string dateCollected;
    public string fromCase;
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
public class SavableObjectCollection
{
    public List<SavableObject> savableObjectCollection = new List<SavableObject>();
}

public class SaveManager : MonoBehaviour
{
    const string FILENAME = "SavedCrimeScenes.json";

    void Start()
    {
        LoadObjectData();
    }

    public void SaveObjectData()
    {
        SavableObjectCollection objectsToSave = new SavableObjectCollection();
        
        foreach (Transform item in transform)
        {
            if (item.name == "Floor")
            {
                continue;
            }

            SavableObject savObject = new SavableObject();
            savObject.name = item.name;

            savObject.px = item.position.x;
            savObject.py = item.position.y;
            savObject.pz = item.position.z;

            savObject.sx = item.localScale.x;
            savObject.sy = item.localScale.y;
            savObject.sz = item.localScale.z;

            savObject.rx = item.localRotation.x;
            savObject.ry = item.localRotation.y;
            savObject.rz = item.localRotation.z;
            savObject.rw = item.localRotation.w;

            Trace itemTrace = item.gameObject.GetComponent<ObjectFeatures>().GetTraceInfo();

            if (itemTrace != null)
            {
                savObject.trace = itemTrace;
            }

            objectsToSave.savableObjectCollection.Add(savObject);
        }


        string serializedObjectData = JsonUtility.ToJson(objectsToSave);

        FileManager.StoreSerializedData(FILENAME, serializedObjectData);
    }

    public void LoadObjectData()
    {
        SavableObjectCollection objectsToLoad = FileManager.ReadSerializedData<SavableObjectCollection>(FILENAME);

        if (objectsToLoad != null)
        {
            foreach (SavableObject obj in objectsToLoad.savableObjectCollection)
            {
                if (obj.name == "Floor")
                {
                    continue;
                }
                
                Vector3 position = new Vector3(obj.px, obj.py, obj.pz);
                Vector3 scale = new Vector3(obj.sx, obj.sy, obj.sz);
                Quaternion rotation = new Quaternion(obj.rx, obj.ry, obj.rz, obj.rw);
                InstantiateManager.InstatiateObjects(obj.name, gameObject.transform, position, scale, rotation);
            }
        }
    }
}
