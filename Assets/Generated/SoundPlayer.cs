using UnityEngine;
using Photon.Pun;

public class SoundPlayer : MonoBehaviourPun
{
    [Tooltip("The audio clip to play")]
    public AudioClip introClip;
    public AudioClip chamberClip;


    [PunRPC]
    private void PlaySoundIntro()
    {
        if (PhotonNetwork.LocalPlayer.NickName[0] + "" != "T")
        {
           AudioSource.PlayClipAtPoint(introClip, transform.position);
        }
    }

    [PunRPC]
    private void PlaySoundChamber()
    {
        if (PhotonNetwork.LocalPlayer.NickName[0] + "" != "T")
        {
            AudioSource.PlayClipAtPoint(chamberClip, transform.position);
        }
    }

    public void PlaySoundIntroOnAllClients()
    {
        photonView.RPC("PlaySoundIntro", RpcTarget.All);
    }

    public void PlaySoundChamberOnAllClients()
    {
        photonView.RPC("PlaySoundChamber", RpcTarget.All);
    }
}