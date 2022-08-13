using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rise : MonoBehaviour
{
    //public AK.Wwise.Event Target;
    public SpawnPrefab spawner;
    public float speed = 1f;

    // Update is called once per frame
    void Update()
    {
        transform.position += transform.up * speed * Time.deltaTime;
        if(transform.position.y >= 10)
        {
            Debug.Log("Destroyed");
            spawner.resetSpawner();
            GameObject.Destroy(gameObject);
        }
    }
    public void destroySelf()
    {
        spawner.resetSpawner();
        GameObject.Destroy(gameObject);

       // Target.Post(gameObject);

    }
}
