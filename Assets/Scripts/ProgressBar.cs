using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour
{
    private const float InitialElapsedTime = 3f;

    [SerializeField]
    private Slider _loadingBar;

    [SerializeField]
    private float _loadingTime = 5f;

    private float _elapsedTime = 0f;
    public bool _isProcessing = false;

    private void Update()
    {
        if (_isProcessing)
        {
            if (_loadingBar == null)
            {
                Debug.LogWarning("Loading bar not assigned.");
                return;
            }

            _elapsedTime += Time.deltaTime;

            float progress = Mathf.Clamp01(_elapsedTime / _loadingTime);
            _loadingBar.value = progress;

            if (progress >= 1f)
            {
                _isProcessing = false;
                Debug.Log("Loading Complete!");
            }
        }
    }

    public void Start()
    {
        _elapsedTime = 0f;  // Reset elapsed time
        _isProcessing = true;
        Debug.Log(_isProcessing);
    }

    private void Awake()
    {
        _loadingBar.value = 0f;  // Set the initial value of the loading bar
        _isProcessing = false;
    }

}
