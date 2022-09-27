using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Photon.Pun;

public class FPSGameManager : MonoBehaviour
{
    [Header("Stats")]
    public int points;
    public int pointsToNextWeapon = 100;
    int nextGun1, nextGun2;
    public int totalPoints;
    float m,s;

    [Header("References")]
    public Transform[] spawns;
    public Gun player;

    [Header("UI")]
    public Slider nextWeaponSlider;
    public TextMeshProUGUI nextWeaponText;
    public TextMeshProUGUI timeText;
    public TextMeshProUGUI pointsText;
    public GameObject upgradesAvailable;


    private bool ended;


    // Start is called before the first frame update
    void Start()
    {
        points = 0;
        m = 3;

        nextWeaponSlider.maxValue = pointsToNextWeapon;
        nextWeaponSlider.value = points;

        player = FindObjectOfType<Gun>(); // todo: check player for local
    }

    // Update is called once per frame
    void Update()
    {
        if (m > -1) s -= Time.deltaTime;
        else
        {
            // end the game if time goes below 0
            if (!ended) EndGame();
        }

        if (s <= 0f) {
            m--;
            s = 60;
        }

        if (player == null)
        {
            // todo: check player for local
            player = FindObjectOfType<Gun>();
        }

        if (points >= pointsToNextWeapon)
        {

            if (upgradesAvailable != null)
            {
                upgradesAvailable.SetActive(true);

                player.shield.SetActive(false);

                nextGun1 = Random.Range(0, player.models.Length);
                nextGun2 = Random.Range(0, player.models.Length);
            }

            // chose new weapon
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                // choice 1
                player.ChangeModel(nextGun1);

                points = points - pointsToNextWeapon;
                upgradesAvailable.SetActive(false);

                player.shield.SetActive(true);
            }

            // chose new weapon
            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                // choice 2
                player.ChangeModel(nextGun2);

                points = points - pointsToNextWeapon;
                upgradesAvailable.SetActive(false);

                player.shield.SetActive(true);
            }

            if (Input.GetKeyDown(KeyCode.Backspace))
            {
                // choice decline

                points = points - pointsToNextWeapon;
                upgradesAvailable.SetActive(false);

                player.shield.SetActive(true);
            }
        }
    }

    private void OnGUI()
    {
        if (nextWeaponSlider != null)
        {
            nextWeaponSlider.value = points;
        }

        if (nextWeaponText != null) nextWeaponText.text = "" + points;
        if (pointsText != null) pointsText.text = "Total points: " + totalPoints;
        if (timeText != null) timeText.text = "Time remaining " + m + ":" + (int)s;
    }

    void EndGame()
    {
        if (!PhotonNetwork.IsMasterClient)
        {
            return;
        }

        ended = true;

        PhotonNetwork.LoadLevel("AfterGameLobby");
    }
}
