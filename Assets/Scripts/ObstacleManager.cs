using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;

public class ObstacleManager : MonoBehaviour
{
    //[SerializeField] private float snowDecelerate;

    public abstract class Obstacle
    {
        public abstract string Name { get; }
        public abstract void affectPlayer(GameObject obstacle);

        public Transform playerTrans = GameObject.FindWithTag("Player").transform;
        public PenguinController playerController = GameObject.FindWithTag("Player").GetComponent<PenguinController>();
        public abstract float speedModifier { get; }
    }

    public class SnowPlatform : Obstacle
    {
        public override string Name => "SnowPlatform";
        public override float speedModifier=> -30f;

        public override void affectPlayer(GameObject obstacle)
        {
            if(playerTrans.position.y > obstacle.transform.position.y)
            {
                float newSpeed = playerController.GetComponent<PenguinController>().GetSpeed() + speedModifier;
                playerController.GetComponent<PenguinController>().SetSpeed(newSpeed);
            }
           
        }
    }

    public class IcePlatform : Obstacle
    {
        public override string Name => "IcePlatform";

        public override float speedModifier => +5f;
        public override void affectPlayer(GameObject obstacle)
        {
            int speedModifier = (int)this.speedModifier;
            if (playerTrans.position.y < obstacle.transform.position.y)
            {
                speedModifier = speedModifier * -1;
            }

            float newSpeed = playerController.GetComponent<PenguinController>().GetSpeed() + speedModifier;
            playerController.GetComponent<PenguinController>().SetSpeed(newSpeed);

        }
    }

    /*public class Pedra : Obstacle
    {
        public override string Name => "Pedra";

        public override float speedModifier => 0f;


        public override void affectPlayer(GameObject obstacle)
        {
            Debug.Log("Pedra is affecting!");
        }
    }*/

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


}

