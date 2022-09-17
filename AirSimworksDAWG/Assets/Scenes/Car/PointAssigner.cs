using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointAssigner : MonoBehaviour
{
    [SerializeField] PointCollector car;
    [SerializeField] int pointValue;

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            car.AddPointPool(pointValue);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            car.RemovePointPool(pointValue);
        }
    }
}
