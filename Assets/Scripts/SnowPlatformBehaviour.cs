using UnityEngine;

public class SnowPlatformBehaviour : MonoBehaviour
{
    private ObstacleCommon obstacleCommon;
    private GameObject player;
    private PenguinController playerController;

    [SerializeField] private float speedModifier;
    void Start()
    {
        obstacleCommon = transform.GetComponent<ObstacleCommon>();
    }

    // Update is called once per frame
    void Update()
    {
        if (obstacleCommon.triggerAbility)
        {
            player = obstacleCommon.GetComponent<ObstacleCommon>().player.gameObject;
            playerController = player.GetComponent<PenguinController>();

            if (player.transform.position.y > transform.position.y)//player goes over the snow platform, reduce speed
            {
                playerController.SetSpeed(playerController.GetSpeed() - speedModifier);
            }
            obstacleCommon.triggerAbility = false; // Reset the trigger after applying the effect
        }
    }
}
