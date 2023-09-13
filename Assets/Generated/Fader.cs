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
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    [PunRPC]
    private void ChangeAlpha(float targetAlpha)
    {
        StartCoroutine(ChangeAlphaCoroutine(targetAlpha));
    }

    public void FadeOut()
    {
        photonView.RPC("ChangeAlpha", RpcTarget.All, 0f);
    }

    public void FadeIn()
    {
        photonView.RPC("ChangeAlpha", RpcTarget.All, 1f);
    }

    private IEnumerator ChangeAlphaCoroutine(float targetAlpha)
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