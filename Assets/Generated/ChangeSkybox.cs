using UnityEngine;
using UnityEngine.Serialization;
using Photon.Pun;
using Photon.Realtime;

public class ChangeSkybox : MonoBehaviourPunCallbacks
{
    [Tooltip("The skybox material to use for the server and clients.")]
    public Material skyboxMaterial;

    private void Start()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            photonView.RPC("ChangeSkyboxRPC", RpcTarget.AllBuffered, skyboxMaterial.name);
        }
    }

    [PunRPC]
    private void ChangeSkyboxRPC(string materialName)
    {
        Material newSkyboxMaterial = Resources.Load<Material>(materialName);
        RenderSettings.skybox = newSkyboxMaterial;
    }
}