using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public AK.Wwise.Event Play_Shoot;
    public Camera fpsCam;
    public score score;

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Fire1"))
        {
            Shoot();
            
        }
    }
    
    void Shoot()
    {

        RaycastHit hit;

        Play_Shoot.Post(gameObject);
        

        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit))
        {
            Debug.Log(hit.transform.name);
            if (hit.transform.name == "Target(Clone)")
            {
                hit.transform.gameObject.GetComponent<rise>().destroySelf();
                score.addPoint();
            }
        }
    }
}
