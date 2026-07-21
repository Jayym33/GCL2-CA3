using UnityEngine;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SimpleTimer : MonoBehaviour
{
    [Header("Timer Settings")]
    public float timeRemaining = 120f; // Starting time in seconds,do make it 0 for timer and 60 for counting down
    public bool isCountdown = true;    // True for countdown, false for stopwatch
    public bool timerIsRunning = false;

    [Header("UI Components")]
    public TMP_Text timerText;

    private void Start()
    {
        // Start the timer automatically
        timerIsRunning = true;
    }

    void Update()
    {
        if (timerIsRunning)
        {
            if (isCountdown) //check if isCountdown = true
            {
                // Countdown logic
                if (timeRemaining > 0)
                {
                    timeRemaining -= Time.deltaTime; // Subtract elapsed frame time
                }
                else
                {
                    timeRemaining = 0;
                    timerIsRunning = false;
                    OnTimerEnd();
                }
            }
            else
            {
                // Stopwatch/Count-up logic
                timeRemaining += Time.deltaTime; // Add elapsed frame time
            }

            DisplayTime(timeRemaining);
        }
    }

    // Formats the float time into more accurate Minutes:Seconds
    void DisplayTime(float timeToDisplay)
    {
        // Calculate minutes and remaining seconds for timer
        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);

        // Format string to always show two digits (e.g., 01:05)
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    void OnTimerEnd()
    {
        Debug.Log("Time is up!");
        // Add game-over or level completion trigger logic here if going by counting down for this mechanic
        SceneManager.LoadScene("LoseScreen");
    }
}