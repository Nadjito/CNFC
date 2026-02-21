using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScoreController : MonoBehaviour
{
    public float score=1;
    [Header("Score Increase Settings")]
    [Space(5)]
    [Tooltip("Default score increase")]
    [SerializeField] private float scoreIncreaseValue;
    [Tooltip("How often score increases")]
    [SerializeField] private float scoreIncreaseRate;
    [Space(5)]
    [SerializeField] private PenguinController player;
    private float scoreMultiplier=1f;

    [SerializeField] private TMP_Text ScoreValueTMP;

    private float time = 0.0f;

    private float defaultForwardSpeed;
    [SerializeField] private float speedMultiplier;

    [Tooltip("DONT CHANGE VALUE")]
    [SerializeField]private float scoreRate;

    //Test Purposes
    private float timeTest = 0.0f;
    [SerializeField] private float timeTestLimit;
    private bool testFinished = false;
    //

    void Start()
    {
        score = 1;
        defaultForwardSpeed = player.forwardSpeed;
        scoreRate = 0;
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        if (time > scoreIncreaseRate)
        {
            /*
            scoreMultiplier = player.currentForwardSpeed / defaultForwardSpeed;
            score += (float)Math.Pow(score, scoreMultiplier);
            time = 0.0f;*/
            /*
            scoreMultiplier = 1-defaultForwardSpeed/ player.currentForwardSpeed;
            score += score * scoreMultiplier;
            time = 0.0f;*/

            scoreRate = scoreIncreaseValue + Mathf.Pow(player.currentForwardSpeed, speedMultiplier);
            score += scoreRate;
            time = 0.0f;


        }


        ScoreValueTMP.text="Score: " + Mathf.Floor(score).ToString();

        if (!testFinished)
        {
            timeTest += Time.deltaTime;
            if (timeTest > timeTestLimit)
            {
                Debug.Log("Finished with score: " + Mathf.Floor(score).ToString());

                testFinished = true;
            }
        }
       
    }
}
