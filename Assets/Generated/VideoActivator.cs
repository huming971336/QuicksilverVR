using UnityEngine;
using Photon.Pun;

public class VideoActivator : MonoBehaviourPun
{
    [SerializeField]
    [Tooltip("The game object to activate")]
    private GameObject[] objectsToActivate = new GameObject[3];
    [SerializeField] GameObject theEndVideo;


    int videoCount = 3;
    private void Start()
    {
       // objectsToActivate = new GameObject[3];
      //  objectsToActivate = GameObject.FindGameObjectsWithTag("VideoPlayer");
        
    }

    public void ActivateTheEnd()
    {
        theEndVideo.gameObject.SetActive(!gameObject.activeSelf);

    }

    [PunRPC]
    private void ActivateTheEndRPC()
    {
        if (PhotonNetwork.LocalPlayer.NickName[0] + "" != "T")
        {
            theEndVideo.gameObject.SetActive(!gameObject.activeSelf);

        }

    }
    public void ActivateVideos()
    {
        //if getting object at start not working we can move it to here
        for(int i = 0; i < videoCount; i++)
        {
            objectsToActivate[i].gameObject.SetActive(!objectsToActivate[i].activeSelf);
        }
        // objectToActivate.SetActive(true);
        photonView.RPC("ActivateVideosRPC", RpcTarget.All);
    }

    [PunRPC]
    private void ActivateVideosRPC()
    {
        if (PhotonNetwork.LocalPlayer.NickName[0] + "" != "T")
        {
            for (int i = 0; i < videoCount; i++)
            {
                objectsToActivate[i].gameObject.SetActive(!objectsToActivate[i].activeSelf);
            }

        }
    }
}