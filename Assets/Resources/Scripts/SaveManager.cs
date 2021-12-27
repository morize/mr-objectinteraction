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
    string filename = "listOfObjects.json";

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

            objectsToSave.savableObjectCollection.Add(savObject);
        }

        string serializedObjectData = JsonUtility.ToJson(objectsToSave);

        WriteFile(GetPath(filename), serializedObjectData);
    }

    public void LoadObjectData()
    {
        SavableObjectCollection objectsToLoad = new SavableObjectCollection();
        string serializedObjectData = ReadFile(GetPath(filename));

        if (string.IsNullOrEmpty(serializedObjectData) || serializedObjectData == "{}")
        {
            return;
        }

        objectsToLoad = JsonUtility.FromJson<SavableObjectCollection>(serializedObjectData);

        foreach (SavableObject savableObject in objectsToLoad.savableObjectCollection)
        {
            // Instantiate with name and assign serialized data.
            Debug.Log(savableObject.px);
        }
    }

    private string GetPath (string filename)
    {
        return Application.persistentDataPath + "/" + filename;
    }

    private void WriteFile(string path, string content)
    {
        FileStream fileStream = new FileStream(path, FileMode.Create);

        using (StreamWriter writer = new StreamWriter(fileStream))
        {
            writer.Write(content);
        }
    }

    private string ReadFile(string path)
    {
        if (File.Exists(path))
        {
            using (StreamReader reader = new StreamReader(path))
            {
                string content = reader.ReadToEnd();
                return content;
            }
        }
        return "";
    }
}
