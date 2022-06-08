using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayButton : MonoBehaviour
{
    public GameObject selector;
    // Start is called before the first frame update
    void Start()
    {
        selector.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void clicked()
    {
        selector.SetActive(true);
    }
}
