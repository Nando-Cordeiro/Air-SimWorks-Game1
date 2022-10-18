using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SkillTab : MonoBehaviour
{
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

    public Skills skill;

    public TextMeshProUGUI levelText, pointsText, skillNameText;
    public Slider slider;

    DataManager dm;

    // Start is called before the first frame update
    void Start()
    {
        dm = FindObjectOfType<DataManager>();

        skillNameText.text = skill.ToString();

        int xp = PlayerPrefs.GetInt(skill.ToString());

        levelText.text = "Lvl " + (xp / 1000);

        if (dm.skill1.ToString() == skill.ToString() || dm.skill2.ToString() == skill.ToString())
        {
            int xpGot = dm.lastGamesPoints / 2;
            float xpCarryover = (xpGot / 1000f) - (int)(xpGot / 1000);

            pointsText.text = "+" + xpGot + "exp";

            if (xpGot < 1000) slider.value = xpGot;
            else slider.value = xpCarryover * 1000;
        }
        else // handles other not active ones 
        {
            pointsText.text = "+0exp";
            slider.value = ((xp / 1000f) - (xp /1000)) * 1000;
        }
    }


}
