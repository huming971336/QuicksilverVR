using UnityEngine;
using Photon.Pun;

public class ObjectActivationSync : MonoBehaviourPun
{
    public  GameObject scene;
    private void Start()
    {
        // Set the GameObject to be active for the local player.
        scene.SetActive(false);
    }

    public void ActivateObject()
    {
        
        // Call this method to enable the GameObject across the network.
        photonView.RPC("SetActiveRPC", RpcTarget.All, true);
    }

    public void DeactivateObject()
    {
        // Call this method to disable the GameObject across the network.
        photonView.RPC("SetActiveRPC", RpcTarget.All, false);
    }

    // RPC method to set the active state of the GameObject for all clients.
    [PunRPC]
    private void SetActiveRPC(bool isActive)
    {
        // Set the active state of the GameObject based on the received value.
        scene.SetActive(isActive);
    }
}