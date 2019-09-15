using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;


public class Launcher : MonoBehaviourPunCallbacks
{
    public void Awake()
    {
        PhotonNetwork.AutomaticallySyncScene = true;
        Connect();
    }

    public override void OnConnectedToMaster()
    {
        // try to join random room
        Debug.Log("Connected");
        Join();

        base.OnConnectedToMaster();
    }


    // If we have joined room, then we change scene.
    public override void OnJoinedRoom()
    {
        StartGame();
        base.OnJoinedRoom();
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {

        Create();
        base.OnJoinRandomFailed(returnCode, message);
    }

    public void Connect()
    {
        Debug.Log("Trying To Connect...");
        PhotonNetwork.GameVersion = "0.0.0"; // game version, can only play with ppl that has same version
        PhotonNetwork.ConnectUsingSettings();
    }
    
    // Join match
    public void Join()
    {
        PhotonNetwork.JoinRandomRoom();
    }

    // Create a match
    public void Create()
    {
        PhotonNetwork.CreateRoom("");
    }

    public void StartGame()
    {
        // Only load game, if your the only player inn the scene, if your the
        // second player you don't need to load a level. The Photon network will 
        // auto join u to their scene
        if(PhotonNetwork.CurrentRoom.PlayerCount == 1)
        {
            PhotonNetwork.LoadLevel(2);
        }
    }
}
