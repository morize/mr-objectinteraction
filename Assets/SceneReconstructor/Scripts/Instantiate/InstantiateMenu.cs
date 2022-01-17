using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class InstantiateMenu : MonoBehaviour
{
    [SerializeField]
    Transform objectListGrid;
    
    [SerializeField]
    Transform crimeScene;

    void Start()
    {
        InstantiateListObjects();
    }

    private void InstantiateListObjects()
    {
        StartCoroutine(InstantiateManager.GetAddressableObjects((keys) => {
            foreach (string key in keys)
            {
                string objectName = (key.Split('/').Last()).Split('.').First();
                InstantiateManager.InstatiateMenuButton(objectName, objectListGrid, crimeScene);
            }
        }));
    }

    public void OpenListObjectsMenu()
    {
        if (!gameObject.activeInHierarchy)
        {
            gameObject.SetActive(true);
        }
    }
}
