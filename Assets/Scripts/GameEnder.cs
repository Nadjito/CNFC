using System;
using TMPro;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class GameEnder : MonoBehaviour
{
    [SerializeField] private float distanceToEnd;

    [SerializeField] private Transform player;
    [SerializeField] private Transform endPoint;

    [SerializeField] private Slider completionBar;
    
    [SerializeField] private GameObject gameCanvas;
    [SerializeField] private GameObject button;
    [SerializeField] private GameObject text;
    [SerializeField] private TMP_Text congrats;
    [SerializeField] public Button endButton;

    private float completionRate;
    private float totalDistance;
    private float endPosition;

    void Start()
    {
        completionRate = 0f;
        endPoint.position = player.position + new Vector3(distanceToEnd, 0f, 0f);
        totalDistance=Mathf.Abs(endPoint.position.x - player.position.x);
        endPosition= endPoint.position.x;
        Button btn = endButton.GetComponent<Button>();
        btn.onClick.AddListener(OnClick);
    }
        // Update is called once per frame
    void Update()
    {
        completionRate=1-((endPosition - player.position.x) / totalDistance);
        completionBar.value=completionRate;

        if (completionRate >= 1)
        {
            Time.timeScale=0f;
            gameCanvas.SetActive(true);
            button.SetActive(true);
            text.SetActive(true);
        }
    }
    
    public void OnClick()
    {
        Application.Quit();
        Debug.Log("Game Ended");
    }
}
