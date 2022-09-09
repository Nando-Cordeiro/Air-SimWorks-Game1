using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement3 : MonoBehaviour
{
    public float enemySpeed = 10f;

    private Transform target;
    private int waypointsIndex = 0;

    void Start()
    {
        target = Path3.points3[0];
    }
    void Update()
    {
        Vector3 dir = target.position - transform.position;
        transform.Translate(dir.normalized * enemySpeed * Time.deltaTime, Space.World);

        if (Vector3.Distance(transform.position, target.position) <= 0.4f)
        {
            GetNextWaypoint();
        }
    }

    void GetNextWaypoint()
    {
        if (waypointsIndex >= Path3.points3.Length - 1)
        {
            Destroy(gameObject);
            return;
        }

        waypointsIndex++;
        target = Path3.points3[waypointsIndex];
    }
}
