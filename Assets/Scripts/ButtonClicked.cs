using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonClicked : MonoBehaviour
{
    public ProgressBar progressBar;

    public void OnButtonClick()
    {
        Debug.Log("Button Clicked");

        if (progressBar == null)
        {
            Debug.LogError("ProgressBar not assigned in ButtonClicked script.");
            return;
        }

        if (progressBar._isProcessing)
        {
            Debug.Log("Starting Loading");
            progressBar.Start();
        }
        else
        {
            Debug.LogWarning("Loading is already in progress.");
        }
    }

}
