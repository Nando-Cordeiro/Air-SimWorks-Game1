using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] float walkSpeed;
    [SerializeField] float runSpeed;
    float moveSpeed;
    [SerializeField] float dragValue;

    Rigidbody playerRB;

    bool pressedShift;

    Vector3 moveDir = Vector3.zero;
    Vector3 input = Vector3.zero;
    Vector3 friction = Vector3.zero;

    MazeGameManager mm;

    public PlayerCam cam;

    public TextMeshProUGUI[] texts; 

    private void Awake()
    {
        playerRB = this.GetComponent<Rigidbody>();
    }

    private void Start()
    {
        //SpawnPlayer();
        mm.playerCam = cam;
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

        pressedShift = Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift);
    }  
    
    private void ApplyFriction()
    {
        friction.x = playerRB.velocity.x * (1 - Time.deltaTime * dragValue);

        friction.y = playerRB.velocity.y * (1 - Time.deltaTime * dragValue);

        friction.z = playerRB.velocity.z * (1 - Time.deltaTime * dragValue);
    }

    private void MovePlayer()
    {
        moveSpeed = pressedShift ? runSpeed : walkSpeed; 

        playerRB.AddForce(moveDir * moveSpeed, ForceMode.Acceleration);
    }

    public void SpawnPlayer()
    {
        this.transform.position = mm.spawnPoints[Random.Range(0, mm.spawnPoints.Length)].transform.position;
    }
}
