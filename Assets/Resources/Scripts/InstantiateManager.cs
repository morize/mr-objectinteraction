using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantiateManager : MonoBehaviour
{
    static readonly string objectPath = "Prefabs/Objects/";

    public static void InstatiateObjects(string objectName, Transform parent)
    {
        GameObject instantiatedObject = (GameObject)Instantiate(Resources.Load(objectPath + objectName), parent);
        instantiatedObject.name = objectName;
    }

    public static void InstatiateObjects(string objectName, Transform parent, Vector3 position, Vector3 scale, Quaternion rotation)
    {
        GameObject instantiatedObject = (GameObject)Instantiate(Resources.Load(objectPath + objectName), parent);
        instantiatedObject.name = objectName;
        instantiatedObject.transform.position = position;
        instantiatedObject.transform.localScale = scale;
        instantiatedObject.transform.rotation = rotation;
    }
}
