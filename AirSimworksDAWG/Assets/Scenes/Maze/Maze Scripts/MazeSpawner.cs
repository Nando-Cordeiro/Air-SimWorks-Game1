using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeSpawner : MonoBehaviour
{
    public GameObject[] MiddlePrefabs;
    public GameObject[] SidePrefabs;
    public GameObject[] CornerPrefabs;

    public int SpawnerType;

    public Transform SpawnPosition;
    
    private void Start()
    {
        if (SpawnerType == 0)
        {
            Instantiate(MiddlePrefabs[Random.Range(0, MiddlePrefabs.Length)], SpawnPosition.position, SpawnPosition.rotation);
        }
        if (SpawnerType == 1)
        {
            Instantiate(SidePrefabs[Random.Range(0, SidePrefabs.Length)], SpawnPosition.position, SpawnPosition.rotation);
        }
        if (SpawnerType == 2)
        {
            Instantiate(CornerPrefabs[Random.Range(0, CornerPrefabs.Length)], SpawnPosition.position, SpawnPosition.rotation);
        }

    }
}
