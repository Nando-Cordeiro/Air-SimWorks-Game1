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

    public RecruitManager manager;

    // Start is called before the first frame update
    void Start()
    {
        manager = FindObjectOfType<RecruitManager>();

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

        int i = 0;
        foreach (Follow.Proficiancy proficiancy in desiredTraits)
        {
            desiredTraitIcons[i].material = desiredTraitMats[(int)proficiancy];
            i++;
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

                foreach (RecruitGameManager rgm in FindObjectsOfType<RecruitGameManager>())
                {
                    if (aiFollowers.Contains(ai))
                    {
                        rgm.aiFollowers.Remove(ai);
                        rgm.RemovePoints(GetPointValue(ai));
                    }
                }
               
                ai.target = transform;

                aiFollowers.Add(ai);
                AddPoints(GetPointValue(ai));
            }
        }
    }

    public void AddPoints(int pointValue)
    {
        manager.points += pointValue;
    }

    public void RemovePoints(int pointValue)
    {
        manager.points -= pointValue;
    }

    int GetPointValue(Follow ai)
    {
        if (ai.proficiancy == desiredTraits[0]) return 5;
        if (ai.proficiancy == desiredTraits[1]) return 3;
        if (ai.proficiancy == desiredTraits[2]) return 3;
        if (ai.proficiancy == desiredTraits[3]) return 2;
        if (ai.proficiancy == desiredTraits[4]) return 2;
        if (ai.proficiancy == desiredTraits[5]) return 1;
        if (ai.proficiancy == desiredTraits[6]) return 1;
        if (ai.proficiancy == desiredTraits[7]) return 1;

        return 0;
    }

}
