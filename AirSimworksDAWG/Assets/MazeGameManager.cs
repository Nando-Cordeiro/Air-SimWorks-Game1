using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MazeGameManager : MonoBehaviour
{
    [SerializeField] float switchTime;
    [SerializeField] float drainRate;
    [SerializeField] PlayerCam playerCam;
    float timer;
    [SerializeField] Text playerText;
    [SerializeField] Text spectatorText;
    [SerializeField] SpawnManager spawnManager;

    private void Start()
    {
        timer = switchTime;
    }
    private void Update()
    {
        UpdateSwitchTimer();
    }

    void UpdateSwitchTimer()
    {
        playerText.text = timer.ToString("F2");
        spectatorText.text = timer.ToString("F2");
        if (timer >= 0)
        {
            timer -= (Time.deltaTime * drainRate);
        }
        else
        {
            spawnManager.RandomizeMazeSection();
            playerCam.SwitchPlayersLocal();
            timer = switchTime;  
        }
    }
}
