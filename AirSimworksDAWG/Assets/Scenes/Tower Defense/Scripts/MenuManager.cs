using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{

    public GameObject Hud;
    public GameObject Store;
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
            storeOpenAndClose();
        }
            
        
        

    }

    public void storeOpenAndClose()
    {
        
        isOpen = !isOpen;

        if (isOpen == false)
        {
            Hud.SetActive(isOpen);
            Store.SetActive(!isOpen);
            Movement.instance.acceleration = 0;
            Movement.instance.lookSensitivity = 0;
            Cursor.visible = !isOpen;
            unlockCursor();
        }
        else if (isOpen == true)
        {
            Hud.SetActive(isOpen);
            Store.SetActive(!isOpen);
            Movement.instance.acceleration = 50;
            Movement.instance.lookSensitivity = 2;
            Cursor.visible = !isOpen;
            lockCursor();

        }
    }

    private static void unlockCursor()
    {
        Cursor.lockState = CursorLockMode.None;

    }

    private static void lockCursor()
    {
        Cursor.lockState = CursorLockMode.Locked;

    }



}
