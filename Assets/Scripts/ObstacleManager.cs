using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;

public class ObstacleManager : MonoBehaviour
{
    /*

    [SerializeField] public Vector3 spawnPosition;

    [SerializeField] public GameObject snowPlatformPrefab;
    [SerializeField] public GameObject icePlatformPrefab;


    public abstract class Obstacle
    {
        public abstract string Name { get; }
        public abstract Vector3 Position { get; }    }

    public class SnowPlatform : Obstacle
    {
        private GameObject icePlatformGameObject=> icePlatformPrefab;

        public override string Name => "SnowPlatform";
        public override Vector3 Position => spawnPosition;

        private GameObject Instantiate(GameObject icePlatformGameObject, Vector3 position)
        {
            throw new NotImplementedException();
        }
    }

    public class IcePlatform : Obstacle
    {
        public override string Name => "IcePlatform";
        public override Vector3 Position => obstaclePosition;


        public override void Spawn()
        {
            Debug.Log("IcePlatform is spawning!");
        }
    }

    public static class ObstacleFactory
    {
        private static Dictionary<string, Type> obstacleByName;
        private static bool isInitialized => obstacleByName != null;

        private static void InitializeObstacleFactory()
        {
            if(isInitialized)
                return;

            var obstacleTypes = Assembly.GetAssembly(typeof(Obstacle)).GetTypes()
            .Where(myType => myType.IsClass && !myType.IsAbstract && myType.IsSubclassOf(typeof(Obstacle)));

            obstacleByName = new Dictionary<string, Type>();

            foreach (var type in obstacleTypes)
            {
                var tempInstance = Activator.CreateInstance(type) as Obstacle;
                obstacleByName.Add(tempInstance.Name,type);
            }
        }

        public static Obstacle GetObstacle(string obstacleType)
        {
            InitializeObstacleFactory();//just in case it wasn't initialized yet

            if (obstacleByName.ContainsKey(obstacleType))
            {
                Type type = obstacleByName[obstacleType];
                var obstacle = Activator.CreateInstance(type) as Obstacle;
                return obstacle;
            }

            return null;
        }

        internal static IEnumerable<string> GetObstacleNames()
        {
            return obstacleByName.Keys;
        }
    }
    public class NewObject
    {
        string ObjectName;
        GameObject ObjectPrefab;
    }*/
}
