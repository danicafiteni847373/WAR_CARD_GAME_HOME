using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public TextMeshProUGUI timerText;
    private float timer;

    // Property to get the elapsed time
    public float ElapsedTime => timer;

    private void Start()
    {
        if (timerText == null)
        {
            Debug.LogError("Timer script requires a Text component. Please assign the Text component in the Inspector.");
            enabled = false; // Disable the script if Text component is not assigned
            return;
        }

       
    }

    private void Update()
    {
        // Update the timer
        timer += Time.deltaTime;

        // Display the timer on the Text component
        timerText.text = $"Time: {timer:F2}";
        Debug.Log("Timer Update");
    }

    private void OnApplicationQuit()
    {
        // Save the time when the application quits
        SaveTime();
    }

    private void OnDisable()
    {
        // Save the time when the script is disabled (e.g., when switching scenes)
        SaveTime();
    }

    private void SaveTime()
    {
        // Save the time using PlayerPrefs
        PlayerPrefs.SetFloat("SavedTime", timer);
        PlayerPrefs.Save();
    }

    public void LoadTime()
    {
        // Load the saved time from PlayerPrefs
        float savedTime = PlayerPrefs.GetFloat("SavedTime", 0f);
        timer = savedTime;
    }

}
