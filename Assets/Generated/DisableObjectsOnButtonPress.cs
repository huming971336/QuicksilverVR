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

    private bool objectsDisabled = false;

    private void Update()
    {
        if (OVRInput.Get(buttonToPressA))
        {
            Debug.Log("One");
            if (!objectsDisabled)
            {
                foreach (GameObject obj in objectsToDisableEnable)
                {
                    obj.SetActive(false);
                }

                objectsDisabled = true;
            }
        }

        if (OVRInput.Get(buttonToPressA) && OVRInput.Get(buttonToPressB))
        {
            foreach (GameObject obj in objectsToDisableEnable)
            {
                obj.SetActive(true);
            }

            objectsDisabled = false;
        }
    }
}