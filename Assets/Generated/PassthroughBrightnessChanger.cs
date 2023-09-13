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


    private IEnumerator FadeOpacityToZero()
    {
        float t = 0f;
        float startOpacity = 1f;

        while (t < 1f)
        {
            t += Time.deltaTime * passthroughChangeSpeed;
            float newBrightness = Mathf.Lerp(startOpacity, 0f, t);
            passthroughStyler._savedOpacity = newBrightness;
            yield return null;
        }
    }
    private IEnumerator FadeOpacityToOne()
    {
        float t = 0f;
        float startOpacity = 0f;

        while (t < 1f)
        {
            t += Time.deltaTime * passthroughChangeSpeed;
            float newBrightness = Mathf.Lerp(startOpacity, 1f, t);
            passthroughStyler._savedOpacity = newBrightness;
            yield return null;
        }
    }
    public void FadeClientOpacityOne()
    {
        photonView.RPC("StartFadeOpacityToOne", RpcTarget.All);
    }
    public void FadeClientOpacityZero()
    {
        photonView.RPC("StartFadeOpacityToZero", RpcTarget.All);
    }
    [PunRPC]
    public void StartFadeOpacityToOne()
    {
        passthroughStyler._savedOpacity = 1f;
        Debug.Log("opacity 1");
       // StartCoroutine(FadeOpacityToOne());
    }

    [PunRPC]
    public void StartFadeOpacityToZero()
    {
        passthroughStyler._savedOpacity = 0f;
        Debug.Log("opacity 0");

        StartCoroutine(FadeOpacityToZero());
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