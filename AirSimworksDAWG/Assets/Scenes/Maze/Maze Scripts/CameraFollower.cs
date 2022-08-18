using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollower : MonoBehaviour
{
    [SerializeField]
    Transform playerCamPosition;

    // Update is called once per frame
    void LateUpdate()
    {
        UpdateCamLocation();
    }

    void UpdateCamLocation()
    {
        this.transform.position = playerCamPosition.transform.position;
    }
}
