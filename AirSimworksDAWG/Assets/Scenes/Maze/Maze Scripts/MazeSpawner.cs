using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeSpawner : MonoBehaviour
{
    public GameObject[] MiddlePrefabs;
    public GameObject[] SidePrefabs;
    public GameObject[] CornerPrefabs;

    public int SpawnerType;

    public Transform SpawnPosition;

    GameObject prevSpawnedSection = null;
    GameObject spawnedMazeSection;
    GameObject spawnedTrap;
    RaycastHit hitWall;
    List<GameObject> spawnedSigns = new List<GameObject>();
    [SerializeField] GameObject trapSign;

    [SerializeField] SpawnManager spawnManager;

    int width = 9;
    int height = 9;
    enum Orientation { BL, BR, TL, TR , SI, CE};
    [SerializeField] Orientation orientation;
    [SerializeField] GameObject trapDoor;
    Vector3 spawnPosition;
    Vector3 startPosition;
    int widthDirection;
    int heightDirection;
    [SerializeField] List<Vector3> spawnPoints;
    [SerializeField] bool debugShowPosition;

    public void SpawnMazeSection()
    {
        if (SpawnerType == 0)
        {
            spawnedMazeSection = Instantiate(MiddlePrefabs[Random.Range(0, MiddlePrefabs.Length)], SpawnPosition.position, SpawnPosition.rotation);
        }
        if (SpawnerType == 1)
        {
            spawnedMazeSection = Instantiate(SidePrefabs[Random.Range(0, SidePrefabs.Length)], SpawnPosition.position, SpawnPosition.rotation);
        }
        if (SpawnerType == 2)
        {
            spawnedMazeSection = Instantiate(CornerPrefabs[Random.Range(0, CornerPrefabs.Length)], SpawnPosition.position, SpawnPosition.rotation);
        }
    }

    public void ToggleDebug(bool condition)
    {
        debugShowPosition = condition;
    }

    public void DestroyMazeSection()
    {
        Destroy(spawnedMazeSection);
    }

    public void DestroyTrap()
    {
        Destroy(spawnedTrap);
    }

    public void DestroySigns()
    {
        foreach (GameObject sign in spawnedSigns)
        {
            Destroy(sign);
        }
        spawnedSigns.Clear();
    }

    public void RandomizeTrap()
    {
        spawnPosition = spawnPoints[Random.Range(0, spawnPoints.Count)];
        spawnedTrap = Instantiate(trapDoor, spawnPosition, Quaternion.identity, this.transform);
        spawnedTrap.name = "TD[" + spawnPosition.x + "," + spawnPosition.z + "]";
    }

    public void MarkForDelete()
    {
        spawnedMazeSection.GetComponentInChildren<MarkForDelete>().SetForDelete();
    }

    public void SpawnSigns()
    {
        int layer_mask = LayerMask.GetMask("MazeWall");

        if (Physics.Raycast(spawnedTrap.transform.position + Vector3.up * 1.5f, spawnedTrap.transform.forward, out hitWall, 1.5f, layer_mask))
        {
            if (!hitWall.transform.GetComponent<MarkForDelete>().GetStatus())
            {
                spawnedSigns.Add(Instantiate(trapSign, hitWall.point, Quaternion.LookRotation(-hitWall.normal), this.transform));
                Debug.Log(hitWall.transform.gameObject.name);
            }
        }

        if (Physics.Raycast(spawnedTrap.transform.position + Vector3.up * 1.5f, spawnedTrap.transform.right, out hitWall, 1.5f, layer_mask))
        {
            if (!hitWall.transform.GetComponent<MarkForDelete>().GetStatus())
            {
                spawnedSigns.Add(Instantiate(trapSign, hitWall.point, Quaternion.LookRotation(-hitWall.normal), this.transform));
                Debug.Log(hitWall.transform.gameObject.name);
            }
        }

        if (Physics.Raycast(spawnedTrap.transform.position + Vector3.up * 1.5f, -spawnedTrap.transform.forward, out hitWall, 1.5f, layer_mask))
        {
            if (!hitWall.transform.GetComponent<MarkForDelete>().GetStatus())
            {
                spawnedSigns.Add(Instantiate(trapSign, hitWall.point, Quaternion.LookRotation(-hitWall.normal), this.transform));
                Debug.Log(hitWall.transform.gameObject.name);
            }
        }

        if (Physics.Raycast(spawnedTrap.transform.position + Vector3.up * 1.5f, -spawnedTrap.transform.right, out hitWall, 1.5f, layer_mask))
        {
            if (!hitWall.transform.GetComponent<MarkForDelete>().GetStatus())
            {
                spawnedSigns.Add(Instantiate(trapSign, hitWall.point, Quaternion.LookRotation(-hitWall.normal), this.transform));
                Debug.Log(hitWall.transform.gameObject.name);
            }
        }
    }

    private void Update()
    {
        /*
        Debug.DrawRay(spawnedTrap.transform.position + Vector3.up * 1.5f, spawnedTrap.transform.forward * 1.5f);
        Debug.DrawRay(spawnedTrap.transform.position + Vector3.up * 1.5f, spawnedTrap.transform.forward * -1.5f);
        Debug.DrawRay(spawnedTrap.transform.position + Vector3.up * 1.5f, spawnedTrap.transform.right * 1.5f);
        Debug.DrawRay(spawnedTrap.transform.position + Vector3.up * 1.5f, spawnedTrap.transform.right * -1.5f);
        */
    }

    public GameObject GetSpawnedMazeSection()
    {
        return spawnedMazeSection;
    }

    private void OnTriggerEnter(Collider collision)
    {
        if(collision.transform.tag == "Player")
        {
            spawnManager.RemoveSpawnable(this);
        }
    }

    private void OnTriggerExit(Collider collision)
    {
        if (collision.transform.tag == "Player")
        {
            spawnManager.AddSpawnable(this);
        }
    }

    public void GeneratePoints()
    {
        GetDirection();
        startPosition.x = this.transform.position.x - (widthDirection * 4);
        spawnPosition.y = 1.2f;
        startPosition.z = this.transform.position.z - (heightDirection * 4);

        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                if (GetDirectionCondition(i, j))
                {
                    spawnPosition.x = startPosition.x + i * widthDirection;
                    spawnPosition.z = startPosition.z + j * heightDirection;
                    spawnPoints.Add(spawnPosition);
                    if (debugShowPosition)
                    {
                        Instantiate(trapDoor, spawnPosition, Quaternion.identity, this.transform).name = "TD[" + spawnPosition.x + "," + spawnPosition.z + "]";
                    }
                }
            }
        }
    }

    void GetDirection()
    {
        switch (orientation)
        {
            case Orientation.SI:
            case Orientation.CE:
            case Orientation.BL:
                widthDirection = 3;
                heightDirection = 3;
                break;
            case Orientation.BR:
                widthDirection = -3;
                heightDirection = 3;
                break;
            case Orientation.TL:
                widthDirection = 3;
                heightDirection = -3;
                break;
            case Orientation.TR:
                widthDirection = -3;
                heightDirection = -3;
                break;
        }
    }

    bool GetDirectionCondition(int i, int j)
    {
        bool condition = true;
        switch (orientation)
        {
            case Orientation.BL:
            case Orientation.BR:
            case Orientation.TL:
            case Orientation.TR:
                condition = i >= 3 || j >= 3;
                break;
            case Orientation.CE:
                condition = i < 3 || j < 3 || i > 5 || j > 5;
                break;
        }
        return condition;
    }
}
