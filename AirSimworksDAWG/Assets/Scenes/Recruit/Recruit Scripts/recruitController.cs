using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class recruitController : MonoBehaviour
{
    public List<MeshRenderer> renderers;
    public List<Material> materials;
    System.Random rand = new System.Random();
    public List<Traits> traits = new List<Traits>();
    public SpawnPrefab spawner;

    public enum Traits
    {
        Red,
        Blue,
        Green,
        Yellow,
        Orange,
        Purple
    }

    // Start is called before the first frame update
    void Awake()
    {
        for(int i = 0; i < 3; i++)
        {
            int j = rand.Next(0, 6);
            traits.Add((Traits)j);
            renderers[i].material = materials[j];
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
