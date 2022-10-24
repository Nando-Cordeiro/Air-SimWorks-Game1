using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PointCollector : MonoBehaviour
{
    [SerializeField] float maxFallDistance;
    Coroutine carReset = null;
    [SerializeField] float resetTime;
    [SerializeField] float resetSpeed;
    StartGame sg;
    CarGameManager manager;

    private void Start()
    {
        sg = FindObjectOfType<StartGame>();
        manager = FindObjectOfType<CarGameManager>();
    }

    public void AddPointPool(int points)
    {
        manager.pointPool.Add(points);
    }

    public void RemovePointPool(int points)
    {
        manager.pointPool.Remove(points);
    }

    int GetHighestValue()
    {
        int highestPoint = 0;
        foreach (int pointValue in manager.pointPool)
        {
            if(pointValue > highestPoint)
            {
                highestPoint = pointValue;
            }
        }

        return highestPoint;
    }

    private IEnumerator CarReset()
    {
        manager.points += GetHighestValue();

        float timer = resetTime;

        while (timer >= 0f)
        {
            timer -= resetSpeed * Time.deltaTime;
            yield return null;
        }

        this.transform.position = sg.spawns[Random.Range(0, sg.spawns.Length)].position;
        this.transform.rotation = sg.spawns[Random.Range(0, sg.spawns.Length)].rotation;
        manager.pointPool.Clear();
        carReset = null;
    }

    void ResetCar()
    {
        manager.pointPool.Clear();
        this.transform.GetComponent<Rigidbody>().velocity = Vector3.zero;
        this.transform.position = sg.spawns[Random.Range(0, sg.spawns.Length)].position;
        this.transform.rotation = sg.spawns[Random.Range(0, sg.spawns.Length)].rotation;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.transform.tag == "Target")
        {
            if(carReset == null)
            {
                carReset = StartCoroutine(CarReset());
            }
        }
    }

    private void Update()
    {
        if(this.transform.position.y <= maxFallDistance)
        {
            ResetCar();
        }
    }
}
