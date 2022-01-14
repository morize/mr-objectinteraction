using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using System.Threading.Tasks;
using UnityEngine.ResourceManagement.ResourceLocations;
using UnityEngine.ResourceManagement.AsyncOperations;

public class InstantiateManager: MonoBehaviour
{
    private static readonly string objectPath = "Assets/SceneReconstructor/Prefabs/Objects/";
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
    
    public static IEnumerator InitializeMapList(string label)
    {
        AsyncOperationHandle<IList<IResourceLocation>> locationsHandle = Addressables.LoadResourceLocationsAsync(label);
        
        yield return locationsHandle;
        
        if (locationsHandle.Result == null) yield break;
      
        
        List<IResourceLocation> locations = new List<IResourceLocation>(locationsHandle.Result);

        foreach (var location in locations)
        {
            Debug.Log(location.PrimaryKey);
        }
        
        Addressables.Release(locationsHandle);
    }
}
