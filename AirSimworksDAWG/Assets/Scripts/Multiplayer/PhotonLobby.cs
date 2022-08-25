using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class PhotonLobby : MonoBehaviourPunCallbacks
{
    public static PhotonLobby lobby;

    public GameObject homeUI;
    public GameObject searchUI;
    public GameObject loadingUI;

    PhotonRoom pr;

    private void Awake()
    {
        lobby = this;
        pr = FindObjectOfType<PhotonRoom>();
    }

    void Start()
    {
        //PhotonNetwork.ConnectToRegion("us");
        PhotonNetwork.ConnectUsingSettings();
    }

    private void Update()
    {
        //if (Input.GetKeyDown(KeyCode.Escape) && searchUI.activeSelf) CancelSearch();
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("Connected to Photon");
        PhotonNetwork.AutomaticallySyncScene = true;
        homeUI.SetActive(true);
        loadingUI.SetActive(false);
    }

    public void FindGame()
    {
        homeUI.SetActive(false);
        searchUI.SetActive(true);
        FindObjectOfType<MultiplayerSetting>().delayStart = true;
        PhotonNetwork.JoinRandomRoom();
    }
    
    public void SoloGame(int mpScene)
    {
        homeUI.SetActive(false);
        if (searchUI != null) searchUI.SetActive(true);
        FindObjectOfType<MultiplayerSetting>().delayStart = false;
        FindObjectOfType<MultiplayerSetting>().multiplayerScene = mpScene;
        PhotonNetwork.JoinRandomRoom();
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        Debug.Log("no available rooms");
        CreateRoom();
    }

    void CreateRoom()
    {
        Debug.Log("Created new room");
        int randomRoomName = Random.Range(0, 10000);
        RoomOptions roomOps = new RoomOptions() { IsVisible = true, IsOpen = true, MaxPlayers = 6 };
        PhotonNetwork.CreateRoom("Room" + randomRoomName, roomOps);
    }

    void CreateSoloRoom()
    {
        Debug.Log("Created new solo room");
        int randomRoomName = Random.Range(0, 10000);
        RoomOptions roomOps = new RoomOptions() { IsVisible = true, IsOpen = true, MaxPlayers = 6 };
        PhotonNetwork.CreateRoom("Room" + randomRoomName, roomOps);
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("Joined a room");
    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        Debug.Log("Failed to make room, probably already exists");
        CreateRoom();
    }

    public void CancelSearch()
    {
        searchUI.SetActive(false);
        homeUI.SetActive(true);
        PhotonNetwork.LeaveRoom();
        pr.inaRoom = false;
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
