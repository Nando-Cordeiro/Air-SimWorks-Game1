using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPrefab : MonoBehaviour
{
    public bool readyToSpawn = true;
    System.Random rng = new System.Random();
    public GameObject prefab;
    public GameObject spawned;

    public float minWait;
    public float maxWait;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(readyToSpawn && rng.Next(0, 101) <= 10)
        {
            readyToSpawn = !readyToSpawn;
            Invoke("spawnPrefab", Random.Range(minWait, maxWait));
            
            
        }
    }
    public void resetSpawner()
    {
        readyToSpawn = true;
        spawned = new GameObject();
    }
    public void spawnPrefab()
    {
        spawned = GameObject.Instantiate(prefab);
        spawned.transform.position = transform.position;
        spawned.GetComponent<rise>().spawner = this.gameObject.GetComponent<SpawnPrefab>();
    }
}
