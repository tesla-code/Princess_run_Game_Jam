using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Banana : MonoBehaviourPunCallbacks
{
    // i am a banana :)
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("layer is "+other.gameObject.layer);
        Debug.Log(other.name);
        if (other.name == "Vivi_Prefab(Clone)" && other.gameObject.layer == 12)
        {
            
            PhotonNetwork.Destroy(gameObject);
            Debug.Log("Player hit banana!");
            //other.GetComponent<Player>().HitByBanana();
            other.GetComponent<Player>().photonView.RPC("HitByBanana", RpcTarget.All);
        }
    }
}
