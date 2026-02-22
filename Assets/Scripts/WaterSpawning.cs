using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.PlayerSettings;

public class WaterSpawning : MonoBehaviour
{

    [SerializeField] private GameObject waterGO;
    [SerializeField] private int waterAmount;
    private Queue<GameObject> waterCollection;

    [SerializeField] private Transform camera;
    [SerializeField] private float spawnDistanceFromCamera;
    [SerializeField] private float despawnDistanceFromCamera;

    private float halfWaterSize;

    GameObject recentWater;
    GameObject oldWater;

    void Start()
    {
        halfWaterSize = waterGO.GetComponent<SpriteRenderer>().size.x/2;
        waterCollection = new Queue<GameObject>();
        for (int i = 0; i < waterAmount; i++)
        {
            GameObject waterInstance = Instantiate(waterGO, transform.position, Quaternion.identity);
            waterInstance.transform.Rotate(90, 0, 0);
            waterInstance.SetActive(false);
            waterCollection.Enqueue(waterInstance);
        }
        SpawnWater(transform.position);
    }

    // Update is called once per frame
    void Update()
    {
        if (camera.position.x >= recentWater.transform.position.x - halfWaterSize+ spawnDistanceFromCamera) 
        {
            Vector3 newSpawnPos = recentWater.transform.position + new Vector3(halfWaterSize, 0, 0);
            SpawnWater(newSpawnPos);
        }

        if (camera.position.x>= recentWater.transform.position.x+ halfWaterSize + despawnDistanceFromCamera)
        {
            DespawnWater();

        }
    }

    void SpawnWater(Vector3 pos)
    {
        if (waterCollection.Count > 0)
        {
            GameObject waterInstance = waterCollection.Dequeue();
            waterInstance.transform.position = pos;
            waterInstance.SetActive(true);
            recentWater= waterInstance;
        }
    }

    void DespawnWater()
    {
        waterCollection.Enqueue(recentWater);
        recentWater.SetActive(false);
        oldWater = recentWater;
    }
}
