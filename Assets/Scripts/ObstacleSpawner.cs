using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class ObstacleSpawner : MonoBehaviour
{
    [SerializeField] private Transform player;

    [Header("Spawn Settings")]
    [Space(5)]
    [SerializeField] private float spawnRate;
    [SerializeField] private Transform Water;
    [SerializeField] private float spawnYoffset;
    private float spawnPositionY;
    [Space(5)]
    [Header("Y will be changed to Water Level + YOffset.")]
    [SerializeField] private float spawnDistanceFromPlayer;
    private float time = 0.0f;

    [SerializeField] private List<GameObject> ObstaclesList;

    [Header("Object Pooling Settings")]
    [SerializeField] private int obstaclePoolDefaultCapacity;
    [SerializeField] private int obstaclePoolMaxSize;
    [SerializeField] private float obstacleLifetime; // Time after which the obstacle will be returned to the pool
    private Queue<GameObject> obstaclePool = new Queue<GameObject>();
    [SerializeField] private Transform ObstacleHolder;


    void Start()
    {
        spawnPositionY = Water.position.y + spawnYoffset;
        InitializeObstaclePool();
    }


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
        //int randomIndex = Random.Range(0, ObstaclesList.Count);

        GameObject obstacleToSpawn = GetObstacleFromPool();

        Vector3 spawnlocalCoordinates = player.position;
        spawnlocalCoordinates.x += spawnDistanceFromPlayer;

        spawnlocalCoordinates.y = spawnPositionY;
        spawnlocalCoordinates.y += obstacleToSpawn.GetComponent<Renderer>().bounds.size.y/2;//sets the spawn position to be on the water level, not half of the obstacle above it   
        
        obstacleToSpawn.transform.position = spawnlocalCoordinates;
        StartCoroutine(DeactivateObstacle(obstacleToSpawn));
    }

    public GameObject GetObstacleFromPool()
    {
        if (obstaclePool.Count > 0)
        {
            GameObject obstacle = obstaclePool.Dequeue();
            obstacle.SetActive(true);
            return obstacle;
        }
        else
        {
            if(obstaclePool.Count + 1 <= obstaclePoolMaxSize)
            {
                GameObject obstacle = Instantiate(ObstaclesList[Random.Range(0, ObstaclesList.Count)], Vector3.zero, Quaternion.identity);
                obstacle.transform.SetParent(ObstacleHolder);
                obstacle.SetActive(false);
                obstaclePool.Enqueue(obstacle);
                return obstacle;
            }
            return null;
        }
    }

    public void ReturnObstacleToPool(GameObject obstacle)
    {
            obstacle.SetActive(false);
            obstaclePool.Enqueue(obstacle);
    }

    private void InitializeObstaclePool()
    {
        for (int i = 0; i < obstaclePoolDefaultCapacity; i++)
        {
             GameObject obstacle = Instantiate(ObstaclesList[Random.Range(0, ObstaclesList.Count)], Vector3.zero, Quaternion.identity);
            obstacle.transform.SetParent(ObstacleHolder);
            obstacle.SetActive(false);
             obstaclePool.Enqueue(obstacle);
        }
    }

    IEnumerator DeactivateObstacle(GameObject obstacle)
    {
        yield return new WaitForSeconds(obstacleLifetime);
        ReturnObstacleToPool(obstacle);
    }


}
