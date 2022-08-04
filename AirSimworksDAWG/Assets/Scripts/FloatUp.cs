using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatUp : MonoBehaviour
{
    Rigidbody rb;

    public float upForceMin = 0.2f; 
    public float upForceMax = 0.3f; 


    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        rb.AddForce(Random.Range(upForceMin, upForceMax) * Vector3.up * Time.deltaTime, ForceMode.VelocityChange);
    }
}
