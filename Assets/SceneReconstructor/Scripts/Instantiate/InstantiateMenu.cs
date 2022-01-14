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
            string formattedKey = (key.Split('/').Last()).Split('.').First();
            InstantiateManager.InstatiateMenuButtons(formattedKey, objectListGrid, crimeScene);
        }
        }));
    }

    public void OpenMenu()
    {
        
        if (!gameObject.activeInHierarchy)
        {
            gameObject.SetActive(true);
        }
    }
}
