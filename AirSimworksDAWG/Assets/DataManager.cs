using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements.Experimental;

public class DataManager : MonoBehaviour
{
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
        DontDestroyOnLoad(this);

        strategicThinkingLvl = PlayerPrefs.GetInt("StrThink");
        decisionMakingLvl = PlayerPrefs.GetInt("decMake");
        flexibilityLvl = PlayerPrefs.GetInt("flex");
        communicationLvl = PlayerPrefs.GetInt("commun");
        fostersInclusionLvl = PlayerPrefs.GetInt("fostInclu");
        teamworkLvl = PlayerPrefs.GetInt("teamwork");
        selfControlLvl = PlayerPrefs.GetInt("selfControl");
        accountabilityLvl = PlayerPrefs.GetInt("acount");
        resourceManagementLvl = PlayerPrefs.GetInt("resource");
        analyticalThinkingLvl = PlayerPrefs.GetInt("analyticThink");
    }

    public void UpdateData()
    {
        PlayerPrefs.SetInt("StrThink", strategicThinkingLvl);
        PlayerPrefs.SetInt("decMake", decisionMakingLvl);
        PlayerPrefs.SetInt("flex", flexibilityLvl);
        PlayerPrefs.SetInt("commun", communicationLvl);
        PlayerPrefs.SetInt("fostInclu", fostersInclusionLvl);
        PlayerPrefs.SetInt("teamwork", teamworkLvl);
        PlayerPrefs.SetInt("selfControl", selfControlLvl);
        PlayerPrefs.SetInt("acount", accountabilityLvl);
        PlayerPrefs.SetInt("resource", resourceManagementLvl);
        PlayerPrefs.SetInt("analyticThink", analyticalThinkingLvl);
    }
}
