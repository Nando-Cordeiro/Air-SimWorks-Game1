using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MazeUI : MonoBehaviour
{
    public bool spectating;

    [Header("Spectator")]
    public GameObject s_UI;
    public Image[] s_arrows;
    public int activeArrow;

    [Header("Walker")]
    public GameObject w_UI;
    public Image[] w_arrows;

    // Update is called once per frame
    void Update()
    {
        if (spectating)
        {
            s_UI.SetActive(true);
            w_UI.SetActive(false);

            if (Input.GetKeyDown(KeyCode.UpArrow)) UpdateArrow(0);

            if (Input.GetKeyDown(KeyCode.LeftArrow)) UpdateArrow(1);

            if (Input.GetKeyDown(KeyCode.DownArrow)) UpdateArrow(2);

            if (Input.GetKeyDown(KeyCode.RightArrow)) UpdateArrow(3);
        }
        else // walking
        {
            s_UI.SetActive(false);
            w_UI.SetActive(true);

            if (Input.GetKeyDown(KeyCode.Space)) s_arrows[activeArrow].transform.parent.GetChild(1).gameObject.SetActive(true);
        }
    }


    void UpdateArrow(int i)
    {
        foreach (var a in s_arrows)
        {
            a.transform.parent.GetChild(1).gameObject.SetActive(false);

            a.color = new Color(255f, 255f, 255f, 0.078f);
        }
        foreach (var a in w_arrows) a.color = new Color(255f, 255f, 255f, 0.078f);

        s_arrows[i].color = new Color(255f, 255f, 255f, 255f);
        w_arrows[i].color = new Color(255f, 255f, 255f, 255f);

        activeArrow = i;
    }
}
