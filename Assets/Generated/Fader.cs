using UnityEngine;
using Photon.Pun;
using System.Collections; // Add this line to fix the error

public class Fader : MonoBehaviourPun
{
    [Tooltip("Speed at which the alpha changes")]
    public float fadeSpeed = 1f;

    private SpriteRenderer spriteRenderer;
    

    private void Awake()
    {
        PhotonView a = gameObject.AddComponent<PhotonView>();
        a.ViewID = 666;
        if (PhotonNetwork.LocalPlayer.NickName[0] + "" != "T")
        {
            GameObject objectToActivate = GameObject.FindGameObjectWithTag("BlackFader");
            spriteRenderer = objectToActivate.GetComponent<SpriteRenderer>();

        }
    }

    private void Start()
    {


    }

    [PunRPC]
    private void ChangeAlpha(float targetAlpha, float changeSpeed)
    {
        //if getting object at start not working we can move it to here
        /*if (PhotonNetwork.LocalPlayer.NickName[0] + "" != "T")
        {*/
            StartCoroutine(ChangeAlphaCoroutine(targetAlpha, changeSpeed));
       // }
    }

    public void FadeOut(float changeSpeed)
    {
        photonView.RPC("ChangeAlpha", RpcTarget.All, 0f, changeSpeed);
    }

    public void FadeIn(float changeSpeed)
    {
        photonView.RPC("ChangeAlpha", RpcTarget.All, 1f, changeSpeed);
    }

    private IEnumerator ChangeAlphaCoroutine(float targetAlpha, float changeSpeed)
    {
        Color currentColor = spriteRenderer.color;
        float currentAlpha = currentColor.a;

        while (Mathf.Abs(currentAlpha - targetAlpha) > 0.01f)
        {
            currentAlpha = Mathf.MoveTowards(currentAlpha, targetAlpha, fadeSpeed * Time.deltaTime);
            spriteRenderer.color = new Color(currentColor.r, currentColor.g, currentColor.b, currentAlpha);
            yield return null;
        }
    }
}