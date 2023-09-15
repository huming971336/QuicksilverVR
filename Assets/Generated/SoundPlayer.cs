using UnityEngine;
using Photon.Pun;

public class SoundPlayer : MonoBehaviourPun
{
    [Tooltip("The audio clip to play")]
    public AudioClip audioClip;

    [PunRPC]
    private void PlaySound()
    {
        AudioSource.PlayClipAtPoint(audioClip, transform.position);
    }

    public void PlaySoundOnAllClients()
    {
        photonView.RPC("PlaySound", RpcTarget.All);
    }
}