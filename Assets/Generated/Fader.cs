using UnityEngine;
using Photon.Pun;
using System.Collections; // Add this line to fix the error

public class Fader : MonoBehaviourPun
{
    [Tooltip("Speed at which the alpha changes")]
    //00public float fadeSpeed = 1f;

    private SpriteRenderer spriteRenderer;
    private SpriteRenderer spriteRendererWhite;
    private SpriteRenderer spriteRendererCloud;


    public GameObject b;
    public GameObject c;
    public GameObject radiation;

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
        spriteRendererCloud = radiation.GetComponent<SpriteRenderer>();


        // }
    }
    bool fadeOut = false;
    bool fadeIn = false;

    bool fadeOutWhite = false;
    bool fadeInWhite = false;
    float fadeSpeedGlobal;


    private void Start()
    {
        fadeOut = false;
        fadeIn = false;
        fadeOutWhite = false;
       fadeInWhite = false;

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




    private void FixedUpdate()
    {
        if(fadeOut)
        {
            
            ChangeAlphaCoroutine(0f);

        }
        
        if(fadeIn)
        {
           
            ChangeAlphaCoroutine(1f);

        }

        if(fadeOutWhite)
        {
            ChangeAlphaCoroutine(0f);


        }

        if (fadeInWhite)
        {
            ChangeAlphaCoroutine(1f);


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


        if(fadeInWhite)
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

}