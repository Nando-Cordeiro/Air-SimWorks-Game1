using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetSpawner : MonoBehaviour
{
    public float timeBetweenSpawns = 5f;
    float time;

    public GameObject[] targets;

    // Start is called before the first frame update
    void Start()
    {
        time = Random.Range(0, timeBetweenSpawns);
    }

    // Update is called once per frame
    void Update()
    {
        if (time < 0f)
        {
            time = Random.Range(1, timeBetweenSpawns);

            // make new target
            Instantiate(targets[Random.Range(0,targets.Length)], transform.position, transform.rotation);
        }
        else
        {
            time -= Time.deltaTime; // decrement time
        }
    }
}
