using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    [SerializeField]
    private GameObject crimeScene;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void LoadObject(string objectName)
    {
        InstantiateManager.InstatiateObjects(objectName, crimeScene.transform);
    }
}
