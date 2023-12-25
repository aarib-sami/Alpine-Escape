using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] public GameObject[] Obstacles;
    [SerializeField] public GameObject score;
    [SerializeField] private float timeBtwSpawn;
    [SerializeField] public float startTimeBetweenSpawn;
   
    void Update()
    {
        if (timeBtwSpawn <= 0)
        {
            Instantiate(Obstacles[Random.Range(0, Obstacles.Length)], transform.position, Quaternion.identity); ;
            timeBtwSpawn = startTimeBetweenSpawn;
        }
        else
        {
            timeBtwSpawn -= Time.deltaTime;
    
        }

        if (score.GetComponent<DestroyerAndScore>().score >= 5)
        {
            startTimeBetweenSpawn = 2.5f;
        }

        if (score.GetComponent<DestroyerAndScore>().score >= 10)
        {
            startTimeBetweenSpawn = 2.1f;
        }

        if (score.GetComponent<DestroyerAndScore>().score >= 15)
        {
            startTimeBetweenSpawn = 1.8f;
        }

        if (score.GetComponent<DestroyerAndScore>().score >= 20)
        {
            startTimeBetweenSpawn = 1.5f;
        }

        if (score.GetComponent<DestroyerAndScore>().score >= 25)
        {
            startTimeBetweenSpawn = 1.2f;
        }

        if (score.GetComponent<DestroyerAndScore>().score >= 30)
        {
            startTimeBetweenSpawn = 1f;
        }

        if (score.GetComponent<DestroyerAndScore>().score >= 35)
        {
            startTimeBetweenSpawn = 0.7f;
        }



    }
}
