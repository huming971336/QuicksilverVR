using UnityEngine;
using Photon.Pun;
using System.Collections; // Add this line to fix the error

public class Fader : MonoBehaviourPun
{
    [Tooltip("Speed at which the alpha changes")]
    //00public float fadeSpeed = 1f;

    private SpriteRenderer spriteRenderer;
    private SpriteRenderer spriteRendererWhite;

    //fades white black
    public GameObject b;
    public GameObject c;
    bool fadeOut = false;
    bool fadeIn = false;

    bool fadeOutWhite = false;
    bool fadeInWhite = false;
    float fadeSpeedGlobal;

    public GameObject radiation;
    private SpriteRenderer spriteRendererRadiation;
    bool fadeOutRadiation = false;
    bool fadeInRadiation = false;





    private void Awake()
    {
      /*  PhotonView a = gameObject.AddComponent<PhotonView>();
        a.ViewID = 666;
       /* if (PhotonNetwork.LocalPlayer.NickName[0] + "" != "T")
        {*/
            b = GameObject.FindGameObjectWithTag("BlackFader");
            spriteRenderer = b.GetComponent<SpriteRenderer>();
            c = GameObject.FindGameObjectWithTag("WhiteFader");
        spriteRendererWhite = c.GetComponent<SpriteRenderer>();
        radiation = GameObject.FindGameObjectWithTag("PNGAnim");
        spriteRendererRadiation = radiation.GetComponent<SpriteRenderer>();


        // }
    }



    private void Start()
    {
        fadeOut = false;
        fadeIn = false;
        fadeOutWhite = false;
       fadeInWhite = false;
        fadeOutRadiation = false;
        fadeInRadiation = false;
    }


    private void FixedUpdate()
    {
        if (fadeOut)
        {

            ChangeAlphaCoroutine(0f);

        }

        if (fadeIn)
        {

            ChangeAlphaCoroutine(1f);

        }

        if (fadeOutWhite)
        {
            ChangeAlphaWhite(0f);


        }

        if (fadeInWhite)
        {
            ChangeAlphaWhite(1f);


        }

        if (fadeInRadiation)
        {
            ChangeAlphaRadiation(1f);

        }

        if (fadeOutRadiation)
        {
            ChangeAlphaRadiation(0f);


        }
    }
    [PunRPC]
    private void FadeOutAlpha(float fadeSpeed)
    {

       /* if (PhotonNetwork.LocalPlayer.NickName[0] + "" != "T")
        {*/
            fadeOut = true;
            fadeIn = false;

            fadeSpeedGlobal = fadeSpeed;
            Debug.Log("Fade out");

       // }

        //if getting object at start not working we can move it to here

    }


    [PunRPC]

    private void FadeInAlpha(float fadeSpeed)
    {

     /*   if (PhotonNetwork.LocalPlayer.NickName[0] + "" != "T")
        {*/
            fadeIn = true;
            fadeOut = false;

            fadeSpeedGlobal = fadeSpeed;
            Debug.Log("Fade in");
     //   }

        //if getting object at start not working we can move it to here

    }

    [PunRPC]

    private void FadeInAlphaWhite(float fadeSpeed)
    {

        /*   if (PhotonNetwork.LocalPlayer.NickName[0] + "" != "T")
           {*/
        fadeInWhite = true;
        fadeOutWhite = false;

        fadeSpeedGlobal = fadeSpeed;
        Debug.Log("Fade in");
        //   }

        //if getting object at start not working we can move it to here

    }

    [PunRPC]
    private void FadeOutAlphaWhite(float fadeSpeed)
    {

        /* if (PhotonNetwork.LocalPlayer.NickName[0] + "" != "T")
         {*/
        fadeOutWhite = true;
        fadeInWhite = false;

        fadeSpeedGlobal = fadeSpeed;
        Debug.Log("Fade out");

        // }

        //if getting object at start not working we can move it to here

    }


    public void FadeOutButton(float fadeSpeed)
    {
        fadeOut = true;
        fadeIn = false;
        fadeSpeedGlobal = fadeSpeed;
        photonView.RPC("FadeOutAlpha", RpcTarget.All, fadeSpeed);
    }

    public void FadeOutWhiteButton(float fadeSpeed)
    {
        
        fadeOutWhite = true;
        fadeInWhite = false;
        fadeSpeedGlobal = fadeSpeed;
        photonView.RPC("FadeOutAlphaWhite", RpcTarget.All, fadeSpeed);
    }

    public void FadeInButton(float fadeSpeed)
    {
        fadeIn = true;
        fadeOut = false;
        fadeSpeedGlobal = fadeSpeed;
        photonView.RPC("FadeInAlpha", RpcTarget.All, fadeSpeed);
    }

    public void FadeInWhiteButton(float fadeSpeed)
    {
        fadeInWhite = true;
        fadeOutWhite = false;
        fadeSpeedGlobal = fadeSpeed;
        photonView.RPC("FadeInAlphaWhite", RpcTarget.All, fadeSpeed);
    }


