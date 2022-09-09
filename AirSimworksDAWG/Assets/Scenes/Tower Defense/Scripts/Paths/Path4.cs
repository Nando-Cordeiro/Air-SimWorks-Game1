using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Path4 : MonoBehaviour
{
    public static Transform[] points4;

    private void Awake()
    {
        points4 = new Transform[transform.childCount];

        for (int i = 0; i < points4.Length; i++)
        {
            points4[i] = transform.GetChild(i);
        }

    }
}
