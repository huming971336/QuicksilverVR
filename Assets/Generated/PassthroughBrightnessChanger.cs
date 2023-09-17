using Photon.Pun;
using System.Collections;
using UnityEngine;

public class PassthroughBrightnessChanger : MonoBehaviourPunCallbacks
{
    [Tooltip("Adjust the speed of brightness change")]
    public float brightnessChangeSpeed = 1f;
   // public float passthroughChangeSpeed = 1f;


   [SerializeField] PassthroughStyler passthroughStyler;
    private float savedBrightness;
    private float savedStateOfPassthrough;

    public bool passthroughOpen = false;
    public bool passthroughClose = false;

    public float passthroughChangeSpeedGlobal;
    float t = 0f;
    float startOpacity = 0f;



    private void Awake()
    {
        passthroughStyler = gameObject.GetComponent<PassthroughStyler>();
        savedBrightness = passthroughStyler._savedBrightness;
        startOpacity = passthroughStyler._savedOpacity;
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

    /* private IEnumerator FadeBrightnessToSavedValue(float targetValue)
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
     }*/


    /* private IEnumerator FadeOpacityToZero(float passthroughChangeSpeed)
     {
         float t = 0f;
         float startOpacity = 1f;

         while (t < 1f)
         {
             t += Time.deltaTime * passthroughChangeSpeed;
             float newBrightness = Mathf.Lerp(startOpacity, 0f, t);
             passthroughStyler._savedOpacity = newBrightness;
             passthroughStyler.UpdatePassthroughOpacity();

             yield return null;
         }
     }*/
    float a = 0;
    private void FixedUpdate()
    {
        if(!passthroughClose && passthroughOpen && (passthroughStyler._savedOpacity <= 1f))
        {
            startOpacity = passthroughStyler._savedOpacity;

            passthroughStyler._savedOpacity = Mathf.Lerp(startOpacity, 1f, Time.deltaTime * passthroughChangeSpeedGlobal);
            passthroughStyler.UpdatePassthroughOpacity();
            
            if(passthroughStyler._savedOpacity >= (0.99f))
            {
                passthroughStyler._savedOpacity = 1f;
                passthroughStyler.UpdatePassthroughOpacity();

                passthroughOpen = false;

            }

           
            Debug.Log("going up");

        }

        if (!passthroughOpen && passthroughClose && (passthroughStyler._savedOpacity >= 0f))
        {
            startOpacity = passthroughStyler._savedOpacity;

            passthroughStyler._savedOpacity = Mathf.Lerp(startOpacity, 0f, Time.deltaTime * passthroughChangeSpeedGlobal);
            passthroughStyler.UpdatePassthroughOpacity();
            
            Debug.Log("going down " + a);

            if (passthroughStyler._savedOpacity <= 0.01f)

            {
                passthroughStyler._savedOpacity = 0;
                passthroughStyler.UpdatePassthroughOpacity();

                passthroughClose = false;
            }
        }

    }
    /*
    public void FadeOpacityToOne(float passthroughChangeSpeed)
    {
        float t = 0f;
        float startOpacity = 0f;

        while (t < 1f)
        {
            t += Time.deltaTime * passthroughChangeSpeed;
            float newBrightness = Mathf.Lerp(startOpacity, 1f, t);
            passthroughStyler._savedOpacity = newBrightness;
            passthroughStyler.UpdatePassthroughOpacity();

        }
    }*/

   
    public void FadeClientOpacityOneButton(float passthroughChangeSpeed)
    {
        startOpacity = passthroughStyler._savedOpacity;
        passthroughChangeSpeedGlobal = passthroughChangeSpeed;
        passthroughOpen = true;
        passthroughClose = false;

        photonView.RPC("StartFadeOpacityToOne", RpcTarget.All, passthroughChangeSpeed);
    }
    public void FadeClientOpacityZeroButton(float passthroughChangeSpeed)
    {
        startOpacity = passthroughStyler._savedOpacity;
        passthroughChangeSpeedGlobal = passthroughChangeSpeed;
        passthroughOpen = false;
        passthroughClose = true;
        photonView.RPC("StartFadeOpacityToZero", RpcTarget.All, passthroughChangeSpeed);
    }
    

    [PunRPC]
    public void StartFadeOpacityToOne(float passthroughChangeSpeed)
    {
       if (PhotonNetwork.LocalPlayer.NickName[0]+"" != "T")
        {
            startOpacity = passthroughStyler._passthroughLayer.textureOpacity;
            passthroughChangeSpeedGlobal = passthroughChangeSpeed;
            passthroughOpen = true;
            passthroughClose = false;
            Debug.Log("passthrough fading in");

     }


        // StartCoroutine(FadeOpacityToOne());
    }

    [PunRPC]
    public void StartFadeOpacityToZero(float passthroughChangeSpeed)
    {
      if (PhotonNetwork.LocalPlayer.NickName[0] + "" != "T")
       {
            startOpacity = passthroughStyler._passthroughLayer.textureOpacity;
            passthroughChangeSpeedGlobal = passthroughChangeSpeed;
            passthroughOpen = false;
            passthroughClose = true;
            Debug.Log("passthrough fading out");


        }
    }

    /*
    [PunRPC]
    public void StartFadeBrightnessToValue(float targetValue)
    {
        StartCoroutine(FadeBrightnessToValue(targetValue));
    }

    [PunRPC]
    public void StartFadeBrightnessToSavedValue(float targetValue)
    {
        StartCoroutine(FadeBrightnessToSavedValue(targetValue));
    }*/
}