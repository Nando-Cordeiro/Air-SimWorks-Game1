using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PointCollector : MonoBehaviour
{
    [SerializeField] float maxFallDistance;
    [SerializeField] Text pointText;
    int pointsGained = 0;
    List<int> pointPool = new List<int>();
    Coroutine carReset = null;
    [SerializeField] float resetTime;
    [SerializeField] float resetSpeed;
    [SerializeField] Transform respawnPoint;

    public void AddPointPool(int points)
    {
        pointPool.Add(points);
    }

    public void RemovePointPool(int points)
    {
        pointPool.Remove(points);
    }

    int GetHighestValue()
    {
        int highestPoint = 0;
        foreach (int pointValue in pointPool)
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
        pointsGained += GetHighestValue();
        pointText.text = pointsGained.ToString();

        float timer = resetTime;

        while (timer >= 0f)
        {
            timer -= resetSpeed * Time.deltaTime;
            yield return null;
        }

        this.transform.position = respawnPoint.position;
        pointPool.Clear();
        carReset = null;
    }

    void ResetCar()
    {
        pointPool.Clear();
        this.transform.GetComponent<Rigidbody>().velocity = Vector3.zero;
        this.transform.position = respawnPoint.position;
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
