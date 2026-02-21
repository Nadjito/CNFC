using NUnit.Framework;
using UnityEngine;
using static ObstacleManager;

[CreateAssetMenu(fileName = "ObstacleInfo", menuName = "Scriptable Objects/ObstacleInfo")]
public class ObstacleInfo : ScriptableObject
{
    public GameObject obstaclePrefab;

}
