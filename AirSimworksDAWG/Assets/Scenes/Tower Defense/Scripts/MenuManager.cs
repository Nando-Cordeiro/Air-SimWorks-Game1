using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{

    public GameObject hud;
    public GameObject store;
    public bool isOpen = true;

    public static MenuManager instance;

    void Awake()
    {
        instance = this;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            StoreOpenAndClose();
        }
    }

    public void StoreOpenAndClose()
    {
        isOpen = !isOpen;

        if (!isOpen)
        {
            hud.SetActive(isOpen);
            store.SetActive(!isOpen);
            Movement.instance.acceleration = 0;
            Movement.instance.lookSensitivity = 0;
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
        else if (isOpen)
        {
            hud.SetActive(isOpen);
            store.SetActive(!isOpen);
            Movement.instance.acceleration = 50;
            Movement.instance.lookSensitivity = 2;
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;

        }
    }
}
