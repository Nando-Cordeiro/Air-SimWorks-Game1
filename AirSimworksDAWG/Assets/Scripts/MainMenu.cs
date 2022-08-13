using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public GameObject mainUI;
    public GameObject startUI;

    public void PlayButton()
    {
        startUI.SetActive(false);
        mainUI.SetActive(true);
    }

    public void ReturnButton()
    {
        mainUI.SetActive(false);
        startUI.SetActive(true);
    }
}
