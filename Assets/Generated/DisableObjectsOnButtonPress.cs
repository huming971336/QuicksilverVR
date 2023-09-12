using UnityEngine;
using Oculus;

public class DisableEnableObjectsOnButtonPress : MonoBehaviour
{
    [Tooltip("The button on the controller to press to disable and enable objects.")]
    public OVRInput.Button buttonToPressA = OVRInput.Button.One;

    [Tooltip("The button on the controller to press to disable and enable objects.")]
    public OVRInput.Button buttonToPressB = OVRInput.Button.Two;


    [Tooltip("The objects to disable and enable when the button is pressed.")]
    public GameObject[] objectsToDisableEnable;

    public GameObject performerEyes;

    private bool objectsDisabled = false;
    public string targetTag = "Anchor";

    private void Update()
    {
        if (OVRInput.Get(buttonToPressA))
        {
            if (!objectsDisabled)
                
            { 
                GameObject[] objectsWithTag = GameObject.FindGameObjectsWithTag(targetTag);
                foreach (GameObject obj in objectsToDisableEnable)
                {
                    obj.SetActive(false);
                    foreach (GameObject objTag in objectsWithTag)
                    {
                        // Deactivate all children of the object
                        foreach (Transform child in objTag.transform)
                        {
                            child.gameObject.SetActive(false);
                        }
                    }
                }

                objectsDisabled = true;
            }
        }

        if (OVRInput.Get(buttonToPressA) && OVRInput.Get(buttonToPressB))
        {
            GameObject[] objectsWithTag = GameObject.FindGameObjectsWithTag(targetTag);
            foreach (GameObject obj in objectsToDisableEnable)
            {
                obj.SetActive(true);
                foreach (GameObject objTag in objectsWithTag)
                {
                    // Deactivate all children of the object
                    foreach (Transform child in objTag.transform)
                    {
                        child.gameObject.SetActive(true);
                    }
                }
            }

            objectsDisabled = false;
        }
    }
}