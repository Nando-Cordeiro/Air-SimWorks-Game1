using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class UICompass : MonoBehaviour
{
    public GameObject player;

    // Update is called once per frame
    void Update()
    {
        if (player != null)
        {
            //transform.rotation = player.transform.rotation; // new Quaternion(0, 0, player.transform.rotation.y * 6, 1);
            transform.rotation = Quaternion.Euler(0, 0, player.transform.rotation.y * 180f);
        }
        else if (FindObjectOfType<StartGame>().g != null) player = FindObjectOfType<StartGame>().g;
    }
}
