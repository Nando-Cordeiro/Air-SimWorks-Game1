using Photon.Pun;
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

    public PhotonView view;

    public int playerNumberInRoom;
    public int activeArrow;

    public Material[] teamColors;

    private void Awake()
    {
        playerRB = this.GetComponent<Rigidbody>();
    }

    private void Start()
    {
        //SpawnPlayer();
        mm = FindObjectOfType<MazeGameManager>();

        mm.playerCam = cam;
        view = GetComponent<PhotonView>();

        if (playerNumberInRoom % 2 == 0)
        {
            mm.mazeUI.spectating = true;
            cam.SwitchPlayersLocal();
        }

        SetTeamColor();
    }

    void Update()
    {
        view.RPC("RPC_UpdateNumber", RpcTarget.All);

        if (view.IsMine) GetInput();
        else
        {
            cam.walkCam.enabled = false; // needs to be tested
            cam.spectatorCam.enabled = false;
        }
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
        if (mm.mazeUI.spectating) return;

        moveSpeed = pressedShift ? runSpeed : walkSpeed; 

        playerRB.AddForce(moveDir * moveSpeed, ForceMode.Acceleration);
    }

    public void SpawnPlayer()
    {
        this.transform.position = mm.spawnPoints[Random.Range(0, mm.spawnPoints.Length)].transform.position;
    }

    [PunRPC]
    public void RPC_UpdateNumber()
    {
        playerNumberInRoom = FindObjectOfType<PhotonRoom>().myNumberInRoom;
        activeArrow = mm.mazeUI.activeArrow;
    }

    public void SetTeamColor()
    {
        if (playerNumberInRoom < 6)
        {
            GetComponent<MeshRenderer>().material = teamColors[2];
            mm.mazeUI.teamColorText.text = "Green Team";
        }
        else if (playerNumberInRoom < 4)
        {
            GetComponent<MeshRenderer>().material = teamColors[1];
            mm.mazeUI.teamColorText.text = "Blue Team";
        }
        else if (playerNumberInRoom < 2)
        {
            GetComponent<MeshRenderer>().material = teamColors[0];
            mm.mazeUI.teamColorText.text = "Red Team";
        }
    }

    public void UpdatePlayerLocation()
    {
        PlayerMovement partner = null;

        // locate partner
        foreach (PlayerMovement p in FindObjectsOfType<PlayerMovement>())
        {
            if (playerNumberInRoom % 2 == 1 && p.playerNumberInRoom == playerNumberInRoom++)
            {
                partner = p;
            }
            else if (playerNumberInRoom % 2 == 0 && p.playerNumberInRoom == playerNumberInRoom--)
            {
                partner = p;
            }
        }

        // my update pos
        if (partner != null) transform.SetPositionAndRotation(partner.transform.position, partner.transform.rotation);
    }
}
