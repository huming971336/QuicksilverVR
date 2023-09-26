using UnityEngine;
using Photon.Pun;

public class ChcekAppearance : MonoBehaviourPun, IPunObservable
{
    private bool Active;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        
    }
}
