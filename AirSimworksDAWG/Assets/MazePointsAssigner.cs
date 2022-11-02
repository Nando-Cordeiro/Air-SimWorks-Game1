using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazePointsAssigner : MonoBehaviour
{
    public int points = 250;

    private void OnTriggerEnter(Collider other)
    {
        MazeGameManager mm = FindObjectOfType<MazeGameManager>();
        mm.points += points;

        //move the player to a random spot
        PlayerMovement p = other.GetComponent<PlayerMovement>();

        if (p != null) p.SpawnPlayer();
    }
}
