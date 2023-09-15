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
           AudioSource.PlayClipAtPoint(introClip, transform.position, 1.5f);
        }
    }

    [PunRPC]
    private void PlaySoundChamber()
    {
        if (PhotonNetwork.LocalPlayer.NickName[0] + "" != "T")
        {
            AudioSource.PlayClipAtPoint(chamberClip, transform.position, 1.5f);
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