using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpectatorScript : MonoBehaviour
{
    enum DirectionState {up, left, down, right };
    Vector3[] directionDict = { Vector3.zero, Vector3.forward * 90f, Vector3.forward * 180f, Vector3.forward * 270f };

    [SerializeField] Image[] arrows;
    Image selectedDiretion;

    [SerializeField] PlayerCam playerCam;
    [SerializeField] GameObject cancelDirection;

    //void UpdateDirection(DirectionState directionState)
    //{
    //    if(selectedDiretion == null)
    //    {
    //        playerCam.EnableArrow(true);
    //    }
    //    else
    //    {
    //        selectedDiretion.color = Color.white;
    //    }

    //    playerCam.ChangeArrowDirection(directionDict[(int)directionState]);
    //    selectedDiretion = arrows[(int)directionState];
    //    selectedDiretion.color = Color.black;
    //    playerCam.EnableDirectionCancel(false);
    //    EnableDirectionCancel(false);
    //}

    //public void EnableDirectionCancel(bool state)
    //{
    //    cancelDirection.SetActive(state);
    //}
}
