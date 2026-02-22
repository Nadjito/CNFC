using System;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class PauseAndResume : MonoBehaviour
{
    public static bool isPaused;
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private Key pauseButton;
    [SerializeField] private TMP_Text pauseInfo;

    private string pauseButtonName;


    void Start()
    {
        pauseButtonName= pauseButton.ToString();
        pauseMenu.SetActive(false);
        isPaused = false;
        pauseInfo.text="Or press " + pauseButtonName + " again to pause/resume the game.";
        Debug.Log("Pause button is: " + pauseButtonName);
    }
    
    void Update()
    {
        if (Keyboard.current[pauseButton].wasPressedThisFrame)
        {
            Debug.Log("Pause button pressed.");
            if (!isPaused)
            {
                PauseGame();
            }
            else
            {
                ResumeGame();
            }
        }
    }

    public void ResumeGame()
    {
        pauseMenu.SetActive(false);
        isPaused = false;
        Time.timeScale = 1f; // Resume the game by setting time scale back to 1
    }

    private void PauseGame()
    {
        pauseMenu.SetActive(true);
        isPaused = true;
        Time.timeScale = 0f; // Pause the game by setting time scale to 0
    }
}
