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

    GameObject spawnedMazeSection;

    [SerializeField] SpawnManager spawnManager;

    public void SpawnMazeSection()
    {
        if (SpawnerType == 0)
        {
            spawnedMazeSection = Instantiate(MiddlePrefabs[Random.Range(0, MiddlePrefabs.Length)], SpawnPosition.position, SpawnPosition.rotation);
        }
        if (SpawnerType == 1)
        {
            spawnedMazeSection = Instantiate(SidePrefabs[Random.Range(0, SidePrefabs.Length)], SpawnPosition.position, SpawnPosition.rotation);
        }
        if (SpawnerType == 2)
        {
            spawnedMazeSection = Instantiate(CornerPrefabs[Random.Range(0, CornerPrefabs.Length)], SpawnPosition.position, SpawnPosition.rotation);
        }
    }

    public void DestroyMazeSection()
    {
        Destroy(spawnedMazeSection);
    }

    public GameObject GetSpawnedMazeSection()
    {
        return spawnedMazeSection;
    }

    private void OnTriggerEnter(Collider collision)
    {
        if(collision.transform.tag == "Player")
        {
            Debug.Log("CANNOT RESPAWN THIS BOI");
            spawnManager.RemoveSpawnable(this);
        }
    }

    private void OnTriggerExit(Collider collision)
    {
        if (collision.transform.tag == "Player")
        {
            Debug.Log("CAN RESPAWN THIS BOI");
            spawnManager.AddSpawnable(this);
        }
    }
}