    public void FadeInRadiatonButton(float fadeSpeed)
    {
        fadeInRadiation = true;
        fadeOutRadiation = false;
        fadeSpeedGlobal = fadeSpeed;
        photonView.RPC("FadeInRadiation", RpcTarget.All, fadeSpeed);
    }
    public void FadeOutRadiationButton(float fadeSpeed)
    {

        fadeInRadiation = false;
        fadeOutRadiation = true;
        fadeSpeedGlobal = fadeSpeed;
        photonView.RPC("FadeOutRadiation", RpcTarget.All, fadeSpeed);
    }


    [PunRPC]
    private void FadeOutRadiation(float fadeSpeed)
    {

        /* if (PhotonNetwork.LocalPlayer.NickName[0] + "" != "T")
         {*/

        fadeInRadiation = false;
        fadeOutRadiation = true;
        fadeSpeedGlobal = fadeSpeed;
        Debug.Log("Fade out");

        // }

        //if getting object at start not working we can move it to here

    }
    [PunRPC]

    private void FadeInRadiation(float fadeSpeed)
    {

        /*   if (PhotonNetwork.LocalPlayer.NickName[0] + "" != "T")
           {*/
        fadeInRadiation = true;
        fadeOutRadiation = false;

        fadeSpeedGlobal = fadeSpeed;
        Debug.Log("Fade in");
        //   }

        //if getting object at start not working we can move it to here

    }
    private void ChangeAlphaRadiation(float targetAlpha)
    {

        if (fadeOutRadiation)
        {

            if (spriteRendererRadiation.color.a >= 0f)
            {
                float currentAlpha = spriteRendererRadiation.color.a;
                currentAlpha = Mathf.Lerp(currentAlpha, targetAlpha, fadeSpeedGlobal * Time.deltaTime);
                spriteRendererRadiation.color = new Color(spriteRendererRadiation.color.r, spriteRendererRadiation.color.g, spriteRendererRadiation.color.b, currentAlpha);


            }
            if (spriteRendererRadiation.color.a == 0f)

            {
                fadeOutRadiation = false;
            }
        }

        if (fadeInRadiation)
        {
            if (spriteRendererRadiation.color.a <= 1f)
            {
                float currentAlpha = spriteRendererRadiation.color.a;
                currentAlpha = Mathf.Lerp(currentAlpha, targetAlpha, fadeSpeedGlobal * Time.deltaTime);
                spriteRendererRadiation.color = new Color(spriteRendererRadiation.color.r, spriteRendererRadiation.color.g, spriteRendererRadiation.color.b, currentAlpha);

            }
            if (spriteRendererRadiation.color.a == 1f)
            {
                fadeInRadiation = false;
            }
        }

    }


        private void ChangeAlphaWhite(float targetAlpha)
         {

        if (fadeInWhite)
        {
            if (spriteRendererWhite.color.a <= 1f)
            {
                float currentAlpha = spriteRendererWhite.color.a;
                currentAlpha = Mathf.Lerp(currentAlpha, targetAlpha, fadeSpeedGlobal * Time.deltaTime);
                spriteRendererWhite.color = new Color(spriteRendererWhite.color.r, spriteRendererWhite.color.g, spriteRendererWhite.color.b, currentAlpha);

            }
            if (spriteRendererWhite.color.a == 1f)
            {
                fadeInWhite = false;
            }
        }

        if (fadeOutWhite)
        {

            if (spriteRendererWhite.color.a >= 0f)
            {
                float currentAlpha = spriteRenderer.color.a;
                currentAlpha = Mathf.Lerp(currentAlpha, targetAlpha, fadeSpeedGlobal * Time.deltaTime);
                spriteRendererWhite.color = new Color(spriteRendererWhite.color.r, spriteRendererWhite.color.g, spriteRendererWhite.color.b, currentAlpha);


            }
            if (spriteRendererWhite.color.a == 0f)

            {
                fadeOutWhite = false;
            }
        }


    }

        private void ChangeAlphaCoroutine(float targetAlpha)
    {

        if(fadeOut)
        {

            if (spriteRenderer.color.a >= 0f)
            {
                float currentAlpha = spriteRenderer.color.a;
                currentAlpha = Mathf.Lerp(currentAlpha, targetAlpha, fadeSpeedGlobal * Time.deltaTime);
                spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, currentAlpha);
                

            }
            if(spriteRenderer.color.a == 0f)
            
            {
                fadeOut = false;
            }
        }
        
        if(fadeIn)
        {
            if (spriteRenderer.color.a <= 1f)
            {
                float currentAlpha = spriteRenderer.color.a;
                currentAlpha = Mathf.Lerp(currentAlpha, targetAlpha, fadeSpeedGlobal * Time.deltaTime);
                spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, currentAlpha);

            }
            if (spriteRenderer.color.a == 1f)
            {
                fadeIn = false;
            }
        }




    }

}