using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.ResourceManagement.ResourceLocations;

public class InstantiateMenu : MonoBehaviour
{
    [SerializeField]
    Transform ObjectListGrid;
    public IList<IResourceLocation> AssetLocations { get; } = new List<IResourceLocation>();

    void Start()
    {
        InstantiateListObjects();
    }

    private void InstantiateListObjects()
    {
        StartCoroutine(InstantiateManager.InitializeMapList("LoadableObjects"));

        //InstantiateManager.InstatiateObjects(objectName, ObjectListGrid);
    }

   

}
