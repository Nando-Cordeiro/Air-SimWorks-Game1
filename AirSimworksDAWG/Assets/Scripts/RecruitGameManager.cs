using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RecruitGameManager : MonoBehaviour
{
    public enum Occupation
    {
        Pilot,
        Intelligence,
        SecurityForces,
        Engineer,
        Medic,
    }

    public Occupation job;

    public List<Follow.Proficiancy> desiredTraits;

    public List<Follow> aiFollowers;

    public int points;

    public Transform cam;

    public float interactRange = 8f;

    [Header("UI")]
    public Image[] desiredTraitIcons;
    public Material[] desiredTraitMats;
    public TextMeshProUGUI jobText;

    // Start is called before the first frame update
    void Start()
    {
        cam = GetComponentInChildren<Camera>().transform;

        var values = System.Enum.GetValues(typeof(Occupation));
        int random = Random.Range(0, values.Length);

        job = (Occupation)values.GetValue(random);

        if (job == Occupation.Pilot)
        {
            desiredTraits.Add(Follow.Proficiancy.FlightOpperations); // 5

            desiredTraits.Add(Follow.Proficiancy.TechMaintenence); // 3
            desiredTraits.Add(Follow.Proficiancy.Mechanic); // 3

            desiredTraits.Add(Follow.Proficiancy.MedicalResearch); // 2
            desiredTraits.Add(Follow.Proficiancy.InvestigationOfficer); // 2

            desiredTraits.Add(Follow.Proficiancy.Administration); // 1
            desiredTraits.Add(Follow.Proficiancy.DataAnylist); // 1
            desiredTraits.Add(Follow.Proficiancy.CyperOperations); // 1
        }
        else if (job == Occupation.Intelligence)
        {
            desiredTraits.Add(Follow.Proficiancy.DataAnylist); // 5

            desiredTraits.Add(Follow.Proficiancy.CyperOperations); // 3
            desiredTraits.Add(Follow.Proficiancy.InvestigationOfficer); // 3

            desiredTraits.Add(Follow.Proficiancy.Administration); // 2
            desiredTraits.Add(Follow.Proficiancy.TechMaintenence); // 2

            desiredTraits.Add(Follow.Proficiancy.Mechanic); // 1
            desiredTraits.Add(Follow.Proficiancy.MedicalResearch); // 1
            desiredTraits.Add(Follow.Proficiancy.FlightOpperations); // 1
        }
        else if (job == Occupation.SecurityForces)
        {
            desiredTraits.Add(Follow.Proficiancy.InvestigationOfficer); // 5

            desiredTraits.Add(Follow.Proficiancy.DataAnylist); // 3
            desiredTraits.Add(Follow.Proficiancy.MedicalResearch); // 3

            desiredTraits.Add(Follow.Proficiancy.Administration); // 2
            desiredTraits.Add(Follow.Proficiancy.FlightOpperations); // 2

            desiredTraits.Add(Follow.Proficiancy.Mechanic); // 1
            desiredTraits.Add(Follow.Proficiancy.TechMaintenence); // 1
            desiredTraits.Add(Follow.Proficiancy.CyperOperations); // 1
        }
        else if (job == Occupation.Engineer)
        {
            desiredTraits.Add(Follow.Proficiancy.Mechanic); // 5

            desiredTraits.Add(Follow.Proficiancy.TechMaintenence); // 3
            desiredTraits.Add(Follow.Proficiancy.FlightOpperations); // 3

            desiredTraits.Add(Follow.Proficiancy.CyperOperations); // 2
            desiredTraits.Add(Follow.Proficiancy.DataAnylist); // 2

            desiredTraits.Add(Follow.Proficiancy.Administration); // 1
            desiredTraits.Add(Follow.Proficiancy.MedicalResearch); // 1
            desiredTraits.Add(Follow.Proficiancy.InvestigationOfficer); // 1
        }
        else if (job == Occupation.Medic)
        {
            desiredTraits.Add(Follow.Proficiancy.MedicalResearch); // 5

            desiredTraits.Add(Follow.Proficiancy.DataAnylist); // 3
            desiredTraits.Add(Follow.Proficiancy.Administration); // 3

            desiredTraits.Add(Follow.Proficiancy.CyperOperations); // 2
            desiredTraits.Add(Follow.Proficiancy.Mechanic); // 2

            desiredTraits.Add(Follow.Proficiancy.TechMaintenence); // 1
            desiredTraits.Add(Follow.Proficiancy.FlightOpperations); // 1
            desiredTraits.Add(Follow.Proficiancy.InvestigationOfficer); // 1
        }

        jobText.text = "Occupation: " + job.ToString();

        int j = 0;
        foreach (Image i in desiredTraitIcons)
        {
            i.material = desiredTraitMats[j];
            j++;
        }
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;

        if (Physics.Raycast(cam.position, cam.forward,out hit, interactRange))
        {
            if (hit.collider.CompareTag("Recruit") && Input.GetKeyDown(KeyCode.Mouse0))
            {
                Follow ai = hit.collider.GetComponent<Follow>();
                ai.target = transform;

                aiFollowers.Add(ai);
            }
        }

        GetPointValue();
    }

    void GetPointValue()
    {
        points = 0;

        foreach (Follow ai in aiFollowers)
        {
            if (ai.proficiancy == desiredTraits[0]) points += 5;

            if (ai.proficiancy == desiredTraits[1]) points += 3;
            if (ai.proficiancy == desiredTraits[2]) points += 3;

            if (ai.proficiancy == desiredTraits[3]) points += 2;
            if (ai.proficiancy == desiredTraits[4]) points += 2;

            if (ai.proficiancy == desiredTraits[5]) points += 1;
            if (ai.proficiancy == desiredTraits[6]) points += 1;
            if (ai.proficiancy == desiredTraits[7]) points += 1;
        }

    }

}
