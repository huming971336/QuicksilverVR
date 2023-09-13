using Photon.Pun;
using System.Collections;
using UnityEngine;

public class PassthroughBrightnessChanger : MonoBehaviourPunCallbacks
{
    [Tooltip("Adjust the speed of brightness change")]
    public float brightnessChangeSpeed = 1f;
    public float passthroughChangeSpeed = 1f;


    private PassthroughStyler passthroughStyler;
    private float savedBrightness;
    private float savedStateOfPassthrough;


    private void Awake()
    {
        passthroughStyler = GetComponent<PassthroughStyler>();
        savedBrightness = passthroughStyler._savedBrightness;
   //     savedStateOfPassthrough = passthroughStyler.
    }

    private IEnumerator FadeBrightnessToValue(float targetValue)
    {
        float t = 0f;
        float startBrightness = savedBrightness;

        while (t < 1f)
        {
            t += Time.deltaTime * brightnessChangeSpeed;
            float newBrightness = Mathf.Lerp(startBrightness, targetValue, t);
            passthroughStyler._savedBrightness = newBrightness;
            yield return null;
        }
    }

    private IEnumerator FadeBrightnessToSavedValue(float targetValue)
    {
        float t = 0f;
        float startBrightness = targetValue;

        while (t < 1f)
        {
            t += Time.deltaTime * brightnessChangeSpeed;
            float newBrightness = Mathf.Lerp(startBrightness, savedBrightness, t);
            passthroughStyler._savedBrightness = newBrightness;
            yield return null;
        }
    }

    [PunRPC]
    public void StartFadeBrightnessToValue(float targetValue)
    {
        StartCoroutine(FadeBrightnessToValue(targetValue));
    }

    [PunRPC]
    public void StartFadeBrightnessToSavedValue(float targetValue)
    {
        StartCoroutine(FadeBrightnessToSavedValue(targetValue));
    }
}