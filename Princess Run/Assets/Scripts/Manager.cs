using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Photon.Pun;

public class Manager : MonoBehaviour
{
    public string player_prefab;
    public string dragon_prefab;
    public string knight_prefab;

    public Transform spawn_point;
    
    private void Start()
    {
        Spawn();
    }

    public void Spawn()
    {
        Debug.Log("Playerprefab spawned");

        if (Pref.hunter == false)
        {
            PhotonNetwork.Instantiate("Vivi_Prefab", spawn_point.position, spawn_point.rotation);
        }
        else
        {
            Vector3 spawnPost = spawn_point.position;
            spawnPost.x += (5 * PhotonNetwork.CurrentRoom.PlayerCount);
            spawnPost.y += 2;
            // Creating Knight
            PhotonNetwork.Instantiate("Knight2", spawnPost, spawn_point.rotation);
            Debug.Log("Making Knight");
        }

        if (!GameObject.Find("Dragon(Clone)"))
        PhotonNetwork.Instantiate("Dragon", GameObject.Find("Dragonpos").transform.position, GameObject.Find("Dragonpos").transform.rotation);
   
    }
}
