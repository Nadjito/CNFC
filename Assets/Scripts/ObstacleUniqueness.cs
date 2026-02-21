using Unity.VisualScripting;
using UnityEngine;
using static ObstacleManager;

public class ObstacleUniqueness : MonoBehaviour
{
    //[SerializeField] private GameObject ObstacleManager;

    private ObstacleManager _ObstacleManager;

    public enum ObstacleType
    {
        SnowPlatform,
        IcePlatform,
        Pedra
    }

    [SerializeField] private ObstacleType obstacleType;

    /*void OnAwake()
    {

        switch (obstacleType)
        {
            case ObstacleType.SnowPlatform:
                _ObstacleManager = new SnowPlatform();
                break;
            case ObstacleType.IcePlatform:
                _ObstacleManager.ObstacleFactory.CreateObstacle("IcePlatform").affectPlayer();
                break;
            case ObstacleType.Pedra:
                _ObstacleManager.ObstacleFactory.CreateObstacle("Pedra").affectPlayer();
                break;
        }
    }*/

    private void OnCollisionEnter(Collision collision)
    {

    }
}
