using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Microsoft.MixedReality.Toolkit.Utilities.Solvers;

public class TracesController : MonoBehaviour
{
    [Serializable]
    private class Trace
    {
        public GameObject traceObject;
        public Sprite traceImage;

        public GameObject GetGameObject() {
            return traceObject;
        }

        public Sprite GetImage()
        {
            return traceImage;
        }
    }

    [SerializeField] private Trace[] traces;

    bool isHighlighted = false;

    void Start()
    {
        foreach (Trace trace in traces)
        {
            GenerateOutlines(trace);
            GenerateImages(trace);
        }   
    }

    private void GenerateOutlines(Trace traceObject)
    {
        Outline highlight = traceObject.GetGameObject().AddComponent<Outline>();
        highlight.enabled = false;
        highlight.OutlineColor = new Color(0, 71, 255);
    }

    private void GenerateImages(Trace traceObject)
    {
        GameObject image = new GameObject();
        image.name = "TraceImage";

        SpriteRenderer imageSprite = image.AddComponent<SpriteRenderer>();
        imageSprite.sprite = traceObject.GetImage();

        image.AddComponent<SolverHandler>();

        RadialView imageRadialView = image.AddComponent<RadialView>();
        imageRadialView.IgnoreDistanceClamp = true;
        imageRadialView.IgnoreAngleClamp = true;


        image.transform.SetParent(traceObject.GetGameObject().transform);
        image.transform.localPosition = new Vector3(0, 0.5f, -0.5f);
        image.transform.localScale = new Vector3(0.02f, 0.02f, 0.02f);
        image.SetActive(false);
    }

    public void ToggleHighlight()
    {
        if (!isHighlighted)
        {
            foreach (Trace trace in traces)
            {
                trace.GetGameObject().GetComponent<Outline>().enabled = true;
            }
        }

        if (isHighlighted)
        {
            foreach (Trace trace in traces)
            {
                trace.GetGameObject().GetComponent<Outline>().enabled = false;
            }
        }

        isHighlighted = !isHighlighted;
    }

    public void ToggleImageOn(GameObject selectedObject)
    {
        GameObject image = selectedObject.transform.Find("TraceImage").gameObject;
        image.SetActive(true);
    }

    public void ToggleImageOff(GameObject selectedObject)
    {
        GameObject image = selectedObject.transform.Find("TraceImage").gameObject;
        image.SetActive(false);
    }
}
