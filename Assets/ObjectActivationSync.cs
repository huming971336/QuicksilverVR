using UnityEngine;
using UnityEngine.Serialization;
using PhotonPun = Photon.Pun;
using PhotonRealtime = Photon.Realtime;

public class ObjectActivationSync : MonoBehaviour
{
    [Tooltip("Tag of the Photon Network object to delete.")] [SerializeField]
    private string objectTag = "scenes";

    private void Start()
    {
        // Set the GameObject to be active for the local player.
    }

    public void ObjectToSpawn(GameObject obj)
    {
        ActivateObject(obj);
    }

    private void ActivateObject(GameObject Prefab)
    {
        // Call this method to enable the GameObject across the network.
        //photonView.RPC("SetActiveRPC", RpcTarget.All, true);
        var objectToSpawn =
            PhotonPun.PhotonNetwork.Instantiate(Prefab.name, Prefab.transform.position, Prefab.transform.rotation);
    }

   
    public void DeletePhotonObjectWithTag()
    {
        var objectsToDelete = GameObject.FindGameObjectsWithTag(objectTag);
        foreach (var obj in objectsToDelete)
        {
            PhotonPun.PhotonNetwork.Destroy(obj);
        }
    }
}