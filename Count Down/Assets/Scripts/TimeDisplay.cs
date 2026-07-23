using UnityEngine;
using TMPro;

public class TimeDisplay : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI timeText;

    private void OnEnable()
    {
        temp_timelogic.OnTimeStageChanged += UpdateDisplay;
    }

    private void OnDisable()
    {
        temp_timelogic.OnTimeStageChanged -= UpdateDisplay;
    }

    private void Start()
    {
        UpdateDisplay(0); // show correct value at start
    }

    private void UpdateDisplay(int stage)
    {
        timeText.text = "Time Stage: " + stage.ToString();
    }
}