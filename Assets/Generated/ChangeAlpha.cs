using UnityEngine;
using Photon.Pun;

public class ChangeAlpha : MonoBehaviourPun
{
    [Tooltip("The alpha value to set for the sprite renderer")]
    public float alphaValue = 0.5f;

    private SpriteRenderer spriteRenderer;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    [PunRPC]
    private void SetAlpha(float alpha)
    {
        Color color = spriteRenderer.color;
        color.a = alpha;
        spriteRenderer.color = color;
    }

    public void ChangeAlphaValue()
    {
        photonView.RPC("SetAlpha", RpcTarget.AllBuffered, alphaValue);
    }
}