using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviour
{
    //public RaceManager rm;

    public Transform[] spawns;

    public PhotonRoom PR;

    public float timeBeforeStart = 30f;

    GameObject g;

    public bool[] playersFinished;

    public GameObject startCam;

    // Start is called before the first frame update
    void Start()
    {
        PR = FindObjectOfType<PhotonRoom>();

        //rm.players.Clear();
        //rm.players = new List<GameObject>();

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        GetPlayer();

        StartCoroutine(StartTheRace());
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
    }

    public IEnumerator StartTheRace()
    {
        GetPlayer();
        //StartCoroutine(GetPlayersInGame());

        //while (FindObjectsOfType<CameraController>().Length < PR.playersInRoom) yield return null;

        yield return new WaitForSeconds(3f);

        if (g == null && PR.playersInRoom > FindObjectsOfType<CameraController>().Length)
        {
            PR.RPC_CreatePlayer();

            GetPlayer();
            //StartCoroutine(GetPlayersInGame());
        }
        else GetPlayer();

        // move player to coresponding spawn point
        g.transform.position = spawns[PR.myNumberInRoom].position;
        g.transform.rotation = spawns[PR.myNumberInRoom].rotation;

        // disable the start view cam
        startCam.SetActive(false);

        /*
        //if (g.GetComponent<PlayerController>().view.IsMine)
        g.GetComponent<PlayerController>().cam.SetActive(true);

        yield return new WaitForSeconds(raceCountDown);

        //if (g.GetComponent<PlayerController>().view.IsMine) 
        g.GetComponent<PlayerController>().canMove = true;

        foreach (PlayerController p in FindObjectsOfType<PlayerController>()) p.canMove = true;

        rm.ActivateRacers();*/
    }

    void GetPlayer()
    {
        if (g == null) //  || !PR.players.Contains(g)
        {
            foreach (GameObject p in GameObject.FindGameObjectsWithTag("Player"))
            {
                if (p.GetComponent<PhotonView>().IsMine) g = p.gameObject;
            }

            //rm.players.Add(g);
        }
    }

    IEnumerator GetPlayersInGame()
    {
        yield return new WaitForSeconds(5f);
        /*
        foreach (PlayerController p in FindObjectsOfType<PlayerController>())
        {
            if (!p.view.IsMine) FindObjectOfType<PhotonRoom>().players.Add(p.gameObject);
        }

        rm.players = FindObjectOfType<PhotonRoom>().players;*/
    }

    public void DissconnectPlayer()
    {
        StartCoroutine(DissconectAndLoad());
    }

    public IEnumerator DissconectAndLoad()
    {
        //if (g.GetComponent<PlayerController>().lap < 4)
        //{
        //    GameObject _g = PhotonNetwork.InstantiateRoomObject(Path.Combine("PhotonPrefabs", "AI Racer"), transform.position, transform.rotation, 0);
        //    AIRacer newAi = _g.GetComponent<AIRacer>();
        //    newAi.lap = g.GetComponent<PlayerController>().lap;
        //    newAi.totalCheckpointsPassed = g.GetComponent<PlayerController>().totalCheckpointsPassed;
        //    newAi.checkpointsPassed = g.GetComponent<PlayerController>().checkpointsPassed;

        //    foreach (PlayerController p in FindObjectsOfType<PlayerController>())
        //    {
        //        p.rm.racers.Add(_g);
        //    }
        //}

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
