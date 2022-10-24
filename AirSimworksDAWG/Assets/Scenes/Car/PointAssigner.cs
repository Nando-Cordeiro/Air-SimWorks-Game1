using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointAssigner : MonoBehaviour
{
    [SerializeField] int pointValue;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && other.GetComponent<PhotonView>().IsMine)
        {
            other.GetComponent<PointCollector>().AddPointPool(pointValue);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player" && other.GetComponent<PhotonView>().IsMine)
        {
            other.GetComponent<PointCollector>().RemovePointPool(pointValue);
        }
    }
}
