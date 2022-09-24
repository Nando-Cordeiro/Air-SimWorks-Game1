using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MazeGameManager : MonoBehaviour
{
    [SerializeField] float gameTime;
    [SerializeField] float switchTime;
    [SerializeField] float drainRate;
    [SerializeField] PlayerCam playerCam;
    float gameTimer;
    float switchTimer;
    [SerializeField] Text playerText;
    [SerializeField] Text spectatorText;
    [SerializeField] SpawnManager spawnManager;
    [SerializeField] Text[] gameTimeTexts;

    private void Start()
    {
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
        playerText.text = switchTimer.ToString("F2");
        spectatorText.text = switchTimer.ToString("F2");

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
