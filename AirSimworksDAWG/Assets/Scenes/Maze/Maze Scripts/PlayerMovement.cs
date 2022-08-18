using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] float moveSpeed;
    [SerializeField] float dragValue;

    Rigidbody playerRB;

    Vector3 moveDir = Vector3.zero;
    Vector3 input = Vector3.zero;
    Vector3 friction = Vector3.zero;

    private void Awake()
    {
        playerRB = this.GetComponent<Rigidbody>();
    }

    void Update()
    {
        GetInput();
    }
    private void FixedUpdate()
    {
        ApplyFriction();
        MovePlayer();
        playerRB.velocity = friction;
    }

    private void GetInput()
    {
        //Gets movement input and normalizes it;
        input.x = Input.GetAxisRaw("Horizontal");
        input.z = Input.GetAxisRaw("Vertical");
        moveDir = (transform.right * input.x + transform.forward * input.z).normalized;
    }  
    
    private void ApplyFriction()
    {
        friction.x = playerRB.velocity.x * (1 - Time.deltaTime * dragValue);

        friction.y = playerRB.velocity.y * (1 - Time.deltaTime * dragValue);

        friction.z = playerRB.velocity.z * (1 - Time.deltaTime * dragValue);
    }

    private void MovePlayer()
    {
        playerRB.AddForce(moveDir * moveSpeed, ForceMode.Acceleration);
    }
}
