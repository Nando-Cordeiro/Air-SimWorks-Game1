using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements.Experimental;

public class DataManager : MonoBehaviour
{
    static DataManager dm;

    public int lastGamesPoints;

    public enum Skills
    {
        StrategicThinking,
        DecisionMaking,
        Flexibility,
        Communication,
        FostersInclusion,
        Teamwork,
        SelfControl,
        Accountability,
        ResourceManagement,
        AnalyticalThinking,
    }

    public Skills skill1, skill2;

    [Header("Developing Self")]
    public int accountabilityLvl;
    public int communicationLvl;
    public int decisionMakingLvl;
    public int flexibilityLvl;
    public int selfControlLvl;

    [Header("Developing Others")]
    public int fostersInclusionLvl;
    public int teamworkLvl;

    [Header("Developing Ideas")]
    public int analyticalThinkingLvl;

    [Header("Developing Organizations")]
    public int resourceManagementLvl;
    public int strategicThinkingLvl;

    private void Start()
    {
        strategicThinkingLvl = PlayerPrefs.GetInt("StrategicThinking");
        decisionMakingLvl = PlayerPrefs.GetInt("DecisionMaking");
        flexibilityLvl = PlayerPrefs.GetInt("Flexibility");
        communicationLvl = PlayerPrefs.GetInt("Communication");
        fostersInclusionLvl = PlayerPrefs.GetInt("FostersInclusion");
        teamworkLvl = PlayerPrefs.GetInt("Teamwork");
        selfControlLvl = PlayerPrefs.GetInt("SelfControl");
        accountabilityLvl = PlayerPrefs.GetInt("Accountability");
        resourceManagementLvl = PlayerPrefs.GetInt("ResourceManagement");
        analyticalThinkingLvl = PlayerPrefs.GetInt("AnalyticalThinking");
    }

    private void Awake()
    {
        if (DataManager.dm == null)
        {
            DataManager.dm = this;
        }
        else
        {
            if (DataManager.dm != this)
            {
                Destroy(DataManager.dm.gameObject);
                DataManager.dm = this;
            }
        }
        DontDestroyOnLoad(this.gameObject);
    }

    public void UpdateData()
    {
        PlayerPrefs.SetInt("StrategicThinking", strategicThinkingLvl);
        PlayerPrefs.SetInt("DecisionMaking", decisionMakingLvl);
        PlayerPrefs.SetInt("Flexibility", flexibilityLvl);
        PlayerPrefs.SetInt("Communication", communicationLvl);
        PlayerPrefs.SetInt("FostersInclusion", fostersInclusionLvl);
        PlayerPrefs.SetInt("Teamwork", teamworkLvl);
        PlayerPrefs.SetInt("SelfControl", selfControlLvl);
        PlayerPrefs.SetInt("Accountability", accountabilityLvl);
        PlayerPrefs.SetInt("ResourceManagement", resourceManagementLvl);
        PlayerPrefs.SetInt("AnalyticalThinking", analyticalThinkingLvl);
    }
}
