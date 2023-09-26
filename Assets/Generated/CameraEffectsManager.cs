using UnityEngine;

public class CameraEffectsManager : MonoBehaviour
{
    [SerializeField, Tooltip("The OVR Passthrough Layer script to modify.")]
    private OVRPassthroughLayer passthroughLayer;

    [SerializeField, Tooltip("The Passthrough Styler script to modify.")]
    private PassthroughStyler passthroughStyler;

    [SerializeField, Tooltip("The color to set for the passthrough layer.")]
    private Color passthroughColor;

    [SerializeField, Tooltip("The opacity to set for the passthrough layer.")]
    private float passthroughOpacity;

    [SerializeField, Tooltip("The brightness to set for the passthrough styler.")]
    private float passthroughBrightness;

    [SerializeField, Tooltip("The contrast to set for the passthrough styler.")]
    private float passthroughContrast;

    
    void Start()
    {
        GameObject ovrCameraRig = GameObject.Find("OVRCameraRig");
        if (ovrCameraRig == null)
        {
            Debug.LogError("Scene does not contain an OVRCameraRig");
            return;
        }

        passthroughLayer = ovrCameraRig.GetComponent<OVRPassthroughLayer>();
        if (passthroughLayer == null)
        {
            Debug.LogError("OVRCameraRig does not contain an OVRPassthroughLayer component");
        }
    }

    void OverlayController()
    {
        
        if (OVRInput.GetDown(OVRInput.Button.One, OVRInput.Controller.RTouch))
        {
            passthroughLayer.hidden = !passthroughLayer.hidden;
        }

        float thumbstickX = OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick, OVRInput.Controller.RTouch).x;
        passthroughLayer.textureOpacity = thumbstickX * 0.5f + 0.5f;
    }


    private void Update()
    {
        // Update passthrough layer and styler with current values

    }
}