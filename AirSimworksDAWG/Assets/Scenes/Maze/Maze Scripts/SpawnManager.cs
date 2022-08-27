using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] MazeSpawner[] mazeSpawners;
    [SerializeField] List<MazeSpawner> spawnableZone;
    // Start is called before the first frame update
    void Start()
    {
        foreach(MazeSpawner mazeSpawner in mazeSpawners)
        {
            mazeSpawner.SpawnMazeSection();
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

    void RandomizeMazeSection()
    {
        foreach(MazeSpawner mazeSpawner in spawnableZone)
        {
            mazeSpawner.DestroyMazeSection();
            mazeSpawner.SpawnMazeSection();
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
