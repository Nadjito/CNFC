using UnityEngine;

public class IcePlatformBehaviour : MonoBehaviour
{
    private ObstacleCommon obstacleCommon;
    private GameObject player;
    private PenguinController playerController;

    [SerializeField] private float speedModifier;

    // Update is called once per frame
    void Start()
    {
        obstacleCommon = transform.GetComponent<ObstacleCommon>();
    }

    void Update()
    {
        if(obstacleCommon.triggerAbility)
        {
            player = obstacleCommon.GetComponent<ObstacleCommon>().player.gameObject;
            playerController = player.GetComponent<PenguinController>();

            if (player.transform.position.y < transform.position.y)//player goes under the ice platform, reduce speed
            {
                playerController.SetSpeed(playerController.GetSpeed() - speedModifier);
            }
            else//player goes on top of the ice platform, increase speed
            {
                playerController.SetSpeed(playerController.GetSpeed() + speedModifier);
            }
            obstacleCommon.triggerAbility = false; // Reset the trigger after applying the effect
        }
    }
}
