using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecruitGameManager : MonoBehaviour
{
    public List<Follow> aiFollowers;

    public int points;

    public Transform cam;

    public float interactRange = 8f;

    // Start is called before the first frame update
    void Start()
    {
        cam = GetComponentInChildren<Camera>().transform;
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;

        if (Physics.Raycast(cam.position, cam.forward,out hit, interactRange))
        {
            if (hit.collider.CompareTag("Recruit") && Input.GetKeyDown(KeyCode.E))
            {
                Follow ai = hit.collider.GetComponent<Follow>();
                ai.target = transform;

                aiFollowers.Add(ai);
            }
        }
    }
}
