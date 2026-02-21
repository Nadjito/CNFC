using UnityEngine;

public class ScoreController : MonoBehaviour
{
    public float score=0;
    [Header("Score Increase Settings")]
    [Space(5)]
    [Tooltip("Default score increase")]
    [SerializeField] private float scoreIncreaseValue= 1f;
    [Tooltip("How often score increases")]
    [SerializeField] private float scoreIncreaseRate = 0.2f;
    [Space(5)]
    [SerializeField] private PenguinController player;
    private float scoreMultiplier=1f;

    private float time = 0.0f;

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        if (time < scoreIncreaseRate)
        {
            scoreMultiplier = player.currentForwardSpeed / player.forwardSpeed;
            score += scoreIncreaseValue * scoreMultiplier * Time.deltaTime;
            time = 0.0f;
        }
    }
}
