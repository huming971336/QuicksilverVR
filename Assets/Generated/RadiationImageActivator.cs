using UnityEngine;
using Photon.Pun;

public class RadiationImageActivator : MonoBehaviourPun
{
    [SerializeField]
    [Tooltip("The game object to activate")]
    private GameObject objectToActivate;
    private void Start()
    {
        if (PhotonNetwork.LocalPlayer.NickName[0] + "" != "T")
        {
            objectToActivate = GameObject.FindGameObjectWithTag("PNGAnim");
        }
    }
    public void ActivateObject()
    {
        //if getting object at start not working we can move it to here

        // objectToActivate.SetActive(true);
        photonView.RPC("ActivateObjectRPC", RpcTarget.All);
    }

    [PunRPC]
    private void ActivateObjectRPC()
    {
        if (PhotonNetwork.LocalPlayer.NickName[0] + "" != "T")
        {
            objectToActivate.SetActive(true);

        }
    }
}