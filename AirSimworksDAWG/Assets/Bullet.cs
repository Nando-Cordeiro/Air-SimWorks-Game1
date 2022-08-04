using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float timeBeforeDestroy = 10f;

    private void Start()
    {
        Destroy(gameObject, timeBeforeDestroy);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Target"))
        {
            // add points to manager
            Debug.Log("Destroyed a Target");

            // destroy target
            Destroy(other.gameObject);
        }
        else if (other.CompareTag("Player"))
        {
            // kill player? or damage them
        }

        Destroy(gameObject);
    }
}
