using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follow : MonoBehaviour
{
    public enum Proficiancy
    {
        P1,
        P2,
        P3,
        P4,
        P5,
        P6,
    }

    [Header("AI Info")]
    //public Proficiancy proficiancy = Proficiancy.P1;
    public List<Proficiancy> proficiancies = new List<Proficiancy>();

    [Header("References")]
    public Transform target;

    public float followDistance;
    public float dampining;

    public float followSpeed;

    [Space(10)]

    public MeshRenderer[] renderers;
    public Material[] materials;

    Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();

        for (int i = 0; i < 3; i++)
        {
            int j = Random.Range(0, 6);
            proficiancies.Add((Proficiancy)j);
            renderers[i].material = materials[j];
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (target != null)
        {
            // Rotate the camera every frame so it keeps looking at the target
            var lookPos = target.position - transform.position;
            lookPos.y = 0;
            var rotation = Quaternion.LookRotation(lookPos);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * dampining);

            // move ai to keep up with player
            if (Vector3.Distance(target.position, transform.position) > followDistance) rb.AddForce(transform.forward * followSpeed * Time.deltaTime, ForceMode.Acceleration);
        }

    }

    public void StartFollow(GameObject whoToFollow)
    {
        target = whoToFollow.transform;
    }
}
