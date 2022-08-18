using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstPersonController : MonoBehaviour
{
    public float speed = 10f;
    public float jumpHeight = 5f;

    public Transform cam;

    Rigidbody rb;

    public float velocity;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        velocity = Mathf.Abs(rb.velocity.x + rb.velocity.z);

        // Move  && velocity < 10f
        if (Input.GetKey(KeyCode.W)) rb.AddForce(cam.forward * speed, ForceMode.Acceleration);
        if (Input.GetKey(KeyCode.A)) rb.AddForce(-cam.right * speed, ForceMode.Acceleration);
        if (Input.GetKey(KeyCode.S)) rb.AddForce(-cam.forward * speed, ForceMode.Acceleration);
        if (Input.GetKey(KeyCode.D)) rb.AddForce(cam.right * speed, ForceMode.Acceleration);

        //if (velocity > 10f) rb.velocity = new Vector3(rb.velocity)

        //Jump
        if (Input.GetKeyDown(KeyCode.Space) && transform.position.y < 1f) rb.AddForce(Vector3.up * jumpHeight);
    }
}
