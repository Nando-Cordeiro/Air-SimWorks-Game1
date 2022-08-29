using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomActivate : MonoBehaviour
{
    void Start()
    {
        if (Random.Range(0,10) % 2 == 0) gameObject.SetActive(false);
    }
}
