using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RecruitController : MonoBehaviour
{
    public MeshRenderer icon;
    public List<Material> materials;
    public Traits trait;
    //public Image[] traitIcons;

    public enum Traits
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

    // Start is called before the first frame update
    void Awake()
    {
        //for(int i = 0; i < 3; i++)
        //{
        //    int j = rand.Next(0, 6);
        //    traits.Add((Traits)j);
        //    renderers[i].material = materials[j];
        //}

        var values = System.Enum.GetValues(typeof(Traits));
        int random = Random.Range(0, values.Length);

        trait = (Traits)values.GetValue(random);

        icon.material = materials[random];
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
