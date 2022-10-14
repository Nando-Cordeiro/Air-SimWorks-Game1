using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LobbyMenu : MonoBehaviour
{
    public PhotonRoom pr;

    [Header("UI")]
    public GameObject waiting;
    public GameObject host, levelSelect;
    public TextMeshProUGUI hostLevelText, waitingLevelText;


    void Awake()
    {
        waiting.SetActive(false);
        host.SetActive(false);

        pr = FindObjectOfType<PhotonRoom>();

        if (pr.myNumberInRoom == 1) host.SetActive(true);
        else waiting.SetActive(true);

        SetVariables();
    }

    // Update is called once per frame
    void Update()
    {
       if (pr == null) pr = FindObjectOfType<PhotonRoom>();
    }

    void SetVariables() // for parity (and so it works lol)
    {
        pr.gametypeWaitingText = waitingLevelText;
        pr.gametypeHostText = hostLevelText;
    }

    public void StartGame()
    {
        pr.StartGame();
    }

    public void CancelSearch()
    {
        if (pr.myNumberInRoom == 1)
        {
            pr.view.RPC("HostLeft", RpcTarget.All); // should kick everyone
        }
        else
        {
            PhotonNetwork.LeaveRoom();
            Destroy(pr);
            SceneManager.LoadScene(0);
        }
    }

    public void SetMPScene(int _mpScene)
    {
        FindObjectOfType<MultiplayerSetting>().multiplayerScene = _mpScene;

        //set top bar stuff / game name
        if (_mpScene == 1) pr.view.RPC("RPC_ChangeGametype", RpcTarget.All, "Arena");
        if (_mpScene == 2) pr.view.RPC("RPC_ChangeGametype", RpcTarget.All, "Recruit");
        if (_mpScene == 3) pr.view.RPC("RPC_ChangeGametype", RpcTarget.All, "Maze");
        if (_mpScene == 4) pr.view.RPC("RPC_ChangeGametype", RpcTarget.All, "Cars");
        if (_mpScene == 5) pr.view.RPC("RPC_ChangeGametype", RpcTarget.All, "Tower Defense");
    }

    public void ReturnButton()
    {
        levelSelect.SetActive(false);
        host.SetActive(true);
    }

    public void LevelSelectButton()
    {
        levelSelect.SetActive(true);
        host.SetActive(false);
    }

}
