using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

[Serializable]
public struct SavableObject
{
    public string name;
    public float px, py, pz;
    public float sx, sy, sz;
    public float rx, ry, rz, rw;
}

[Serializable]
public class SavableObjectCollection
{
    public List<SavableObject> savableObjectCollection = new List<SavableObject>();
}

public class SaveManager : MonoBehaviour
{
    string fileName = "listOfObjects.json";

    void Start()
    {
        LoadObjectData();
    }

    public void SaveObjectData()
    {
        SavableObjectCollection objectsToSave = new SavableObjectCollection();

        foreach (Transform item in transform)
        {
            SavableObject savObject = new SavableObject();
            savObject.name = item.name;

            savObject.px = item.transform.position.x;
            savObject.py = item.transform.position.y;
            savObject.pz = item.transform.position.z;

            savObject.sx = item.transform.localScale.x;
            savObject.sy = item.transform.localScale.y;
            savObject.sz = item.transform.localScale.z;

            savObject.rx = item.transform.localRotation.x;
            savObject.ry = item.transform.localRotation.y;
            savObject.rz = item.transform.localRotation.z;
            savObject.rw = item.transform.localRotation.w;

            objectsToSave.savableObjectCollection.Add(savObject);
        }

        string serializedObjectData = JsonUtility.ToJson(objectsToSave);

        FileManager.StoreSerializedData(fileName, serializedObjectData);
    }

    public void LoadObjectData()
    {
        SavableObjectCollection objectsToLoad = FileManager.ReadSerializedData<SavableObjectCollection>(fileName);

        foreach (SavableObject obj in objectsToLoad.savableObjectCollection)
        {     
            if(obj.name != "Floor")
            {
                Vector3 position = new Vector3(obj.px, obj.py, obj.pz);
                Vector3 scale = new Vector3(obj.sx, obj.sy, obj.sz);
                Quaternion rotation = new Quaternion(obj.rx, obj.ry, obj.rz, obj.rw);
                InstantiateManager.InstatiateObjects(obj.name, gameObject.transform, position, scale, rotation);
            }  
        }
    }
}
