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

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        GetPlayer();

        StartCoroutine(StartTheGame());
    }

    public IEnumerator StartTheGame()
    {
        GetPlayer();

        yield return new WaitForSeconds(3f);

        GetPlayer();

        // move player to coresponding spawn point
        if (g.GetComponent<PhotonView>().IsMine)
        {
            CharacterController c = g.GetComponent<CharacterController>();
            ArcadeCar ac = g.GetComponent<ArcadeCar>();


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
            else if (ac != null)
            {
                ac.enabled = false;

                g.transform.position = spawns[PR.myNumberInRoom].position;
                g.transform.rotation = spawns[PR.myNumberInRoom].rotation;

                ac.enabled = true;
                g.GetComponent<ArcadeCar>().enabled = true;
                g.GetComponent<Rigidbody>().isKinematic = false;
                g.transform.parent.GetChild(1).gameObject.SetActive(true);
                g.transform.parent.GetChild(2).gameObject.SetActive(true);
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

            if (g != null) players.Add(g);
            else
            {
                PR.RPC_CreatePlayer(); // creates player if none exists

                foreach (GameObject p in GameObject.FindGameObjectsWithTag("Player"))
                {
                    PhotonView pv = p.GetComponent<PhotonView>();

                    if (pv != null && pv.IsMine) g = p.gameObject;
                }

                players.Add(g);
            }
        }
    }

    void EnableMe()
    {
        foreach (CameraController p in FindObjectsOfType<CameraController>()) // check for arcade
        {
            if (p.view.IsMine)
            {
                p.GetComponent<CameraController>().enabled = true;
                p.GetComponent<Camera>().enabled = true;
                p.GetComponent<Gun>().enabled = true;
                p.GetComponent<AudioListener>().enabled = true;

                return;
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
}
