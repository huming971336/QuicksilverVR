using UnityEngine;
using UnityEngine.Serialization;
using PhotonPun = Photon.Pun;
using PhotonRealtime = Photon.Realtime;

public class BoolsNetwork : MonoBehaviour
{
    public GameObject toActivatePrefab;

    [Tooltip("Parent transform for instantiated objects.")] [SerializeField]
    private Transform parentTransform;

    public void ActivateObject()
    {
        // Check if the current player is the owner of the GameObject (to prevent non-owners from activating it).
        GameObject objectToSpawn =
            PhotonPun.PhotonNetwork.Instantiate(toActivatePrefab.name, transform.position,
                transform.rotation);
        if (parentTransform != null)
        {
            objectToSpawn.transform.parent = parentTransform;
        }
    }
}