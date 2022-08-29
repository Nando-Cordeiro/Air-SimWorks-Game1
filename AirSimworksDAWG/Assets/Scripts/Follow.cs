using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follow : MonoBehaviour
{
    public enum Proficiancy
    {
        FlightOpperations,
        Mechanic,
        MedicalResearch,
        InvestigationOfficer,
        DataAnylist,
        Administration,
        CyperOperations,
        TechMaintenence,
    }

    [Header("AI Info")]
    public Proficiancy proficiancy;

    [Header("References")]
    public Transform target;

    public float followDistance;
    public float dampining;

    public float followSpeed;

    [Space(10)]

    public MeshRenderer icon;
    public Material[] materials;

    Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();

        var values = System.Enum.GetValues(typeof(Proficiancy));
        int random = Random.Range(0, values.Length);

        proficiancy = (Proficiancy)values.GetValue(random);

        icon.material = materials[random];
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
