using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpacityController : MonoBehaviour
{
    GameObject[] walls;
    bool isVisible = true;

    void Start()
    {
        walls = GameObject.FindGameObjectsWithTag("Walls");
    }

    public void ToggleOpacity()
    {
        foreach (GameObject wall in walls)
        {
            Material wallMaterial = wall.GetComponent<Renderer>().material;

            if (isVisible)
            {
                SetOpacityOff(wallMaterial);
            }

            else
            {
                SetOpacityOn(wallMaterial);
            }
        }

         isVisible = !isVisible;
    }

    void SetOpacityOff(Material material)
    {
        material.SetOverrideTag("RenderType", "Transparent");
        material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
        material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
        material.SetInt("_ZWrite", 0);
        material.DisableKeyword("_ALPHATEST_ON");
        material.EnableKeyword("_ALPHABLEND_ON");
        material.DisableKeyword("_ALPHAPREMULTIPLY_ON");
        material.renderQueue = (int)UnityEngine.Rendering.RenderQueue.Transparent;

        material.color = new Color(1, 1, 1, 0.05f);
    }

    void SetOpacityOn(Material material)
    {
        material.SetOverrideTag("RenderType", "");
        material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.One);
        material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.Zero);
        material.SetInt("_ZWrite", 1);
        material.DisableKeyword("_ALPHATEST_ON");
        material.DisableKeyword("_ALPHABLEND_ON");
        material.DisableKeyword("_ALPHAPREMULTIPLY_ON");
        material.renderQueue = -1;
    }
}