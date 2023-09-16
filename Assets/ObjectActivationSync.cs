using UnityEngine;
using UnityEngine.Serialization;
using PhotonPun = Photon.Pun;
using PhotonRealtime = Photon.Realtime;
using UnityEngine;
using System.Collections;
using Photon.Pun;

public class ObjectActivationSync : MonoBehaviour
{
    [Tooltip("Tag of the Photon Network object to delete.")] [SerializeField]
    private string objectTag = "scenes";
    public GameObject[] objectsToActivate;
    public GameObject[] objectsToActivatethorns;
    public float activationDelay = 3f;

    private int currentIndex = 0;
    private int currentIndexThorn=0;


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
    public void ObjectInventinghope()
    {
        StartCoroutine(ActivateObjectsWithDelay());
    }
    
    private IEnumerator ActivateObjectsWithDelay()
    {
        
        Debug.Log(currentIndex);
        while (currentIndex < objectsToActivate.Length)
        {
            GameObject currentObject = objectsToActivate[currentIndex];
            
            // Check if the object is not null and has a PhotonView.
            if (currentObject != null && currentObject.GetComponent<PhotonView>() != null)
            {
                // Activate the object locally (on all clients).
                PhotonNetwork.Instantiate(currentObject.name, currentObject.transform.position, currentObject.transform.rotation);
            }

            currentIndex++;

            // Wait for the next activation delay.
            yield return new WaitForSeconds(activationDelay);
        }
    }
    public void ObjectThorns()
    {
        StartCoroutine(ActivateThornsObjectsWithDelay());
    }

    
    private IEnumerator ActivateThornsObjectsWithDelay()
    {
        
        while (currentIndexThorn < objectsToActivatethorns.Length)
        {
            GameObject currentObject = objectsToActivatethorns[currentIndex];
            
            // Check if the object is not null and has a PhotonView.
            if (currentObject != null && currentObject.GetComponent<PhotonView>() != null)
            {
                // Activate the object locally (on all clients).
                PhotonNetwork.Instantiate(currentObject.name, currentObject.transform.position, currentObject.transform.rotation);
            }

            currentIndex++;

            // Wait for the next activation delay.
            yield return new WaitForSeconds(activationDelay);
        }
    }


    public void DeletePhotonObjectWithTag()
    {
        var objectsToDelete = GameObject.FindGameObjectsWithTag(objectTag);
        foreach (var obj in objectsToDelete)
        {
            PhotonPun.PhotonNetwork.Destroy(obj);
        }
    }
    public void DeletePhotonObject(GameObject deleteObject)
    {
        
        
            PhotonPun.PhotonNetwork.Destroy(deleteObject);
       
    }
}