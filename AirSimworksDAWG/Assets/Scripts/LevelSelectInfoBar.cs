using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LevelSelectInfoBar : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI gameName, gameDesc, gameCompetency1, gameCompetency2;

    void Start()
    {
        SetArenaInfo();
    }

    //Could add refences to respective text if want to not have to update changes twice

    public void SetArenaInfo()
    {
        SetInfo("Arena", "", "Strategic Thinking", "Decision Making");
    }

    public void SetRecruitInfo()
    {
        SetInfo("Recruit", "", "Fosters Inclusion", "Teamwork");
    }

    public void SetMazeInfo()
    {
        SetInfo("Maze", "", "Flexibility", "Communication");
    }

    public void SetCarsInfo()
    {
        SetInfo("Cars", "", "Self-Control", "Accountability");
    }

    public void SetTowerDefenseInfo()
    {
        SetInfo("Tower Defense", "", "Resource Management", "Analytical Thinking");
    }

    void SetInfo(string name, string description, string competency1, string competency2)
    {
        gameName.text = name;
        gameDesc.text = description;
        gameCompetency1.text = competency1;
        gameCompetency2.text = competency2;
    }
}
