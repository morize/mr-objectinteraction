using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoomController : MonoBehaviour
{
    Renderer crimeScene;
    bool isZoomed = false;

    void Start()
    {
        crimeScene = this.GetComponent<Renderer>();
    }

    public void ToggleZoom()
    {
        if (!isZoomed)
        {
            transform.localScale = new Vector3(1f, 1f, 1f);
        }

        else
        {
            transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);
        }

        isZoomed = !isZoomed;
    }
}
