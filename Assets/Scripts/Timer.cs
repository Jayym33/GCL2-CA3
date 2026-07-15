using UnityEngine;
using TMPro;

public class SimpleTimer : MonoBehaviour
{
    [Header("Timer Settings")]
    public float timeRemaining = 0f; // Starting time in seconds, make it 0 for timer and 60 for counting down
    public bool isCountdown = false;    // True for countdown, false for stopwatch
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
            if (isCountdown)
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

    // Formats the float time into accurate Minutes:Seconds
    void DisplayTime(float timeToDisplay)
    {
        // Calculate minutes and remaining seconds
        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);

        // Format string to always show two digits (e.g., 01:05)
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    void OnTimerEnd()
    {
        Debug.Log("Time is up!");
        // Add game-over or level completion trigger logic here
    }
}