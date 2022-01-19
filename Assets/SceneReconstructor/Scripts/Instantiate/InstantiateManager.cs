using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.ResourceLocations;
using UnityEngine.ResourceManagement.AsyncOperations;


public static class InstantiateManager
{
    private static readonly string objectPath = "Assets/SceneReconstructor/Prefabs/Objects/";
    private static readonly string utilsPath = "Assets/SceneReconstructor/Prefabs/Utilities/";

    public static void InstatiateObject(string objectName, Transform parent)
    {
        Addressables.InstantiateAsync(objectPath + objectName + ".prefab", parent).Completed += (res) => {
            res.Result.name = objectName;
        };
    }

    public static void InstatiateObject(string objectName, Transform parent, Vector3 position, Vector3 scale, Quaternion rotation, Trace trace)
    {
        Addressables.InstantiateAsync(objectPath + objectName + ".prefab", parent).Completed += (res) => {
            res.Result.name = objectName;
            res.Result.transform.localPosition = position;
            res.Result.transform.localScale = scale;
            res.Result.transform.localRotation = rotation;
            res.Result.GetComponent<ObjectFeatures>().SetTraceInfo(trace);
        };
    }

    public static void InstatiateMenuButton(string objectName, Transform gridParent, Transform instantiateParent)
    {
        InstantiateButton buttonReference;

        Addressables.InstantiateAsync(utilsPath + "ListObjectButton.prefab", gridParent).Completed += (res) => {
            res.Result.name = "ListObjectButton " + objectName;
            buttonReference = res.Result.GetComponent<InstantiateButton>();
            buttonReference.SetLabel(objectName);
            buttonReference.SetInstantiateButtonEvent(objectName, instantiateParent);
        };
    }

    public static IEnumerator GetAddressableObjects(Action<List<string>> callback)
    {
        AsyncOperationHandle<IList<IResourceLocation>> handle = Addressables.LoadResourceLocationsAsync("LoadableObjects");

        yield return handle;

        if (handle.Result == null) yield break;

        List<string> keys = new List<string>();

        foreach (var location in handle.Result)
        {
            keys.Add(location.PrimaryKey);
        }

        callback(keys);

        Addressables.Release(handle);
    }

    public static void ReleaseGameObject(GameObject obj)
    {
        Addressables.ReleaseInstance(obj);
    }
}
