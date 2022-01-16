using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TracesManager : MonoBehaviour
{
    void Start()
    {
        
    }

    public void OpenTracesWindow()
    {
        if (!gameObject.activeInHierarchy)
        {
            gameObject.SetActive(true);
        }
    }

    // Set hardcoded traces data
    public Trace SetTraceInfo(string trace)
    {
        Trace traceToSave = new Trace();

        if (trace == "blood")
        {
            traceToSave.name = "Blood Samples around the chair";
            traceToSave.imageId = 1;
            traceToSave.type = "Blood";
            traceToSave.condition = "Clotted";
            traceToSave.dateCollected = "12/1/2022";
        }

        if (trace == "fingerprints")
        {
            traceToSave.name = "Fingerprints on the closet";
            traceToSave.imageId = 2;
            traceToSave.type = "Fingerprints";
            traceToSave.condition = "Visible with ALS";
            traceToSave.dateCollected = "01/1/2022";
        }

        if (trace == "drugs")
        {
            traceToSave.name = "Druglab leftovers";
            traceToSave.imageId = 3;
            traceToSave.type = "Chemicals";
            traceToSave.condition = "Hazardous";
            traceToSave.dateCollected = "08/1/2022";
        }

        if (trace == "delete")
        {
            return null;
        }

        traceToSave.description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt" +
            "ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit " +
            "in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.";
        traceToSave.fromCase = "Badjasmoord";

        return traceToSave;
    }
}
