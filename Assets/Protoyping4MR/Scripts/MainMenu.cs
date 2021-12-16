using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public GameObject playground;
    public GameObject refrigerator;

    // Start is called before the first frame update
    void Start()
    {
        
    }
    
    public void LoadObject(GameObject prefabObject)
    {
        prefabObject = Instantiate(prefabObject);
        prefabObject.transform.SetParent(playground.transform);
        prefabObject.name = "Object_Refrigerator";
    }
}
