using UnityEngine;
using UnityEngine.UI;

public class GameEnder : MonoBehaviour
{
    [SerializeField] private float distanceToEnd;

    [SerializeField] private Transform player;
    [SerializeField] private Transform endPoint;

    [SerializeField] private Slider completionBar;

    private float completionRate;
    private float totalDistance;
    private float endPosition;

    void Start()
    {
        completionRate = 0f;
        endPoint.position = player.position + new Vector3(distanceToEnd, 0f, 0f);
        totalDistance=Mathf.Abs(endPoint.position.x - player.position.x);
        endPosition= endPoint.position.x;
    }
        // Update is called once per frame
    void Update()
    {
        completionRate=1-((endPosition - player.position.x) / totalDistance);
        completionBar.value=completionRate;

        if (completionRate >= 1)
        {
            Debug.Log("Game Finished");
        }
    }
}
