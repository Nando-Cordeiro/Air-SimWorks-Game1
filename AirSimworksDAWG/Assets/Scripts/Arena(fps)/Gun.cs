using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [Header("References")]
    public Camera fpsCam;
    public Transform firepoint;
    public GameObject bullet;
    FPSGameManager manager;
    public GameObject[] models;
    public GameObject shield;
    public PhotonView view;

    [Header("Stats")]
    public float shootSpeed = 1f;
    public float timeBetweenShots = 1f;
    float timeShots;
    public bool doSpread;
    public int activeModel = 0;
    public bool fullAuto;

    public int totalPoints;

    private void Start()
    {
        manager = FindObjectOfType<FPSGameManager>();
        view = GetComponentInParent<PhotonView>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!GetComponent<PhotonView>().IsMine) return; // if the object isnt mine do nothing

        timeShots -= Time.time;

        if(Input.GetButtonDown("Fire1") && timeShots <= 0f)
        {
            view.RPC("Shoot", RpcTarget.All);
            //Shoot();
        }    

        if (activeModel == 0) // pistol
        {
            fullAuto = false;
            timeBetweenShots = 1f;
        }
        else if (activeModel == 1) // shotgun
        {
            fullAuto = false;
            timeBetweenShots = 1.3f;
        }
        else if (activeModel == 2) // rifle
        {
            fullAuto = true;
            timeBetweenShots = 0.1f;
        }
    }

    [PunRPC]
    public void ChangeModel(int num)
    {
        foreach (GameObject g in models) g.SetActive(false);

        models[num].SetActive(true);

        activeModel = num;

        shield.SetActive(true);

        // set stats


    }

    [PunRPC]
    void Shoot()
    {
        timeShots = timeBetweenShots;

        // this one uses a projectile

        //GameObject _b = Instantiate(bullet, firepoint.position, firepoint.rotation);
        //_b.GetComponent<Rigidbody>().AddForce(shootSpeed * fpsCam.transform.forward);

        // saving this if you want it

        RaycastHit hit;
        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit))
        {
            //Debug.Log(hit.transform.name);

            if (hit.collider.tag == "Target")
            {
                int _p = hit.collider.GetComponent<Target>().pointValue;

                if (manager != null)
                {
                    manager.points += _p;
                    manager.totalPoints += _p;
                }

                Destroy(hit.collider.gameObject);
                //Debug.Log("Destroyed a Target");
            }
            else if (hit.collider.tag == "Player")
            {
                if (hit.collider.GetComponent<PhotonView>())
                {
                    hit.collider.GetComponentInChildren<Gun>().Die();
                }
            }
        }


        // set points locally for others to reference
        if (manager != null) totalPoints = manager.totalPoints;
    }

    [PunRPC]
    public void DisableShield()
    {
        shield.SetActive(false);
    }

    public void Die()
    {
        Debug.Log("darn im dead :(");

        // do some stuff
    }
}
