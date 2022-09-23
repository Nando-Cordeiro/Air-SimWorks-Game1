using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using TMPro;
using System.Linq;

public class PhotonLobby : MonoBehaviourPunCallbacks
{
    public static PhotonLobby lobby;

    public GameObject homeUI;
    public GameObject searchUI;
    public GameObject loadingUI;

    public GameObject hostUI;
    public GameObject waitingUI;
    public GameObject levelSelectUI;
    public GameObject startUI;

    public TMP_InputField roomName;

    public int mpScene = 0;

    PhotonRoom pr;
    //MultiplayerSetting ms;

    private void Awake()
    {
        lobby = this;
        pr = FindObjectOfType<PhotonRoom>();
        //ms = FindObjectOfType<MultiplayerSetting>();
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
        //ms.delayStart = true;
        //PhotonNetwork.JoinRandomRoom();
        PhotonNetwork.JoinRoom(roomName.text);
    }
    
    public void SoloGame()
    {
        if (roomName.text == "") return;

        startUI.SetActive(false);

        //ms.delayStart = false;
        //ms.multiplayerScene = mpScene;

        PhotonNetwork.JoinRoom(roomName.text);
        //PhotonNetwork.JoinRandomRoom();

        Debug.Log("Room name = " + roomName.text);
    }

    public void JoinGame()
    {
        if (roomName.text == "") return;

        startUI.SetActive(false);

        PhotonNetwork.JoinRoom(roomName.text);

        Debug.Log("Room name = " + roomName.text);

        if (!waitingUI.activeSelf)
        {
            Debug.Log("No room existed");

            startUI.SetActive(true);
        }
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        Debug.Log("no available rooms");
        CreateRoom();
    }

    public void CreateRoom()
    {
        if (roomName.text == "") return;

        startUI.SetActive(false);

        FindObjectOfType<MultiplayerSetting>().delayStart = false;
        FindObjectOfType<MultiplayerSetting>().multiplayerScene = mpScene;

        Debug.Log("Created new room");
        //int randomRoomName = Random.Range(0, 10000);
        RoomOptions roomOps = new RoomOptions() { IsVisible = true, IsOpen = true, MaxPlayers = 6 };
        PhotonNetwork.CreateRoom(roomName.text, roomOps);

        hostUI.SetActive(true);
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

        if (pr.myNumberInRoom != 1)
        {
            waitingUI.SetActive(true);
        }
    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        Debug.Log("Failed to make room, probably already exists");
        CreateRoom();
    }

    public void CancelSearch()
    {
        hostUI.SetActive(false);
        waitingUI.SetActive(false);
        startUI.SetActive(true);
        PhotonNetwork.LeaveRoom();
        pr.inaRoom = false;
    }

    public void SetMPScene(int _mpScene)
    {
        FindObjectOfType<MultiplayerSetting>().multiplayerScene = _mpScene;


        //set top bar stuff / game name

    }

    public void ReturnButton()
    {
        levelSelectUI.SetActive(false);
        hostUI.SetActive(true);
    }

    public void LevelSelectButton()
    {
        levelSelectUI.SetActive(true);
        hostUI.SetActive(false);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
