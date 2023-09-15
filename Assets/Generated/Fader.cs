using UnityEngine;
using Photon.Pun;
using System.Collections; // Add this line to fix the error

public class Fader : MonoBehaviourPun
{
    [Tooltip("Speed at which the alpha changes")]
    public float fadeSpeed = 1f;

    private SpriteRenderer spriteRenderer;
    public GameObject b;

    private void Awake()
    {
      /*  PhotonView a = gameObject.AddComponent<PhotonView>();
        a.ViewID = 666;
       /* if (PhotonNetwork.LocalPlayer.NickName[0] + "" != "T")
        {*/
            b = GameObject.FindGameObjectWithTag("BlackFader");
            spriteRenderer = b.GetComponent<SpriteRenderer>();

       // }
    }
    bool fadeOut = false;
    float fadeSpeedGlobal;
    bool fadeIn = false;

    private void Start()
    {
        fadeOut = false;
        fadeIn = false;

    }

    [PunRPC]
    private void FadeOutAlpha(float fadeSpeed)
    {

        if (PhotonNetwork.LocalPlayer.NickName[0] + "" != "T")
        {
            fadeOut = true;
            fadeIn = false;

            fadeSpeedGlobal = fadeSpeed;
        }
    
        //if getting object at start not working we can move it to here
        
    }


    [PunRPC]

    private void FadeInAlpha(float fadeSpeed)
    {

        if (PhotonNetwork.LocalPlayer.NickName[0] + "" != "T")
        {
            fadeIn = true;
            fadeOut = false;

            fadeSpeedGlobal = fadeSpeed;
        }

        //if getting object at start not working we can move it to here

    }

    public void FadeOutButton(float fadeSpeed)
    {
        fadeOut = true;
        fadeIn = false;
        fadeSpeedGlobal = fadeSpeed;
        photonView.RPC("FadeOutAlpha", RpcTarget.All, fadeSpeed);
    }

    public void FadeInButton(float fadeSpeed)
    {
        fadeIn = true;
        fadeOut = false;
        fadeSpeedGlobal = fadeSpeed;
        photonView.RPC("FadeInAlpha", RpcTarget.All, fadeSpeed);
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