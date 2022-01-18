using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TracesMenu : MonoBehaviour
{
    public Sprite bloodTraceImg;
    public Sprite fingerprintsTraceImg;
    public Sprite drugsTraceImg;

    public Image image;
    public TextMeshProUGUI extraInfo;

    public TextMeshProUGUI title;
    public TextMeshProUGUI description;

    [SerializeField]
    public GameObject viewmodeContent;
    public GameObject editmodeContent;


    public void LoadTraceWindow(bool isEditable)
    {
        if (!gameObject.activeInHierarchy)
        {
            gameObject.SetActive(true);
        }

        if (isEditable)
        {
            viewmodeContent.SetActive(false);
            editmodeContent.SetActive(true);
        }
        else
        {
            editmodeContent.SetActive(false);
            viewmodeContent.SetActive(true);
        }
    }

    public void CloseTracesWindow()
    {
        if (gameObject.activeInHierarchy)
        {
            gameObject.SetActive(false);
        }
    }

    // Hardcoded traces data
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
            "in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.\n\n" +
            "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt" +
            "ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit " +
            "in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.";
        traceToSave.fromCase = "Badjasmoord";

        return traceToSave;
    }

    public void LoadTraceInfo(Trace selectedObjectTrace)
    {
        if (selectedObjectTrace != null)
        {
            if (selectedObjectTrace.name != "")
            {
                // Image should load from base54 string (adquired from database) and create a sprite with it. 
                // For now the images are hard loaded in this script and referenced by id.
                if (selectedObjectTrace.imageId == 1)
                {
                    image.sprite = bloodTraceImg;
                }
                if (selectedObjectTrace.imageId == 2)
                {
                    image.sprite = fingerprintsTraceImg;
                }
                if (selectedObjectTrace.imageId == 3)
                {
                    image.sprite = drugsTraceImg;
                }

                image.preserveAspect = true;

                title.text = selectedObjectTrace.name;
                description.text = selectedObjectTrace.description;
                extraInfo.text = "Type: " + selectedObjectTrace.type + "\n" +
                    "Condition: " + selectedObjectTrace.condition + "\n" +
                    "Date Collected " + selectedObjectTrace.dateCollected + "\n" +
                    "From Case " + selectedObjectTrace.fromCase;
            }
        }
    }
}
