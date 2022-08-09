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
    }

    // Update is called once per frame
    void Update()
    {
        if (points >= pointsToNextWeapon)
        {

            if (upgradesAvailable != null)
            {
                upgradesAvailable.SetActive(true);
            }

            // chose new weapon
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                // choice 1



                points = 0;
                upgradesAvailable.SetActive(false);
            }

            // chose new weapon
            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                // choice 2



                points = 0;
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
