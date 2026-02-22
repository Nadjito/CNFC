using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class ObstacleSpawner : MonoBehaviour
{
    [SerializeField] public GameObject GameManager;//only here as a reference to game manager for obstacles.
    [SerializeField] public Transform player;

    [Header("Spawn Settings")]
    [Space(5)]
    [SerializeField] private float spawnRate;
    [SerializeField] private Transform Water;
    [SerializeField] private float spawnYoffset;
    [SerializeField] private float distanceBetweenObstacles;
    private float? previousObstacleX;
    private float spawnPositionY;
    [Space(5)]
    [Header("Y will be changed to Water Level + YOffset.")]
    [SerializeField] private float spawnDistanceFromPlayer;
    private float time = 0.0f;

    [SerializeField] private List<GameObject> ObstaclesList;

    [Header("Object Pooling Settings")]
    [SerializeField] private int obstaclePoolDefaultCapacity;
    [SerializeField] private int obstaclePoolMaxSize;
    [SerializeField] public float obstacleLifetime; // Time after which the obstacle will be returned to the pool
    private List<GameObject> obstaclePool = new List<GameObject>();
    private int uniqueObstacleIndex;
    Vector3 spawnlocalCoordinates;


    void Start()
    {
        previousObstacleX = null;
        spawnPositionY = Water.position.y + spawnYoffset;
        Debug.Log("Obstacle Pool initialized with " + ObstaclesList.Count + " obstacles.");
        InitializeObstaclePool();
        spawnlocalCoordinates = player.position;

    }


    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;

        if (time >= spawnRate)
        {
            if(obstaclePool.Count >= obstaclePoolMaxSize)
            {
                Debug.LogWarning("No obstacles available in the pool to spawn. Consider increasing the pool size or reducing the spawn rate.");
                return;
            }
            else
            {
                time = 0.0f;
                SpawnRandomObstacle();
            }
        }
    }

    private void SpawnRandomObstacle()
    {
        //int randomIndex = Random.Range(0, ObstaclesList.Count);

        GameObject obstacleToSpawn = GetObstacleFromPool();

        if (previousObstacleX == null)
        {
            spawnlocalCoordinates.x += spawnDistanceFromPlayer;
        }
        else
        {
            if(player.position.x-previousObstacleX>distanceBetweenObstacles)//if players is furher away from previous obtacle than obstacle spawn difference (going fast)
                                                                            //spawn obstacle at distance from player, not from previous obstacle
            {
                spawnlocalCoordinates.x = player.position.x + spawnDistanceFromPlayer;
            }
            else
            {
                spawnlocalCoordinates.x = (float)previousObstacleX + distanceBetweenObstacles;

            }
        }

        previousObstacleX = spawnlocalCoordinates.x;


        spawnlocalCoordinates.y = spawnPositionY;
        spawnlocalCoordinates.y += obstacleToSpawn.GetComponent<Renderer>().bounds.size.y/2;//sets the spawn position to be on the water level, not half of the obstacle above it   
        
        obstacleToSpawn.transform.position = spawnlocalCoordinates;
        //StartCoroutine(DeactivateObstacle(obstacleToSpawn));
    }

    public GameObject GetObstacleFromPool()
    {
        if (obstaclePool.Count <= 0)
        {
            if(obstaclePool.Count + 1 >= obstaclePoolMaxSize)
            {
                Debug.LogWarning("Obstacle Pool has reached its maximum size. Consider increasing the max size or reducing the spawn rate.");
                return null;
            }
            else
            {
                AddNewObstacleToPool();
            }   
        }

        int randomIndex = Random.Range(0, ObstaclesList.Count);
        GameObject obstacle = obstaclePool[randomIndex];
        obstaclePool.RemoveAt(randomIndex);

        obstacle.SetActive(true);
        return obstacle;
    }

    public void ReturnObstacleToPool(GameObject obstacle)
    {
            obstacle.SetActive(false);
            obstaclePool.Add(obstacle);
    }

    private void InitializeObstaclePool()
    {
        int uniqueObstacleIndex = ObstaclesList.Count-1;
        for (int i = 0; i < obstaclePoolDefaultCapacity; i++)
        {
            Debug.Log("Capacity is : " + obstaclePoolDefaultCapacity);
            Debug.Log("Adding new obstacle to pool. Current pool size: " + ObstaclesList.Count);
            AddNewObstacleToPool();
        }
    }

    private void AddNewObstacleToPool()
    {
        if (uniqueObstacleIndex <= 0)
            uniqueObstacleIndex = ObstaclesList.Count-1;
        else { uniqueObstacleIndex--; }

        GameObject obstacle = Instantiate(ObstaclesList[uniqueObstacleIndex], Vector3.zero, Quaternion.identity);
        obstacle.transform.SetParent(transform);
        obstacle.SetActive(false);
        obstaclePool.Add(obstacle);

    }

    IEnumerator DeactivateObstacle(GameObject obstacle)
    {
        yield return new WaitForSeconds(obstacleLifetime);
        //obstacle.GetComponent<ObstacleCommon>().triggerAbility = false; // Reset the trigger ability before returning to pool
        ReturnObstacleToPool(obstacle);
    }


}
