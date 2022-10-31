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
    [SerializeField] Image arrow;
    Vector3 arrowDirection = Vector3.zero;
    [SerializeField] GameObject arrowPivot;
    Camera playerCam;
    [SerializeField] GameObject cancelDirection;
    [SerializeField] SpectatorScript spectator;

    // Start is called before the first frame update
    void Start()
    {
        playerCam = Camera.main;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    public void ChangeArrowDirection(Vector3 direction)
    {
        arrowPivot.transform.localEulerAngles = direction;
    }

    private void Update()
    {
        //if (Input.GetKeyDown(KeyCode.T))
        //{
        //   playerCam.enabled = !playerCam.enabled;
        //}

        //if (Input.GetKeyDown(KeyCode.Space))
        //{
        //    EnableDirectionCancel(true);
        //    spectator.EnableDirectionCancel(true);
        //}
    }

    public void SwitchPlayersLocal()
    {
        playerCam.enabled = !playerCam.enabled;
    }

    //public void EnableDirectionCancel(bool state)
    //{
    //    cancelDirection.SetActive(state);
    //}

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

    public void EnableArrow(bool state)
    {
        arrow.enabled = state;
    }
}
