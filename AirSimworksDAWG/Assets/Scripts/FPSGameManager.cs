using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

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


    // Start is called before the first frame update
    void Start()
    {
        points = 0;

        nextWeaponSlider.maxValue = pointsToNextWeapon;
        nextWeaponSlider.value = points;

        player = FindObjectOfType<Gun>(); // todo: check player for local
    }

    // Update is called once per frame
    void Update()
    {
        s += Time.deltaTime;

        if (s >= 60f) {
            m++;
            s = 0;
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
        if (timeText != null) timeText.text = "Time elapsed " + m + ":" + (int)s;
    }
}
