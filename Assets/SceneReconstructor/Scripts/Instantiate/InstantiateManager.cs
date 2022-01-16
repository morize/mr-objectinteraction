using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.AddressableAssets.ResourceLocators;
using UnityEngine.ResourceManagement.ResourceLocations;
using UnityEngine.ResourceManagement.AsyncOperations;


public static class InstantiateManager
{
    private static readonly string objectPath = "Assets/SceneReconstructor/Prefabs/Objects/";
    private static readonly string utilsPath = "Assets/SceneReconstructor/Prefabs/Utilities/";
    public static IList<IResourceLocation> AssetLocations { get; } = new List<IResourceLocation>();

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

    public static void InstatiateMenuButtons(string objectName, Transform gridParent, Transform instantiateParent)
    {
        InstantiateButton buttonReference;

        Addressables.InstantiateAsync(utilsPath + "ListObjectButton.prefab", gridParent).Completed += (res) => {
            res.Result.name = "ListObjectButton " + objectName;
            buttonReference = res.Result.GetComponent<InstantiateButton>();
            buttonReference.SetLabel(objectName);
            buttonReference.SetButtonEvent(objectName, instantiateParent);
        };
    }

    public static IEnumerator GetAddressableObjects(System.Action<List<string>> callback)
    {
        List<string> keys = new List<string>();

        AsyncOperationHandle<IList<IResourceLocation>> handle = Addressables.LoadResourceLocationsAsync("LoadableObjects");

        yield return handle;

        if (handle.Result == null) yield break;

        foreach (var location in handle.Result)
        {
            keys.Add(location.PrimaryKey);
        }

        callback(keys);

        Addressables.Release(handle);
    }

    public static void ReleaseGameObject (GameObject obj)
    {
        Addressables.ReleaseInstance(obj);
    }
}
