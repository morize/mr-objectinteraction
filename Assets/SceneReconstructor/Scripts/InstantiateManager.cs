using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class InstantiateManager : MonoBehaviour
{
    private static readonly string objectPath = "Assets/SceneReconstructor/Prefabs/Objects/";

    public static void InstatiateObjects(string objectName, Transform parent)
    {
        Addressables.InstantiateAsync(objectPath + objectName + ".prefab", parent).Completed += (res) => {
            res.Result.name = objectName;
        };
    }

    public static void InstatiateObjects(string objectName, Transform parent, Vector3 position, Vector3 scale, Quaternion rotation)
    {
        Addressables.InstantiateAsync(objectPath + objectName + ".prefab", parent).Completed += (res) => {
            res.Result.name = objectName;
            res.Result.transform.position = position;
            res.Result.transform.localScale = scale;
            res.Result.transform.rotation = rotation;
        };
    }
}
