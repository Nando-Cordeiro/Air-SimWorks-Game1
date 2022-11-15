using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnCollision : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            other.GetComponent<PlayerMovement>().SpawnPlayer();
        }
    }
}
