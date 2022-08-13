using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Photon.Pun;

public class CreateAndJoinRooms : MonoBehaviourPunCallbacks
{
    public TMP_InputField raceNameText;
    public TextMeshProUGUI playerCountText;

    public GameObject menuHome;
    public GameObject menuLobby;

    public int playersInRoom;

    private void Update()
    {
        if (PhotonNetwork.CurrentRoom != null) playersInRoom = PhotonNetwork.CurrentRoom.PlayerCount;

        if (playerCountText != null) playerCountText.text = playersInRoom + "/6 Players";
    }

    public void CreateRoom()
    {
        PhotonNetwork.CreateRoom(raceNameText.text);
    }
    
    public void JoinRoom()
    {
        PhotonNetwork.JoinRoom(raceNameText.text);
    }

    public override void OnJoinedRoom()
    {
        menuHome.SetActive(false);
        menuLobby.SetActive(true);

        //PhotonNetwork.LoadLevel("TheRace");
    }

    public void StartGame()
    {
        PhotonNetwork.LoadLevel("TheRace");
    }
}
