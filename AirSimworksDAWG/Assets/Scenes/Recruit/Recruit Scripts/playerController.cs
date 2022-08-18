using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerController : MonoBehaviour
{
    public CharacterController characterController;
    public float speed = 12f;

    public float gravity = 9.8f;
    
    //bool grounded = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * y;

        characterController.Move(move * speed * Time.deltaTime);
        characterController.Move(-Vector3.up * gravity * Time.deltaTime); // keep player grounded
    }
}
