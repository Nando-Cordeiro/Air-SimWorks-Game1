using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CarGameManager : MonoBehaviour
{
    [SerializeField] Text[] gameTimeTexts;
    [SerializeField] float gameTime;
    [SerializeField] float drainRate;
    float gameTimer;

    void Start()
    {
        gameTimer = gameTime;
    }

    private void Update()
    {
        UpdateGameTimer();
    }

    void UpdateGameTimer()
    {
        //update display
        foreach (Text gameTimeText in gameTimeTexts)
        {
            gameTimeText.text = gameTimer.ToString("F2");
        }

        //count down
        if (gameTimer >= 0)
        {
            gameTimer -= (Time.deltaTime * drainRate);
        }
        else
        {
            gameTimer = 0;
        }
    }
}
