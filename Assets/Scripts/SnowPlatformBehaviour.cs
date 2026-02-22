using UnityEngine;

public class SnowPlatformBehaviour : MonoBehaviour
{
    private ObstacleCommon obstacleCommon;
    private GameObject player;
    private PenguinController playerController;

    [Tooltip("Vertical buffer to determine if player is on top of snow platform")]
    [SerializeField] private float yBuffer;

    [SerializeField] private float speedModifier;

    private AudioManager audioManager;
    void Start()
    {
        obstacleCommon = transform.GetComponent<ObstacleCommon>();
        audioManager = transform.parent.GetComponent<ObstacleSpawner>().GameManager.GetComponent<AudioManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (obstacleCommon.triggerAbility)
        {
            player = obstacleCommon.GetComponent<ObstacleCommon>().player.gameObject;
            playerController = player.GetComponent<PenguinController>();

            if (player.transform.position.y + yBuffer > transform.position.y)//player goes over the snow platform, reduce speed
            {
                //Debug.Log("Player is on top of the snow platform, reducing speed.");
                audioManager.Play("snowHitSound");
                playerController.SetSpeed(playerController.GetSpeed() - speedModifier);
            }
            obstacleCommon.triggerAbility = false; // Reset the trigger after applying the effect
        }
    }
}
