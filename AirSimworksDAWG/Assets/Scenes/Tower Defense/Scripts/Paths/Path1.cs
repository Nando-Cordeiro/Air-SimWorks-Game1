using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Path1: MonoBehaviour
{
    public static Transform[] points1;
    public static Transform startingPoint;
  
    private void Awake()
    {
        points1 = new Transform[transform.childCount];

        startingPoint = transform.GetChild(0);

        for (int i = 0; i < points1.Length; i++)
        {
            points1[i] = transform.GetChild(i);
        }

       
    }
}

    
