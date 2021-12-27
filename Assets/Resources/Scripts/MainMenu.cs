using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public GameObject crimeScene;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    
    public void LoadObject(string objectName)
    {
        GameObject instantiatedObject = (GameObject)Instantiate(Resources.Load("Prefabs/Objects/" + objectName));

        instantiatedObject.transform.SetParent(crimeScene.transform);
        instantiatedObject.name = "Object_" + objectName;
    }
}
