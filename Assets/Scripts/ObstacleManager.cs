using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;

public class ObstacleManager : MonoBehaviour
{


    public abstract class Obstacle
    {
        public abstract string Name { get; }
        public abstract void affectPlayer();
    }  

    public class SnowPlatform : Obstacle
    {
        public override string Name => "SnowPlatform";

        public override void affectPlayer()
        {
            Debug.Log("SnowPlatform is affecting!");
        }
    }

    public class IcePlatform : Obstacle
    {
        public override string Name => "IcePlatform";

        public override void affectPlayer()
        {
            Debug.Log("IcePlatform is affecting!");
        }
    }

    public class Pedra : Obstacle
    {
        public override string Name => "Pedra";

        public override void affectPlayer()
        {
            Debug.Log("Pedra is affecting!");
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

    /*public GetRandomObstacle()
    {
        var obstacleNames = ObstacleFactory.GetObstacleNames().ToList();
        int randomIndex = UnityEngine.Random.Range(0, obstacleNames.Count);
        string randomObstacleName = obstacleNames[randomIndex];
        return ObstacleFactory.GetObstacle(randomObstacleName);
    }*/
}
