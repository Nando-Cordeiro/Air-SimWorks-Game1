using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnCollision : MonoBehaviour
{
    [SerializeField] PlayerMovement player;
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            player.SpawnPlayer();
        }
    }

    //bad but fast prototyping (fix later with instancing probably) 
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
    }
}
