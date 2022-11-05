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

        foreach (Gun g in FindObjectsOfType<Gun>())  points += g.totalPoints;

        foreach (CarGameManager cgm in FindObjectsOfType<CarGameManager>()) FindObjectOfType<CarGameManager>().points += points;

        foreach (MazeGameManager mgm in FindObjectsOfType<MazeGameManager>()) FindObjectOfType<MazeGameManager>().points += points;

        foreach (RecruitManager rgm in FindObjectsOfType<RecruitManager>()) FindObjectOfType<RecruitManager>().points += points; 

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
            dataManager.fostersInclusionLvl += totalPoints / 2;
            dataManager.teamworkLvl += totalPoints / 2;
        }
        else if (gameType.gameType == StartGame.GameType.Maze)
        {
            // TODO: change these
            dataManager.flexibilityLvl += totalPoints / 2;
            dataManager.communicationLvl += totalPoints / 2;
        }
        else if (gameType.gameType == StartGame.GameType.Cars)
        {
            // TODO: change these
            dataManager.selfControlLvl += totalPoints / 2;
            dataManager.accountabilityLvl += totalPoints / 2;
        }
        else if (gameType.gameType == StartGame.GameType.TowerDefense)
        {
            // TODO: change these
            dataManager.strategicThinkingLvl += totalPoints / 2;
            dataManager.decisionMakingLvl += totalPoints / 2;
        }
        else Debug.LogError("Game type does not match level");

        dataManager.UpdateData();
    }
}
