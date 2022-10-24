using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.UI;

public class CarGameManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI timeText, pointsText;


    public int points;
    public List<int> pointPool = new List<int>();


    float m, s;
    private bool ended;

    void Start()
    {
        m = FindObjectOfType<StartGame>().gameLength/60;
    }

    private void Update()
    {
        UpdateGameTimer();
    }

    private void OnGUI()
    {
        if (pointsText != null) pointsText.text = "Total points: " + points;
        if (timeText != null && s >= 10f) timeText.text = "Time remaining " + m + ":" + (int)s;
        else if (timeText != null && s < 10f) timeText.text = "Time remaining " + m + ":0" + (int)s;
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

    void EndGame()
    {
        FindObjectOfType<PointsGiver>().GiveOutPoints();

        DataManager dm = FindObjectOfType<DataManager>();
        dm.lastGamesPoints = points; // set after every game

        // set per level
        dm.skill1 = DataManager.Skills.StrategicThinking;
        dm.skill2 = DataManager.Skills.DecisionMaking;

        if (!PhotonNetwork.IsMasterClient)
        {
            return;
        }

        ended = true;

        PhotonNetwork.LoadLevel("AfterGameLobby");
    }
}
