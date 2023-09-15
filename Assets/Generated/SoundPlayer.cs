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
            AudioSource a= new AudioSource();
            a.spatialBlend = 0;
            a.PlayOneShot(introClip, 1.5f);
        }
    }

    [PunRPC]
    private void PlaySoundChamber()
    {
        if (PhotonNetwork.LocalPlayer.NickName[0] + "" != "T")
        {

            AudioSource a = new AudioSource();
            a.spatialBlend = 0;
            a.PlayOneShot(chamberClip, 1.5f);
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