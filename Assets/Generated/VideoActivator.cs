using UnityEngine;
using Photon.Pun;

public class VideoActivator : MonoBehaviourPun
{
    [SerializeField]
    [Tooltip("The game object to activate")]
    private GameObject[] objectsToActivate;

    int videoCount = 3;
    private void Start()
    {
        videoCount = 3;
        objectsToActivate = new GameObject[3];
            objectsToActivate = GameObject.FindGameObjectsWithTag("VideoPlayer");
        
    }
    public void ActivateObject()
    {
        //if getting object at start not working we can move it to here
        for(int i = 0; i < videoCount; i++)
        {
            objectsToActivate[i].gameObject.SetActive(!objectsToActivate[i].activeSelf);
        }
        // objectToActivate.SetActive(true);
        photonView.RPC("ActivateObjectRPC", RpcTarget.All);
    }

    [PunRPC]
    private void ActivateObjectRPC()
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