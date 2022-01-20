using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    private static TextMeshPro textMeshPro;

    void Start()
    {
        textMeshPro = gameObject.transform.Find("Main Menu Text").gameObject.GetComponent<TextMeshPro>();
    }

    public static void SetModeLabel(string mode)
    {
        textMeshPro.text = "Case: Badjasmoord\nCurrent Mode: " + mode;
    }
}
