using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCam : MonoBehaviour
{
    float xRot;
    float yRot;
    public float mouseSense;
    [SerializeField] Rigidbody playerRB;
    Vector3 arrowDirection = Vector3.zero;
    public Camera walkCam, spectatorCam;

    MazeGameManager mm;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        mm = FindObjectOfType<MazeGameManager>();
    }

    public void SwitchPlayersLocal()
    {
        walkCam.enabled = !walkCam.enabled;
        spectatorCam.enabled = !spectatorCam.enabled;
    }

    private void Update()
    {
        if (mm == null) mm = FindObjectOfType<MazeGameManager>();
    }

    // Update is called once per frame
    void LateUpdate()
    {
        xRot -= Input.GetAxisRaw("Mouse Y") * Time.fixedDeltaTime * mouseSense;
        xRot = Mathf.Clamp(xRot, -90f, 90f);

        yRot += Input.GetAxisRaw("Mouse X") * Time.fixedDeltaTime * mouseSense;

        this.transform.rotation = Quaternion.Euler(xRot, yRot, 0);

        //arrowDirection.z = this.transform.localEulerAngles.y;
        //arrow.transform.localEulerAngles = arrowDirection;


        //arrow.transform.eulerAngles = arrowDirection;
        //Quaternion.Euler(0, 0, this.transform.rotation.y);
    }

    private void FixedUpdate()
    {
        playerRB.MoveRotation(Quaternion.Euler(0, yRot, 0));
    }
}
