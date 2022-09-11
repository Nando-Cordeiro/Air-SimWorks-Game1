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

    [Header("References")]
    public Transform[] spawns;
    public Gun player;

    [Header("UI")]
    public Slider nextWeaponSlider;
    public TextMeshProUGUI nextWeaponText;
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
            }

            // chose new weapon
            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                // choice 2
                player.ChangeModel(nextGun2);

                points = points - pointsToNextWeapon;
                upgradesAvailable.SetActive(false);
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
    }
}
