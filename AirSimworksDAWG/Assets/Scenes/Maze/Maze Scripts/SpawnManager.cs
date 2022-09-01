using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] MazeSpawner[] mazeSpawners;
    [SerializeField] List<MazeSpawner> spawnableZone;
    [SerializeField] bool debugShowPosition;
    // Start is called before the first frame update
    void Start()
    {
        foreach(MazeSpawner mazeSpawner in mazeSpawners)
        {
            mazeSpawner.ToggleDebug(debugShowPosition);
            mazeSpawner.SpawnMazeSection();
            mazeSpawner.GeneratePoints();
            mazeSpawner.RandomizeTrap();
            mazeSpawner.SpawnSigns();
            spawnableZone.Add(mazeSpawner);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            RandomizeMazeSection();
        }
    }

    public void RandomizeMazeSection()
    {
        foreach(MazeSpawner mazeSpawner in spawnableZone)
        {
            mazeSpawner.MarkForDelete();
            mazeSpawner.DestroyMazeSection();
            mazeSpawner.SpawnMazeSection();
            mazeSpawner.DestroySigns();
            mazeSpawner.DestroyTrap();
            mazeSpawner.RandomizeTrap();
            mazeSpawner.SpawnSigns();
        }

        StartCoroutine(WaitForSpawn());
    }

    IEnumerator WaitForSpawn()
    {
        yield return new WaitForSecondsRealtime(1.0f);
        foreach (MazeSpawner mazeSpawner in spawnableZone)
        {
            mazeSpawner.SpawnSigns();
        }
    }

    public void AddSpawnable(MazeSpawner mazeSpawner)
    {
        spawnableZone.Add(mazeSpawner);
    }

    public void RemoveSpawnable(MazeSpawner mazeSpawner)
    {
        spawnableZone.Remove(mazeSpawner);
    }
}
