using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarGameManager : MonoBehaviour
{
    Vector3 gravity = Vector3.down * 20f;
    // Start is called before the first frame update
    void Start()
    {
        Physics.gravity = gravity;
        
    }
}
