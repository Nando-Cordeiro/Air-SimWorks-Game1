using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class PhotonRoom : MonoBehaviourPunCallbacks, IInRoomCallbacks
{
    public static PhotonRoom room;
    public PhotonView view;

    public bool isGameLoaded;
    public int currentScene;
    public Player[] playerControllers;
    public List<GameObject> players;
    public int playersInRoom;
    public int myNumberInRoom;

    public int playersInGame;

    private bool readyToCount;
    private bool readyToStart;
    public float startingTime;
    private float lessThanMaxPlayers;
    private float atMaxPlayer;
    private float timeToStart;

    public bool inaRoom;

    [Header("UI")]
    public TextMeshProUGUI gametypeHostText;
    public TextMeshProUGUI gametypeWaitingText;

    [Header("References")]
    public TextMeshProUGUI playerCountText;
    public Image playerCountImage;

    private void Awake()
    {
        if (PhotonRoom.room == null)
        {
            PhotonRoom.room = this;
        }
        else
        {
            if (PhotonRoom.room != this)
            {
                Destroy(PhotonRoom.room.gameObject);
                PhotonRoom.room = this;
            }
        }
        DontDestroyOnLoad(this.gameObject);
    }

    public override void OnEnable()
    {
        base.OnEnable();

        PhotonNetwork.AddCallbackTarget(this);
        SceneManager.sceneLoaded += OnSceneFinishedLoading;
    }

    public override void OnDisable()
    {
        base.OnDisable();

        PhotonNetwork.RemoveCallbackTarget(this);
        SceneManager.sceneLoaded -= OnSceneFinishedLoading;
    }


    // Start is called before the first frame update
    void Start()
    {
        view = GetComponent<PhotonView>();
        readyToCount = false;
        readyToStart = false;
        lessThanMaxPlayers = startingTime;
        atMaxPlayer = 6;
        timeToStart = startingTime;

        //playerCountImage.fillAmount = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (MultiplayerSetting.multiplayerSetting.delayStart)
        {
            if (playersInRoom == 1)
            {
                RestartTimer();
            }

            if (!isGameLoaded)
            {
                if (readyToStart)
                {
                    atMaxPlayer -= Time.deltaTime;
                    lessThanMaxPlayers = atMaxPlayer;
                    timeToStart = atMaxPlayer;
                }
                else if (readyToCount)
                {
                    lessThanMaxPlayers -= Time.deltaTime;
                    timeToStart = lessThanMaxPlayers;
                }
                //Debug.Log("Time to start: " + timeToStart);

                if (timeToStart <= 0)
                {
                    StartGame();
                }
            }
        }
    }

    private void OnGUI()
    {
        if (inaRoom && playerCountText != null)
        {
            playerCountText.text = playersInRoom + " / 6";
            playerCountImage.fillAmount = 0f;
            //for (int i = 0; i < playersInRoom; i++) playerCountImage.fillAmount += (1f / 6f);
        }
    }

    public override void OnJoinedRoom()
    {
        base.OnJoinedRoom();
        Debug.Log("Joined a room");
        inaRoom = true;

        playerControllers = PhotonNetwork.PlayerList;
        playersInRoom = playerControllers.Length;
        myNumberInRoom = playersInRoom;
        PhotonNetwork.NickName = "Player " + myNumberInRoom.ToString();

        if (MultiplayerSetting.multiplayerSetting.delayStart)
        {
            // show player count
            if (playerCountText != null) playerCountText.text = playersInRoom + " / 6";
            //playerCountImage.fillAmount += (1 / 6);

            if (playersInRoom > 1)
            {
                readyToCount = true;
            }
            if (playersInRoom == MultiplayerSetting.multiplayerSetting.maxPlayers)
            {
                readyToStart = true;
                if (!PhotonNetwork.IsMasterClient) return;
                PhotonNetwork.CurrentRoom.IsOpen = false;
            }
        }
        else
        {
            //StartGame();
        }
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        base.OnPlayerEnteredRoom(newPlayer);
        Debug.Log("New player here");
        playerControllers = PhotonNetwork.PlayerList;
        playersInRoom++;

        if (MultiplayerSetting.multiplayerSetting.delayStart)
        {
            if (playersInRoom > 1)
            {
                readyToCount = true;
            }
            if (playersInRoom == MultiplayerSetting.multiplayerSetting.maxPlayers)
            {
                readyToStart = true;
                if (!PhotonNetwork.IsMasterClient) return;
                PhotonNetwork.CurrentRoom.IsOpen = false;
            }
        }
    }

    public void StartGame()
    {
        isGameLoaded = true;

        if (!PhotonNetwork.IsMasterClient)
        {
            return;
        }
        
        if (MultiplayerSetting.multiplayerSetting.delayStart)
        {
            PhotonNetwork.CurrentRoom.IsOpen = false;
        }

        PhotonNetwork.LoadLevel(MultiplayerSetting.multiplayerSetting.multiplayerScene);
    }

    void RestartTimer()
    {
        lessThanMaxPlayers = startingTime;
        timeToStart = startingTime;
        atMaxPlayer = 6;
        readyToCount = false;
        readyToStart = false;
    }

    void OnSceneFinishedLoading(Scene scene, LoadSceneMode mode)
    {
        currentScene = scene.buildIndex;

        if (currentScene == MultiplayerSetting.multiplayerSetting.multiplayerScene)
        {
            isGameLoaded = true;

            if (MultiplayerSetting.multiplayerSetting.delayStart)
            {
                view.RPC("RPC_LoadedGameScene", RpcTarget.MasterClient);
            }
            else
            {
                //RPC_CreatePlayer();
                view.RPC("RPC_LoadedGameScene", RpcTarget.MasterClient);
            }
        }
    }

    [PunRPC]
    public void RPC_LoadedGameScene()
    {
        playersInGame++;

        if (playersInGame == PhotonNetwork.PlayerList.Length)
        {
            //view.RPC("RPC_CreatePlayer", RpcTarget.All);
        }
    }

    [PunRPC]
    public void RPC_CreatePlayer()
    {
        string playerType = SceneManager.GetActiveScene().name;

        Debug.Log("Creating player");
        GameObject g = null;

        if (playerType == "Arena")              g = PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "Player"), transform.position, Quaternion.identity, 0);
        else if (playerType == "Recruit")       g = PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "Player Variant"), transform.position, Quaternion.identity, 0);
        else if (playerType == "Maze")          g = PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "MazePlayer"), transform.position, Quaternion.identity, 0);
        else if (playerType == "Car")           g = PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "CarGamePlayer"), transform.position, Quaternion.identity, 0);
        else if (playerType == "TowerDefense")  g = PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "Player"), transform.position, Quaternion.identity, 0);
        else Debug.LogError("@ty your dumbass spelt shit wrong again >:(");
        //if (players.Count <= playersInRoom) 
        players.Add(g);
        Debug.Log("Added new player");
    }

    [PunRPC]
    public void RPC_ChangeGametype(string gametype)
    {
        gametypeHostText.text = "Game Type: " + gametype;
        gametypeWaitingText.text = "Game Type: " + gametype;
    }

    [PunRPC]
    public void HostLeft()
    {
        PhotonNetwork.LoadLevel("Main Menu");
        PhotonNetwork.LeaveRoom();
        Destroy(room.gameObject);
        //SceneManager.LoadScene("Main Menu");
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        base.OnPlayerLeftRoom(otherPlayer);
        Debug.Log(otherPlayer.NickName + "Has left the game");
        playersInRoom--;
    }
}
