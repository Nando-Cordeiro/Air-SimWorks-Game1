using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviour
{
    //public RaceManager rm;

    public enum GameType
    {
        Arena,
        Recruit,
        Maze,
        Cars,
        TowerDefense,
    }

    public GameType gameType = GameType.Arena;

    public Transform[] spawns;

    public PhotonRoom PR;

    public float timeBeforeStart = 30f;

    GameObject g;

    public bool[] playersFinished;

    public GameObject startCam;

    public List<GameObject> players;

    public float gameLength = (60 * 3);

    // Start is called before the first frame update
    void Start()
    {
        PR = FindObjectOfType<PhotonRoom>();

        //rm.players.Clear();
        //rm.players = new List<GameObject>();

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        GetPlayer();

        StartCoroutine(StartTheGame());
    }

    private void Update()
    {
        /*for (int i = 0; i < rm.players.Count; i++)
        {
            if (rm.players[i] != null)
            {
                if (rm.players[i].GetComponent<PlayerController>().finishedRace) playersFinished[i] = true;
            }
        }*/

        //playersFinished = rm.playersFinished; // test case 1

        if (GameObject.FindGameObjectsWithTag("Player").Length > PR.playersInRoom) // keep the room clear
        {
            foreach (GameObject p in GameObject.FindGameObjectsWithTag("Player"))
            {
                if (p != null)
                {
                    PhotonView view = p.GetComponent<PhotonView>();
                    if (view.IsMine && view.ViewID > g.GetComponent<PhotonView>().ViewID) Destroy(p);
                }
            }
        }
    }

    public IEnumerator StartTheGame()
    {
        GetPlayer();

        //while (FindObjectsOfType<CameraController>().Length < PR.playersInRoom) yield return null;

        yield return new WaitForSeconds(3f);

        if (g == null && PR.playersInRoom > FindObjectsOfType<CameraController>().Length)
        {
            if (!GameObject.FindGameObjectWithTag("Player").GetComponent<PhotonView>().IsMine) PR.RPC_CreatePlayer();
        }

        GetPlayer();

        // move player to coresponding spawn point
        if (g.GetComponent<PhotonView>().IsMine)
        {
            CharacterController c = g.GetComponent<CharacterController>();

            if (c != null)
            {
                c.enabled = false;

                g.transform.position = spawns[PR.myNumberInRoom].position;
                g.transform.rotation = spawns[PR.myNumberInRoom].rotation;


                c.enabled = true;
                g.GetComponent<playerController>().enabled = true;
                g.GetComponentInChildren<CameraController>().enabled = true;
                g.GetComponentInChildren<Camera>().enabled = true;

                g.transform.GetChild(2).gameObject.SetActive(true);
            }
            else
            {
                g.transform.position = spawns[PR.myNumberInRoom].position;
                g.transform.rotation = spawns[PR.myNumberInRoom].rotation;
            }
        }

        // disable the start view cam
        startCam.SetActive(false);

        // let players start
        EnableMe();
    }

    void GetPlayer()
    {
        if (g == null) //  || !PR.players.Contains(g)
        {
            foreach (GameObject p in GameObject.FindGameObjectsWithTag("Player"))
            {
                if (p.GetComponent<PhotonView>().IsMine) g = p.gameObject;
            }

            players.Add(g);
        }
    }

    void EnableMe()
    {
        foreach (CameraController p in FindObjectsOfType<CameraController>())
        {
            if (p.view.IsMine)
            {
                p.GetComponent<CameraController>().enabled = true;
                p.GetComponent<Camera>().enabled = true;
                p.GetComponent<Gun>().enabled = true;
                p.GetComponent<AudioListener>().enabled = true;
            }
        }
    }

    public void DissconnectPlayer()
    {
        StartCoroutine(DissconectAndLoad());
    }

    public IEnumerator DissconectAndLoad()
    {
        PhotonNetwork.Disconnect();
        PR.playersInGame--;

        while (PhotonNetwork.IsConnected) yield return null; // wait

        SceneManager.LoadScene(MultiplayerSetting.multiplayerSetting.menuScene);

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting) // sends data to other clients
        {
            //stream.SendNext(playersFinished);
        }
        else // recieves data from other clients (needs to be in same order to work)
        {
            //playersFinished = (bool[])stream.ReceiveNext();
        }
    }
}
