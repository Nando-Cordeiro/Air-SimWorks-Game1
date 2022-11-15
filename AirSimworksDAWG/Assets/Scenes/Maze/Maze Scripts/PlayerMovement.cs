using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.SymbolStore;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviourPunCallbacks, IPunObservable
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

    public int playerNumberInRoom = 0;
    int playerNum = 0;
    public int activeArrow = 0;
    public bool badArrow = false;

    // for local checks
    int lastArrowGotten;
    bool arrowChecked;

    public Material[] teamColors;

    PlayerMovement partner;

    private void Awake()
    {
        playerRB = this.GetComponent<Rigidbody>();
    }

    private void Start()
    {
        view = GetComponent<PhotonView>(); // needs to be here to avoid errors
        mm = FindObjectOfType<MazeGameManager>();

        if (!view.IsMine) return;

        //SpawnPlayer();
        mm.mazeUI.playersView = this;

        mm.playerCam = cam;

        if (playerNumberInRoom % 2 == 0)
        {
            mm.mazeUI.spectating = true;
            cam.SwitchPlayersLocal();
        }

        SetTeamColor();

        UpdateNumbers();

        //view.RPC("RPC_UpdateNumber", RpcTarget.All);
    }

    void Update()
    {
        if (view.IsMine) GetInput();
        else
        {
            if (cam.gameObject.activeSelf)
            {
                cam.spectatorCam.gameObject.SetActive(false);
                cam.gameObject.SetActive(false);
            }

            return;
        }

        if (!view.IsMine) return; // just to be sure though should never be reached

        playerNum = FindObjectOfType<PhotonRoom>().myNumberInRoom;

        //view.RPC("RPC_UpdateNumber", RpcTarget.All);

        UpdateNumbers();

        if (partner == null) // search for partner
        {
            foreach (PlayerMovement p in FindObjectsOfType<PlayerMovement>()) 
            {
                if (playerNumberInRoom % 2 == 0)
                {
                    if (p.playerNumberInRoom == playerNumberInRoom - 1) partner = p;
                }
                else
                {
                    if (p.playerNumberInRoom == playerNumberInRoom + 1) partner = p;
                }
            }
        }

        if (partner != null) // get their data
        {
            if (!mm.mazeUI.spectating && partner.activeArrow != lastArrowGotten)
            {
                lastArrowGotten = partner.activeArrow;
                activeArrow = partner.activeArrow;
                badArrow = false;
            }
            else if (mm.mazeUI.spectating && !arrowChecked)
            {
                arrowChecked = true;
                badArrow = partner.badArrow;
            }
        }
    }

    private void FixedUpdate()
    {
        if (!view.IsMine) return;

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
        playerNumberInRoom = playerNum;
        activeArrow = mm.mazeUI.activeArrow;
        badArrow = mm.mazeUI.badArrow;
    }
    
    public void UpdateNumbers()
    {
        playerNumberInRoom = playerNum;
        activeArrow = mm.mazeUI.activeArrow;
        badArrow = mm.mazeUI.badArrow;
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

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(playerNumberInRoom);
            stream.SendNext(activeArrow);
            stream.SendNext(badArrow);
        }
        else
        {
            playerNumberInRoom = (int)stream.ReceiveNext();
            activeArrow = (int)stream.ReceiveNext();
            badArrow = (bool)stream.ReceiveNext();
        }
    }
}
