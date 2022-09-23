using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements.Experimental;

public class DataManager : MonoBehaviour
{
    public int strategicThinkingLvl;
    public int decisionMakingLvl;
    public int flexibilityLvl;
    public int communicationLvl;
    public int fostersInclusionLvl;
    public int teamworkLvl;
    public int selfControlLvl;
    public int accountabilityLvl;
    public int resourceManagementLvl;
    public int analyticalThinkingLvl;



    private void Start()
    {
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
}
