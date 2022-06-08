using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mouseUnlock : MonoBehaviour
{
    // Start is called before the first frame update
    void Awake()
    {
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}