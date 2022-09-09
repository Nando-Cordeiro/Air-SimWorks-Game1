using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomMap : MonoBehaviour
{
    public GameObject[] Maps;
    public Transform SpawnPosition;

    void Start()
    {
        Instantiate(Maps[Random.Range(0, Maps.Length)], SpawnPosition.position, SpawnPosition.rotation);
    }

}
