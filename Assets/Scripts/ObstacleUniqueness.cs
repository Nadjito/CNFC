using Unity.VisualScripting;
using UnityEngine;
using static ObstacleManager;

public class ObstacleUniqueness : MonoBehaviour
{
    //[SerializeField] private GameObject ObstacleManager;

    private ObstacleManager _ObstacleManager;
    private Obstacle collisionObstacle;
    private Collider collider;

    public enum ObstacleType
    {
        SnowPlatform,
        IcePlatform,
        //Pedra
    }

    [SerializeField] private ObstacleType obstacleType;

    void OnEnable()
    {
        if(collider != null)
        {
            collider.enabled = true;

        }
    }

    void Start()
    {
        collider = gameObject.GetComponent<Collider>();
        _ObstacleManager = Object.FindAnyObjectByType<ObstacleManager>();

        switch (obstacleType)
        {
            case ObstacleType.SnowPlatform:
                collisionObstacle = new SnowPlatform();
                break;
            case ObstacleType.IcePlatform:
                collisionObstacle = new IcePlatform();
                break;
            default:
                break;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        collider.enabled = false;

        if (collisionObstacle != null)
        {
            collisionObstacle.affectPlayer(gameObject);
        }

    }
}
