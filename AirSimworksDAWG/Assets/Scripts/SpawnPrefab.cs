using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using System.IO;

public enum spawnableType
{
    Target,
    Recruit
}

public class SpawnPrefab : MonoBehaviour
{
    public bool readyToSpawn = true;
    System.Random rng = new System.Random();
    public GameObject prefab;
    public GameObject spawned;
    public spawnableType spawnType;


    public float minWait;
    public float maxWait;

    // Update is called once per frame
    void Update()
    {
        if(readyToSpawn && rng.Next(0, 101) <= 10 && spawnType == spawnableType.Target)
        {
            readyToSpawn = !readyToSpawn;
            Invoke("spawnPrefab", Random.Range(minWait, maxWait)); 
        }
        if(readyToSpawn && rng.Next(0, 101) <= 10 && spawnType == spawnableType.Recruit)
        {
            readyToSpawn = !readyToSpawn;
            Invoke("spawnRecruitPrefab", Random.Range(minWait, maxWait));
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
    public void spawnRecruitPrefab()
    {
        //spawned = GameObject.Instantiate(prefab);
        //spawned.transform.position = transform.position;
        //spawned.GetComponent<recruitController>().spawner = this.gameObject.GetComponent<SpawnPrefab>();

        PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "AI Guy"), transform.position, Quaternion.identity, 0);
    }
}
