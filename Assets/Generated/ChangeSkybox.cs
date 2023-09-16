using UnityEngine;
using UnityEngine.Serialization;
using Photon.Pun;
using Photon.Realtime;

public class ChangeSkybox : MonoBehaviourPunCallbacks
{
    [Tooltip("The skybox material to use for the server and clients.")]
    public GameObject skyboxSphere;

    public void ChangeSceneSkybox(string materialName)
    {
        Material newSkyboxMaterial = Resources.Load<Material>(materialName);

        skyboxSphere.gameObject.GetComponent<MeshRenderer>().material = newSkyboxMaterial;
            photonView.RPC("ChangeSkyboxRPC", RpcTarget.AllBuffered, materialName);
        
    }

    [PunRPC]
    private void ChangeSkyboxRPC(string materialName)
    {
        Material newSkyboxMaterial = Resources.Load<Material>(materialName);
        skyboxSphere.gameObject.GetComponent<MeshRenderer>().material = newSkyboxMaterial;
    }
}