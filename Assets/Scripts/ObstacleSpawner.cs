using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    [SerializeField] private Transform player;

    [SerializeField] private float spawnRate;
    [SerializeField] private Vector3 spawnPosition;
    private float time = 0.0f;

    [SerializeField] private List<GameObject> ObstaclesList;

  
    //public abstract Obstacle 

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;

        if (time >= spawnRate)
        {
            time = 0.0f;
            SpawnRandomObstacle();

        }
    }

    private void SpawnRandomObstacle()
    {


        int randomIndex = Random.Range(0, ObstaclesList.Count);
        GameObject obstacleToSpawn = ObstaclesList[randomIndex];
        Vector3 spawnlocalCoordinates = player.position + spawnPosition;
        Instantiate(obstacleToSpawn, spawnlocalCoordinates, Quaternion.identity);
    }
}
