using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.UI;

public class MazeGameManager : MonoBehaviour
{
    [SerializeField] float gameTime;
    [SerializeField] float switchTime;
    [SerializeField] float drainRate;
    public PlayerCam playerCam;
    float gameTimer;
    float switchTimer;
    //public TextMeshProUGUI[] switchTimeTexts;
    [SerializeField] SpawnManager spawnManager;
    [SerializeField] TextMeshProUGUI timeText, pointsText;
    public TextMeshProUGUI switchTimeText;

    public GameObject[] spawnPoints;
    public StartGame sg;
    public MazeUI mazeUI;

    public int points;
    float m, s;
    private bool ended;

    private void Start()
    {
        sg = FindObjectOfType<StartGame>();

        if (sg.PR != null && sg.PR.myNumberInRoom % 2 == 1)
        {
            mazeUI.spectating = true;
        }

        m = sg.gameLength / 60;

        gameTimer = gameTime;
        switchTimer = switchTime;
    }
    private void Update()
    {
        UpdateGameTimer();
        UpdateSwitchTimer();
    }

    void UpdateSwitchTimer()
    {
        //update display 
        switchTimeText.text = ((int)switchTimer).ToString("F2");

        if(gameTimer >= 0)
        {
            //count down
            if (switchTimer >= 0)
            {
                switchTimer -= (Time.deltaTime * drainRate);
            }
            else
            {
                spawnManager.RandomizeMazeSection();
                playerCam.SwitchPlayersLocal();
                mazeUI.spectating = !mazeUI.spectating;
                switchTimer = switchTime;
            }
        }
        else
        {
            switchTimer = 0;
            //Disable controls / go back to menu somewhere
        }
    }

    void UpdateGameTimer()
    {
        if (m > -1) s -= Time.deltaTime;
        else
        {
            // end the game if time goes below 0
            if (!ended) EndGame();
        }

        if (s <= 0f)
        {
            m--;
            s = 60;
        }
    }

    private void OnGUI()
    {
        if (pointsText != null) pointsText.text = "Total points: " + points;
        if (timeText != null && s >= 10f) timeText.text = "Time remaining " + m + ":" + (int)s;
        else if (timeText != null && s < 10f) timeText.text = "Time remaining " + m + ":0" + (int)s;
    }

    void EndGame()
    {
        FindObjectOfType<PointsGiver>().GiveOutPoints();

        DataManager dm = FindObjectOfType<DataManager>();
        dm.lastGamesPoints = points; // set after every game

        // set per level
        dm.skill1 = DataManager.Skills.Flexibility;
        dm.skill2 = DataManager.Skills.Communication;

        if (!PhotonNetwork.IsMasterClient)
        {
            return;
        }

        ended = true;

        PhotonNetwork.LoadLevel("AfterGameLobby");
    }
}
