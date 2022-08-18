using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCam : MonoBehaviour
{
    float xRot;
    float yRot;
    public float mouseSense;
    [SerializeField] Rigidbody playerRB;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        xRot -= Input.GetAxisRaw("Mouse Y") * Time.fixedDeltaTime * mouseSense;
        xRot = Mathf.Clamp(xRot, -90f, 90f);

        yRot += Input.GetAxisRaw("Mouse X") * Time.fixedDeltaTime * mouseSense;

        this.transform.rotation = Quaternion.Euler(xRot, yRot, 0);
        
    }

    private void FixedUpdate()
    {
        playerRB.MoveRotation(Quaternion.Euler(0, yRot, 0));
    }
}
