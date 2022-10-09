using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointsGiver : MonoBehaviour
{
    public StartGame gameType;
    public DataManager dataManager;

    public int totalPoints;
    int points;

    private void Start()
    {
        gameType = FindObjectOfType<StartGame>();
        dataManager = FindObjectOfType<DataManager>();
    }

    // Update is called once per frame
    void Update()
    {
        points = 0;

        foreach (Gun g in FindObjectsOfType<Gun>())
        {
            points += g.totalPoints;
        }

        if (points > totalPoints) totalPoints = points;
    }

    public void GiveOutPoints()
    {
        if (dataManager == null)
        {
            Debug.LogWarning("Error! data manager not present in scene");

            return;
        }

        if (gameType.gameType == StartGame.GameType.Arena)
        {
            dataManager.strategicThinkingLvl += totalPoints / 2;
            dataManager.decisionMakingLvl += totalPoints / 2;
        }
        else if (gameType.gameType == StartGame.GameType.Recruit)
        {
            // TODO: change these
            dataManager.strategicThinkingLvl += totalPoints / 2;
            dataManager.decisionMakingLvl += totalPoints / 2;
        }
        else if (gameType.gameType == StartGame.GameType.Recruit)
        {
            // TODO: change these
            dataManager.strategicThinkingLvl += totalPoints / 2;
            dataManager.decisionMakingLvl += totalPoints / 2;
        }
        else if (gameType.gameType == StartGame.GameType.Recruit)
        {
            // TODO: change these
            dataManager.strategicThinkingLvl += totalPoints / 2;
            dataManager.decisionMakingLvl += totalPoints / 2;
        }
        else if (gameType.gameType == StartGame.GameType.Recruit)
        {
            // TODO: change these
            dataManager.strategicThinkingLvl += totalPoints / 2;
            dataManager.decisionMakingLvl += totalPoints / 2;
        }

        dataManager.UpdateData();
    }
}
