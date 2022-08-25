using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtPlayer : MonoBehaviour
{
    public Transform target;

    // Update is called once per frame
    void Update()
    {
        if (target == null)
        {
            foreach (playerController t in FindObjectsOfType<playerController>())
            {
                if (t.GetComponent<PhotonView>().IsMine) target = t.transform;
            }
        }

        transform.LookAt(target);
    }
}
